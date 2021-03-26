using App.Twilio;
using App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.HangFire
{
    public class SendAppoinmentConfirm
    {
        public SendAppoinmentConfirm()
        {

        }



        public void Execute(ConfirmAppoinment appointment)
        {
            string Message = appointment.Message;



            var twilioRestClient = new RestClient();

            //Changed this code


            twilioRestClient.SendSmsMessage(
           appointment.Phone, Message
           );



        }
    }
}
