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

    public async Task<List<NonPaperMedia>> GetAllAsync() =>
        await _repository.GetAllAsync();

    public async Task<NonPaperMedia?> GetAsync(int id)
    {
        NpmId npmId = new NpmId(id);

        return await _repository.GetByIdAsync(npmId);
    }

    public async Task<bool> UpdateAsync(int id, UpdatedNPMDto npmDto)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _repository.GetByIdAsync(npmId);

        if (nonPaperMedia == null)
            return false;

        UpdateNonPaperMedia(nonPaperMedia, npmDto);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        NpmId npmId = new NpmId(id);
        NonPaperMedia? nonPaperMedia = await _repository.GetByIdAsync(npmId);

        if (nonPaperMedia == null)
            return false;

        nonPaperMedia.ToBreak();
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private void UpdateNonPaperMedia(NonPaperMedia nonPaperMedia, UpdatedNPMDto npmDto)
    {
        if (string.IsNullOrWhiteSpace(npmDto.Manufacturer) == false || npmDto.Manufacturer != nonPaperMedia.Manufacturer)
            nonPaperMedia.UpdateManufacturer(npmDto.Manufacturer);

        if (string.IsNullOrWhiteSpace(npmDto.Model) == false || npmDto.Model != nonPaperMedia.Model)
            nonPaperMedia.UpdateModel(npmDto.Model);

        if (string.IsNullOrWhiteSpace(npmDto.SerialNumber) == false || npmDto.SerialNumber != nonPaperMedia.SerialNumber)
            nonPaperMedia.UpdateSerialNumber(npmDto.SerialNumber);

        if (npmDto.Capacity > 0 || npmDto.Capacity != nonPaperMedia.Capacity)
            nonPaperMedia.UpdateCapacity(npmDto.Capacity);

        if (npmDto.IsBroken == nonPaperMedia.IsBroken)
            return;

        if (npmDto.IsBroken)
            nonPaperMedia.ToBreak();
        else
            nonPaperMedia.Repair();
    }
}
