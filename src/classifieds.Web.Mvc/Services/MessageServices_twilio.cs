using Abp.Configuration;
using classifieds.Settings.Constants;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace classifieds.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : ISmsSender
    {
         private readonly ISettingManager _settingManager;

        public AuthMessageSender(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }


        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        public async Task<MessageResource> SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = await _settingManager.GetSettingValueAsync(SiteSettings.SmsId);
            // Your Auth Token from twilio.com/console
            var authToken = await _settingManager.GetSettingValueAsync(SiteSettings.SmsPassword); ;
            var smsNumber = await _settingManager.GetSettingValueAsync(SiteSettings.SmsNumber); 
            TwilioClient.Init(accountSid, authToken);

            return await MessageResource.CreateAsync(
              to: new PhoneNumber(number),
              from: new PhoneNumber(smsNumber),
              body: message);
        }
    }
}
