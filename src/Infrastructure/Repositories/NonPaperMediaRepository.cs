using ApplicationCore.Interfaces;
using Domain.Models.NPMs;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NonPaperMediaRepository : INonPaperMediaRepository
{
    private readonly SQLiteContext _context;

    public NonPaperMediaRepository(SQLiteContext context) =>
        _context = context;

    public void Delete(NonPaperMedia nonPaper) =>
        _context.NonPaperMedias.Remove(nonPaper);

    public async Task<List<NonPaperMedia>> GetAllAsync() =>
        await _context.NonPaperMedias.ToListAsync();

    public async Task<NonPaperMedia?> GetByIdAsync(NpmId id) =>
        await _context.NonPaperMedias.FindAsync(id);

    public void Insert(NonPaperMedia nonPaperMedia) =>
        _context.NonPaperMedias.Add(nonPaperMedia);
}
