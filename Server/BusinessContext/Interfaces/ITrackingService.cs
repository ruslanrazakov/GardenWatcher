using System.Threading.Tasks;

public interface ITrackingService
{
    /// <summary>
    /// Pushes data from sensors to DB. 
    /// Returns Task, because Hangfire.RecurringJob.AddOrUpdate() method takes Task as parameter
    /// </summary>
    /// <returns></returns>
    Task GetData();
}