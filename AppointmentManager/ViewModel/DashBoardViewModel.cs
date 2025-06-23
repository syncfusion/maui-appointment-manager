using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ManageAppointments
{
    /// <summary>
    /// ViewModel for dashboard appointments.
    /// </summary>
    public class DashboardViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Holds the missed appointments count.
        /// </summary>
        public ObservableCollection<DashAppointment> Appointments { get; private set; } = new();

        /// <summary>
        /// Holds the missed appointments count.
        /// </summary>
        public int UpcomingAppointmentsCount => Appointments.Count(a => a.Status == "Pending");

        /// <summary>
        /// Holds the missed appointments count.
        /// </summary>
        public int CompletedAppointmentsCount => Appointments.Count(a => a.Status == "Completed");

        /// <summary>
        /// Holds the missed appointments count.
        /// </summary>
        public int MissedAppointmentsCount => Appointments.Count(a => a.Status == "Missed");

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
        /// </summary>
        public DashboardViewModel()
        {
        }

        /// <summary>
        /// Assigns the shared appointment list from SchedulerViewModel.
        /// </summary>
        public void SetAppointmentsSource(ObservableCollection<DashAppointment> appointments)
        {
            if (Appointments != appointments)
            {
                if (Appointments != null)
                    Appointments.CollectionChanged -= (s, e) => UpdateStatistics();

                Appointments = appointments;

                Appointments.CollectionChanged += (s, e) => UpdateStatistics();

                SortAppointments();

                UpdateStatusesBasedOnCurrentTime(); // Update statuses here

                UpdateStatistics();
                OnPropertyChanged(nameof(Appointments));
            }
        }

        /// <summary>
        /// Method to sort appointments.
        /// </summary>
        private void SortAppointments()
        {
            var sorted = Appointments.OrderBy(a => a.Date).ToList();
            if (!sorted.SequenceEqual(Appointments))
            {
                Appointments.Clear();
                foreach (var item in sorted)
                    Appointments.Add(item);
            }
        }

        /// <summary>
        /// Method to update status based on current time.
        /// </summary>
        public void UpdateStatusesBasedOnCurrentTime()
        {
            var now = DateTime.Now;
            var rnd = new Random();

            foreach (var appointment in Appointments)
            {
                if (appointment.Date > now)
                {
                    appointment.Status = "Pending";
                }
                else
                {
                    appointment.Status = rnd.Next(2) == 0 ? "Missed" : "Completed";
                }
            }
            UpdateStatistics();
        }

        /// <summary>
        /// Method to update statistics.
        /// </summary>
        private void UpdateStatistics()
        {
            OnPropertyChanged(nameof(UpcomingAppointmentsCount));
            OnPropertyChanged(nameof(CompletedAppointmentsCount));
            OnPropertyChanged(nameof(MissedAppointmentsCount));
        }

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to trigger the property changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
