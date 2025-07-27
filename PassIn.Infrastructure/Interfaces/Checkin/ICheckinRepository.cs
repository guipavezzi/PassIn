public interface ICheckinRepository
{
    Task<CheckIn> Add(CheckIn entity);
    Task<bool> ExistCheckin(Guid attendeeId);
}