using ApplicationCore.Dtos.NPMs;
using ApplicationCore.Interfaces;
using Domain.Models.NPMs;

namespace ApplicationCore.Services;

public class NonPaperMediaService
{
    private readonly INonPaperMediaRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public NonPaperMediaService(INonPaperMediaRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(CreatedNPMdto dto)
    {
        NonPaperMedia nonPaperMedia = new NonPaperMedia(dto.Type, dto.Manufacturer, dto.Model, dto.SerialNumber, dto.Capacity);
        _repository.Insert(nonPaperMedia);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<NonPaperMedia>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<NonPaperMedia?> GetAsync(int id)
    {
        NpmId npmId = new NpmId(id);

        return await _repository.GetByIdAsync(npmId);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _repository.GetByIdAsync(npmId);

        if (nonPaperMedia == null)
            return false;

        _repository.Delete(nonPaperMedia);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
