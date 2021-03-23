using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace App.Twilio
{
    public class RestClient 
    {
        private readonly ITwilioRestClient _client;
        private readonly string _accountSid = Environment.GetEnvironmentVariable("AccountSid");
        private readonly string _authToken = Environment.GetEnvironmentVariable("AuthToken");
        private readonly string _twilioNumber = Environment.GetEnvironmentVariable("TwilioNumber");

        public RestClient()
        {
            _client = new TwilioRestClient(_accountSid, _authToken);
        }

        public RestClient(ITwilioRestClient client)
        {
            _client = client;
        }

        public void SendSmsMessage(string phoneNumber, string message)
        {
            var to = new PhoneNumber(phoneNumber);
            MessageResource.Create(
                to,
                from: new PhoneNumber(_twilioNumber),
                body: message,
                client: _client);
        }
    }
}
