
using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure;

public class CheckinRepository : ICheckinRepository
{
    private readonly PassInDbContext _context;

    public CheckinRepository(PassInDbContext context)
    {
        _context = context;
    }
    public async Task<CheckIn> Add(CheckIn entity)
    {
        await _context.CheckIns.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> ExistCheckin(Guid attendeeId)
    {
        return await _context.CheckIns.AnyAsync(ch => ch.AttendeeId == attendeeId);
    }
}