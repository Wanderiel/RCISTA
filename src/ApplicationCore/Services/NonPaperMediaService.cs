using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Exceptions;
using ApplicationCore.Extentions;
using ApplicationCore.Interfaces;
using Domain.Models.Base;
using Domain.Models.NPMs;

namespace ApplicationCore.Services;

public class NonPaperMediaService : INonPaperMediaService
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
        Manufacturer manufacturer = await _manufacturerRepository.GetOrCreateByName(dto.Manufacturer, cancellationToken);
        Model model = await _modelRepository.GetOrCreateByName(dto.Model, cancellationToken);
        NonPaperMedia nonPaperMedia = new NonPaperMedia(dto.Type, manufacturer, model, dto.SerialNumber, dto.Capacity);
        _npmRepository.Insert(nonPaperMedia);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<NpmResponse>> GetAll(CancellationToken cancellationToken = default) =>
        (await _npmRepository.GetAll(cancellationToken)).Select(npm => npm.ToDto()).ToList();

    public async Task<NpmResponse?> Get(int id, CancellationToken cancellationToken = default) =>
        (await GetNonPaperMedia(id, cancellationToken))?.ToDto();

    //TODO сломано, починить
    public async Task Update(int id, UpdatedNPMDto npmDto, CancellationToken cancellationToken = default)
    {
        NonPaperMedia nonPaperMedia = await GetNonPaperMedia(id, cancellationToken);
        //UpdateNonPaperMedia(nonPaperMedia, npmDto);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int id, CancellationToken cancellationToken = default)
    {
        NonPaperMedia nonPaperMedia = await GetNonPaperMedia(id, cancellationToken);
        nonPaperMedia.ToBreak();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsAvailable(NpmId id, CancellationToken cancellationToken = default) =>
        await _npmRepository.IsAvailable(id, cancellationToken);

    public async Task<List<NonPaperMedia>> FindDisks(List<int> intIds, CancellationToken cancellationToken = default)
    {
        List<NonPaperMedia> result = [];

        if (intIds.Count == 0)
            return result;

        List<NpmId> npmIds = intIds.Select(intId => new NpmId(intId)).ToList();
        result = await _npmRepository.GetByIds(npmIds, cancellationToken);

        if (result.Count < intIds.Count)
        {
            HashSet<int> missingIds = intIds.ToHashSet()
                .Except(
                    result.Select(d => d.Id.Value)
                    .ToHashSet())
                .ToHashSet();

            throw new MissingNonPaperMediaException(missingIds);
        }

        return result;
    }

    private async Task<NonPaperMedia> GetNonPaperMedia(int id, CancellationToken cancellationToken)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia nonPaperMedia = await _npmRepository.GetById(npmId, cancellationToken)
            ?? throw new MissingNonPaperMediaException(id);

        return nonPaperMedia;
    }
}
