using Microsoft.Maui.Graphics;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ManageAppointments
{
    /// <summary>
    /// ViewModel for managing and displaying scheduled appointments.
    /// </summary>
    public class SchedulerViewModel
    {
        private DashboardViewModel _dashboardViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerViewModel"/> class.
        /// </summary>
        public SchedulerViewModel()
        {
            _dashboardViewModel = new DashboardViewModel();
            InitializeAppointments();
            DisplayDate = DateTime.Now.Date.AddHours(9);
        }

        /// <summary>
        /// Gets or sets the collection of scheduled events.
        /// </summary>
        public ObservableCollection<Appointment>? Events { get; set; }

        /// <summary>
        /// Gets or sets the default display date for the scheduler.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        /// <summary>
        /// Initializes and populates the appointment collection based on dashboard data.
        /// </summary>
        private void InitializeAppointments()
        {
            Events = new ObservableCollection<Appointment>();

            var sortedAppointments = _dashboardViewModel.Appointments
                .OrderBy(a => a.Date) // Ensure appointments are sorted by DateTime
                .ToList();

            foreach (var dashAppointment in sortedAppointments)
            {
                DateTime appointmentDateTime = dashAppointment.Date;

                var meeting = new Appointment
                {
                    From = appointmentDateTime,
                    To = appointmentDateTime.AddHours(1), // Default duration is 1 hour, can be customized
                    EventName = dashAppointment.PatientName,
                    Background = GetColor(dashAppointment.Subject),
                    Location = GetImage(dashAppointment.Subject),
                    IsAllDay = false
                };

                Events.Add(meeting);
            }
        }

        /// <summary>
        /// Returns a brush color based on the event name.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <returns>A <see cref="Brush"/> representing the event color.</returns>
        private Brush GetColor(string eventName)
        {
            return new SolidColorBrush(GetColorForEvent(eventName));
        }

        /// <summary>
        /// Determines the color associated with an event name.
        /// </summary>
        /// <param name="eventName">The event name.</param>
        /// <returns>A <see cref="Color"/> representing the event type.</returns>
        private Color GetColorForEvent(string eventName)
        {
            return eventName switch
            {
                "General Check-Up" => Color.FromArgb("#1000C2"),
                "Asthma" => Color.FromArgb("#136154"),
                "Diagnostic report" => Color.FromArgb("#6A01F5"),
                "Diabetes" => Color.FromArgb("#803500"),
                "Hypothermia" => Color.FromArgb("#1D55AA"),
                "Angina" => Color.FromArgb("#8800D1"),
                _ => Color.FromArgb("#1000C2"),
            };
        }

        /// <summary>
        /// Returns the image file name associated with an event name.
        /// </summary>
        /// <param name="eventName">The event name.</param>
        /// <returns>A string representing the image file name.</returns>
        private string GetImage(string eventName)
        {
            return eventName switch
            {
                "General Check-Up" => "checkup.png",
                "Asthma" => "respiratory.png",
                "Diagnostic report" => "diagnostic.png",
                "Diabetes" => "glucose.png",
                "Hypothermia" => "body.png",
                "Angina" => "heart.png",
                _ => "checkup.png",
            };
        }
    }
}
