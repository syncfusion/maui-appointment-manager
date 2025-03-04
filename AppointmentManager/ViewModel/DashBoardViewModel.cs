using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ManageAppointments
{
    /// <summary>
    /// ViewModel for managing appointment data and statistics in the dashboard.
    /// </summary>
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private static readonly string[] subjects = { "General Check-Up", "Asthma", "Diagnostic Report", "Diabetes", "Hypothermia", "Angina" };
        private static readonly Random random = new Random();
        private DateTime today = DateTime.Now;

        /// <summary>
        /// Gets the list of appointments.
        /// </summary>
        public ObservableCollection<DashAppointment> Appointments { get; set; }

        /// <summary>
        /// Gets the count of upcoming appointments with "Pending" status.
        /// </summary>
        public int UpcomingAppointmentsCount => Appointments.Count(a => a.Status == "Pending");

        /// <summary>
        /// Gets the count of completed appointments.
        /// </summary>
        public int CompletedAppointmentsCount => Appointments.Count(a => a.Status == "Completed");

        /// <summary>
        /// Gets the count of missed appointments.
        /// </summary>
        public int MissedAppointmentsCount => Appointments.Count(a => a.Status == "Missed");

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        public DashboardViewModel()
        {
            Appointments = new ObservableCollection<DashAppointment>();

            GenerateAppointments();

            // Sort appointments after adding
            SortAppointments();

            // Update statistics whenever the collection changes
            Appointments.CollectionChanged += (s, e) => UpdateStatistics();
        }

        /// <summary>
        /// Generates sample appointment data.
        /// </summary>
        private void GenerateAppointments()
        {
            // 🔹 Past Appointments → Mix of "Completed" and "Missed"
            for (int i = 4; i >= 1; i--)
            {
                DateTime pastDate = today.AddDays(-i).AddHours(random.Next(9, 17)).AddMinutes(random.Next(0, 60));
                string status = (i % 2 == 0) ? "Completed" : "Missed";
                Appointments.Add(CreateAppointment($"1000{i}", status, pastDate));
            }

            // 🔹 Today's Appointments → At least one "Completed", one "Missed", and one "Pending"
            Appointments.Add(CreateAppointment("20001", "Completed", today.Date.AddHours(random.Next(9, 12)).AddMinutes(random.Next(0, 60))));
            Appointments.Add(CreateAppointment("20002", "Missed", today.Date.AddHours(random.Next(12, 14)).AddMinutes(random.Next(0, 60))));
            Appointments.Add(CreateAppointment("20003", "Pending", today.Date.AddHours(random.Next(14, 17)).AddMinutes(random.Next(0, 60))));

            // 🔹 Future Appointments → All "Pending"
            for (int i = 1; i <= 5; i++)
            {
                DateTime futureDate = today.AddDays(i).AddHours(random.Next(9, 17)).AddMinutes(random.Next(0, 60));
                Appointments.Add(CreateAppointment($"3000{i}", "Pending", futureDate));
            }
        }

        /// <summary>
        /// Creates a new appointment instance.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <param name="status">The appointment status.</param>
        /// <param name="date">The appointment date.</param>
        /// <returns>A new <see cref="DashAppointment"/> object.</returns>
        private DashAppointment CreateAppointment(string id, string status, DateTime date)
        {
            return new DashAppointment
            {
                ID = id,
                PatientName = $"Patient {id}",
                PhoneNumber = $"(+91)9{random.Next(100000000, 999999999)}",
                Date = date,
                Subject = subjects[random.Next(subjects.Length)],
                Status = status
            };
        }

        /// <summary>
        /// Sorts appointments in ascending order by date.
        /// </summary>
        private void SortAppointments()
        {
            var sortedAppointments = Appointments.OrderBy(a => a.Date).ToList();
            if (!sortedAppointments.SequenceEqual(Appointments))
            {
                Appointments.Clear();
                foreach (var appointment in sortedAppointments)
                {
                    Appointments.Add(appointment);
                }
            }
        }

        /// <summary>
        /// Updates appointment statistics.
        /// </summary>
        private void UpdateStatistics()
        {
            OnPropertyChanged(nameof(UpcomingAppointmentsCount));
            OnPropertyChanged(nameof(CompletedAppointmentsCount));
            OnPropertyChanged(nameof(MissedAppointmentsCount));
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies when a property value changes.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
