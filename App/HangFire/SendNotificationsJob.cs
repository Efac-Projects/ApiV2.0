using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest;
using App.Twilio;
using App.Models;
using App.Repositories;

namespace App.HangFire
{
    public class SendNotificationsJob
    {
        private const string MessageTemplate =
           "Hi {0}. Just a reminder that you have an appointment coming up at {1}.";

        public void Execute()
        {
            var twilioRestClient = new RestClient();

            //Changed this code
            foreach (var appointment in AvailableAppointments())
            {
                twilioRestClient.SendSmsMessage(
               appointment.PhoneNumber,
               string.Format(MessageTemplate, appointment.FirstName, appointment.StartMoment.ToString("t")));
            }

          
        }

        private static IList<Appointment> AvailableAppointments()
        {
            return new AppointmentsFinder(new AppointmentRepository(), new TimeConverter())
                .FindAvailableAppointments(DateTime.Now);
        }
    }
}
