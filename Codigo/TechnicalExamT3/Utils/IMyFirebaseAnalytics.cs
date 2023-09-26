namespace TechnicalExamT3.Utils
{
    public interface IMyFirebaseAnalytics
    {
        Task<dynamic> SendEventAsync(string eventName, object eventSend);
    }
}
