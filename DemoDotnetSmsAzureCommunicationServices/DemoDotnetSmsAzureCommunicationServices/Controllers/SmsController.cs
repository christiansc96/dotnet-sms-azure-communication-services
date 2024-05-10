using Microsoft.AspNetCore.Mvc;
using SmsManager;
using SmsManager.AzureCommunicationServices;

namespace DemoDotnetSmsAzureCommunicationServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController(ISmsAzureCommunicationServicesRepository smsAzureCommunicationServicesRepository) : ControllerBase
    {
        private readonly ISmsAzureCommunicationServicesRepository _smsAzureCommunicationServicesRepository = smsAzureCommunicationServicesRepository;

        [HttpPost]
        public async Task<IActionResult> SendSms(MessageDto message)
        {
            bool smsResponse = await _smsAzureCommunicationServicesRepository.SendSms(message);
            return smsResponse ? Ok(smsResponse) : BadRequest();
        }
    }
}