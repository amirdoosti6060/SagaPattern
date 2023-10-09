namespace SagaPattern
{
    public interface IActivity
    {
        Task<ActivityStatus> ExecuteAsync();
        Task<ActivityStatus> CompensateAsync();
    }
}
