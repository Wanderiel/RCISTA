using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;
using Domain.Models.Base;
using Domain.Models.NPMs;

namespace ApplicationCore.Services;

public class NonPaperMediaService
{
    private readonly INonPaperMediaRepository _npmRepository;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IModelRepository _modelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NonPaperMediaService(
        INonPaperMediaRepository repository,
        IManufacturerRepository manufacturerRepository,
        IModelRepository modelRepository,
        IUnitOfWork unitOfWork)
    {
        _npmRepository = repository;
        _manufacturerRepository = manufacturerRepository;
        _modelRepository = modelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(NpmRequest dto, CancellationToken cancellationToken = default)
    {
        Manufacturer manufacturer = await _manufacturerRepository.GetOrCreateByNameAsync(dto.Manufacturer, cancellationToken);
        Model model = await _modelRepository.GetOrCreateByNameAsync(dto.Model, cancellationToken);
        NonPaperMedia nonPaperMedia = new NonPaperMedia(dto.Type, manufacturer, model, dto.SerialNumber, dto.Capacity);
        _npmRepository.Insert(nonPaperMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<NpmResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await _npmRepository.GetAllAsync(cancellationToken)).Select(npm => npm.ToDto()).ToList();

    public async Task<NpmResponse?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        NpmId npmId = new NpmId(id);

        return (await _npmRepository.GetByIdAsync(npmId, cancellationToken))?.ToDto();
    }

    //TODO сломано, починить
    public async Task<bool> UpdateAsync(int id, UpdatedNPMDto npmDto, CancellationToken cancellationToken = default)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _npmRepository.GetByIdAsync(npmId, cancellationToken);

        if (nonPaperMedia == null)
            return false;

        //UpdateNonPaperMedia(nonPaperMedia, npmDto);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _npmRepository.GetByIdAsync(npmId, cancellationToken);

        if (nonPaperMedia == null)
            return false;

        nonPaperMedia.ToBreak();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
