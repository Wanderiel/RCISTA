using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Interfaces;
using Domain.Models.NPMs;

namespace ApplicationCore.Services;

public class NonPaperMediaService
{
    private readonly INonPaperMediaRepository _npmRepository;
    private readonly IModelRepository _modelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NonPaperMediaService(INonPaperMediaRepository repository, IModelRepository modelRepository, IUnitOfWork unitOfWork)
    {
        _npmRepository = repository;
        _modelRepository = modelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CreatedNPMdto dto)
    {
        Model model = await _modelRepository.GetOrCreateByNameAsync(dto.Model);
        NonPaperMedia nonPaperMedia = new NonPaperMedia(dto.Type, dto.Manufacturer, model, dto.SerialNumber, dto.Capacity);
        _npmRepository.Insert(nonPaperMedia);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<NonPaperMedia>> GetAllAsync() =>
        await _npmRepository.GetAllAsync();

    public async Task<NonPaperMedia?> GetAsync(int id)
    {
        NpmId npmId = new NpmId(id);

        return await _npmRepository.GetByIdAsync(npmId);
    }

    //TODO сломано, починить
    public async Task<bool> UpdateAsync(int id, UpdatedNPMDto npmDto)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _npmRepository.GetByIdAsync(npmId);

        if (nonPaperMedia == null)
            return false;

        //UpdateNonPaperMedia(nonPaperMedia, npmDto);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _npmRepository.GetByIdAsync(npmId);

        if (nonPaperMedia == null)
            return false;

        nonPaperMedia.ToBreak();
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
