namespace SmsManager.AzureCommunicationServices
{
    public interface ISmsAzureCommunicationServicesRepository
    {
        Task<bool> SendSms(MessageDto input);
        Task<bool> SendSmsList(MessageDto input);
    }
}