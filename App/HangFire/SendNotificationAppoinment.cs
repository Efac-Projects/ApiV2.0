using App.Models;
using App.Twilio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.HangFire
{
    public class SendNotificationAppoinment
    {
       

        public SendNotificationAppoinment()
        {
            
        }

        private const string MessageTemplate =
          "Hi {0}. Just a reminder that you have an appointment coming up at {1}. One of our agent will confirm your appoinment";

        public void Execute(Appointment appointment)
        {
            var twilioRestClient = new RestClient();

            //Changed this code
            
            
                twilioRestClient.SendSmsMessage(
               appointment.PhoneNumber,
               string.Format(MessageTemplate, appointment.FirstName, appointment.StartMoment.ToString("t")));
            


        }
    }
}
