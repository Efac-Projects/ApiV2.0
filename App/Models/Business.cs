using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models    // edited now have to change database view
{
    public class Business
    {
        public int BusinessId { get; set;}
        public string Name { get; set; }
        public string Email { get; set; }
        public int TotalCrowd { get; set; }

        [DefaultValue(5)]
        public int CurrentCrowd { get; set; }

        public int PhoneNumber { get; set; } // from business sign up
        public int PostalCode { get; set; } // from business sign up

        public string BusinessType { get; set; }
        public string Summary { get; set; }
        public OpeningHour OpeningHours { get; set; }
        public IList<Appointment> Appointments { get; set; }
        public IList<Treatment> Treatments { get; set; } // looks like doctors in medical centre

        private TimeSpan _maxTimeBetweenAppointments = new TimeSpan(0, 15, 0);
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }

        



        public Business() { }

        public Business(string name, string email)
        {
            Name = name;
            Email = email;
            Treatments = new List<Treatment>();
            OpeningHours = new OpeningHour();
            Appointments = new List<Appointment>();
            //OpeningHours.WorkDays = new List<WorkDay>();
            OpeningHours.FillHours();
        }

        public Business(string name, string email, IList<Treatment> treatments, IList<WorkDay> workDays) : this(name, email)
        {
            //Name = name;
            //Email = email;
            Treatments = treatments;
            OpeningHours = new OpeningHour { WorkDays = workDays };
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

        public bool AddAppointment(Appointment appointment)
        {

            if (NotInOpeningHours(appointment))
                return false;

            if (OverlappingWithAppointment(appointment.StartMoment, appointment.EndMoment))
                return false;

            Appointments.Add(appointment);

            return true;

        }


        public void RemoveAppointment(Appointment appointment)
        {
            appointment.Threatment = null;
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

        //---------------------------------------------------------

        public IList<DateTime> GiveAvailableTimesOnDate(DateTime date, IEnumerable<Treatment> treatments)
        {
            IList<DateTime> avaiableTimes = new List<DateTime>();

            TimeSpan totalDuration = new TimeSpan();
            foreach (Treatment tr in treatments) totalDuration = totalDuration.Add(tr.Duration);

            //Time time = OpeningHours.WorkDays.SingleOrDefault(wd => wd.Day == date.DayOfWeek).Hours[0];

            IList<TimeRange> hours = OpeningHours.WorkDays.SingleOrDefault(wd => wd.Day == date.DayOfWeek).Hours;

            if (hours.Count < 1)
                return avaiableTimes;

            foreach (TimeRange hour in hours)
            {
                DateTime closingHour = GiveDateTime(hour.EndTime, date);
                DateTime startTime = GiveDateTime(hour.StartTime, date);
                DateTime endTime = startTime.Add(totalDuration);


                while (endTime <= closingHour)
                {
                    if (!OverlappingWithAppointment(startTime, endTime))
                        avaiableTimes.Add(startTime);

                    startTime = startTime.Add(_maxTimeBetweenAppointments);
                    endTime = startTime.Add(totalDuration);
                }
            }

            return avaiableTimes;
        }

        public void ChangeWorkDays(int dayId, List<TimeRange> hours)
        {
            DayOfWeek day = (DayOfWeek)(dayId);
            this.OpeningHours.EditHoursOfDay(day, hours);
        }

        //private bool NotInOpeningHours2(Appointment appointment)
        //{
        //    IList<DateTime> openingHours = new List<DateTime>();
        //    IList<Time> workDay = OpeningHours.WorkDays.Single(wd => wd.Day == appointment.StartMoment.DayOfWeek).Hours;
        //    IList<bool> flags = new List<bool>();

        //    if (workDay.Count < 1)
        //        return true;

        //    for (int i = 0; i < workDay.Count; i++)
        //    {
        //        openingHours.Add(new DateTime(appointment.StartMoment.Year, appointment.StartMoment.Month, appointment.StartMoment.Day, workDay[i].Hour, workDay[i].Minute, workDay[i].Second));
        //    }

        //    for (int i = 0; i < openingHours.Count; i++)
        //    {
        //        if (i % 2 != 0)
        //            continue;

        //        if (appointment.StartMoment <= openingHours[i] && appointment.EndMoment <= openingHours[i + 1])
        //            flags.Add(true);
        //        else
        //            flags.Add(false);
        //    }

        //    return (!flags.Contains(true));
        //}

        private bool NotInOpeningHours(Appointment appointment)
        {
            IList<bool> flags = new List<bool>();
            IList<TimeRange> workDay = OpeningHours.WorkDays.Single(wd => wd.Day == appointment.StartMoment.DayOfWeek).Hours;

            if (workDay.Count < 1)
                return true;

            foreach (TimeRange period in workDay)
            {
                if (appointment.StartMoment >= GiveDateTime(period.StartTime, appointment.StartMoment) && appointment.EndMoment <= GiveDateTime(period.EndTime, appointment.EndMoment))
                    flags.Add(true);
                else
                    flags.Add(false);
            }

            return (!flags.Contains(true));
        }

        private DateTime GiveDateTime(Time time, DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }

        //private bool OverlappingWithAppointment(Appointment appointment)
        //{
        //    foreach (Appointment a in Appointments)
        //    {
        //        if (!(appointment.StartMoment <= a.EndMoment && appointment.EndMoment <= a.StartMoment))
        //            if (!(appointment.StartMoment >= a.EndMoment && appointment.EndMoment >= a.StartMoment))
        //                return true;
        //    }

        //    return false;
        //}

        private bool OverlappingWithAppointment(DateTime startMoment, DateTime endMoment)
        {
            foreach (Appointment a in Appointments)
            {
                if (!(startMoment <= a.EndMoment && endMoment <= a.StartMoment))
                    if (!(startMoment >= a.EndMoment && endMoment >= a.StartMoment))
                        return true;
            }

            return false;
        }





    }
}
