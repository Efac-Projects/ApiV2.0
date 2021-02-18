﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models    // edited now have to change database view
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalCrowd { get; set; }
        public int CurrentCrowd { get; set; }
        public int EmergencyAppointment { get; set; } // If this emf appo give priority, like 5 emg app 
        public int PhoneNumber { get; set; } // from business sign up
        public int PostalCode { get; set; } // from business sign up
         public OpeningHours OpeningHours { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public IList<Treatment> Treatments { get; set; }

        private TimeSpan _maxTimeBetweenAppointments = new TimeSpan(0, 15, 0);


        public Business() { }

        public Business(string name, string email)
        {
            Name = name;
            Email = email;
            Treatments = new List<Treatment>();
            OpeningHours = new OpeningHours();
            Appointments = new List<Appointment>();
            //OpeningHours.WorkDays = new List<WorkDay>();
            OpeningHours.FillHours();
        }

        public Business(string name, string email, IList<Treatment> treatments, IList<WorkDay> workDays) : this(name, email)
        {
            //Name = name;
            //Email = email;
            Treatments = treatments;
            OpeningHours = new OpeningHours { WorkDays = workDays };
            //Appointments = new List<Appointment>();

            OpeningHours.WorkDays = workDays;

            //List<Time> hoursMonday = new List<Time>();
            //hoursMonday.Add(new Time(7, 30, 0));
            //hoursMonday.Add(new Time(12, 30, 0));
            //hoursMonday.Add(new Time(13, 30, 0));
            //hoursMonday.Add(new Time(18, 30, 0));

            //OpeningHours.EditHoursOfDay(DayOfWeek.Monday, hoursMonday);
        }

        public Appointment GetAppointment(int id)
        {
            return Appointments.FirstOrDefault(app => app.AppointmentId == id);
        }

        public Treatment GetTreatment(int id)
        {
            return Treatments.FirstOrDefault(tr => tr.TreatmentId == id);
        }

        public void AddTreatment(Treatment treatment)
        {
            Treatments.Add(treatment);
        }

       // to add appointment, we have to check opening hours. method should ...

        public void RemoveAppointment(Appointment appointment)
        {
            appointment.Treatments = null;
            Appointments.Remove(appointment);
        }

        public bool UpdateTreatment(Treatment treatment)
        {
            Treatment treatment1 = Treatments.SingleOrDefault(tr => tr.TreatmentId == treatment.TreatmentId);

            if (treatment1 == null)
                return false;

            treatment1.Name = treatment.Name;
            treatment1.Duration = treatment.Duration;
            treatment1.Price = treatment.Price;
            treatment1.Category = treatment.Category;

            return true;
        }

        public void RemoveTreatment(Treatment treatment)
        {
            Treatments.Remove(treatment);
        }



        //------------------------

        // Add method here to check get availble time for the appointment

        //----------------------- 


    }
}
