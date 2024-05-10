using Azure;
using Azure.Communication.Sms;

namespace SmsManager.AzureCommunicationServices
{
    public class SmsAzureCommunicationServicesRepository : ISmsAzureCommunicationServicesRepository
    {
        private const string connectionString = "<connection_string>";

        public async Task<bool> SendSms(MessageDto input)
        {
            bool smsResult = false;
            try
            {
                SmsClient smsClient = new(connectionString);

                SmsSendResult sendResult = await smsClient.SendAsync(
                    from: input.Sender, 
                    to: input.PhoneNumbers.First(),
                    message: input.Message);
                Console.WriteLine($"Sms id: {sendResult.MessageId}");
                smsResult = sendResult.HttpStatusCode == 200;
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return smsResult;
        }

        public async Task<bool> SendSmsList(MessageDto input)
        {
            bool smsResult = false;
            try
            {
                SmsClient smsClient = new(connectionString);

                Response<IReadOnlyList<SmsSendResult>> response = await smsClient
                    .SendAsync(from: input.Sender, to: input.PhoneNumbers,
                    message: input.Message,
                    options: new SmsSendOptions(enableDeliveryReport: true)
                    {
                        Tag = input.Tag,
                    });

                smsResult = true;
                foreach (SmsSendResult result in response.Value)
                {
                    if (result.Successful)
                        Console.WriteLine($"Successfully sent this message: {result.MessageId} to {result.To}.");
                    else
                    {
                        Console.WriteLine($"Something went wrong when trying to send this message {result.MessageId} to {result.To}.");
                        Console.WriteLine($"Status code {result.HttpStatusCode} and error message {result.ErrorMessage}.");
                        smsResult = false;
                        break;
                    }
                }
            }
            catch (RequestFailedException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return smsResult;
        }
    }
}