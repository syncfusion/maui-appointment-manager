using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ManageAppointments
{
    /// <summary>
    /// ViewModel for managing and displaying scheduled appointments.
    /// Appointments are generated only once per app lifetime and reused on subsequent ViewModel instances.
    /// </summary>
    public class SchedulerViewModel : INotifyPropertyChanged
    {
        // Static flag to ensure appointments are generated once
        private static bool _appointmentsInitialized = false;

        // Static collections to keep appointments for app lifetime
        private static ObservableCollection<Appointment> _staticEvents = new();

        /// <summary>
        /// Gets or sets the observable collection of static dashboard appointments.
        /// </summary>
        private static ObservableCollection<DashAppointment> _staticDashboardAppointments = new();

        /// <summary>
        /// Holds the random instance.
        /// </summary>
        private readonly Random random = new();

        /// <summary>
        /// Gets or sets the value indicating is open or not.
        /// </summary>
        private bool isOpen;

        /// <summary>
        /// Gets or sets the appointment.
        /// </summary>
        private Appointment? appointment;

        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        private DateTime selectedDate;

        // Instance properties point to static collections
        public ObservableCollection<Appointment> Events { get; set; }

        /// <summary>
        /// Gets or sets the observable collection of dashboard viewModel.
        /// </summary>
        public ObservableCollection<DashAppointment> DashboardAppointments { get; set; }

        /// <summary>
        /// Gets or sets the dashboard viewModel.
        /// </summary>
        public DashboardViewModel DashboardVM { get; set; } = new();

        /// <summary>
        /// Gets or sets the display date.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        /// <summary>
        /// Gets or sets the current date.
        /// </summary>
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the appointment editor model.
        /// </summary>
        public AppointmentEditorModel AppointmentEditorModel { get; set; }

        /// <summary>
        /// Gets or sets the command for add appointment.
        /// </summary>
        public Command AddAppointment { get; set; }

        /// <summary>
        /// Gets or sets the command for delete appointment.
        /// </summary>
        public Command DeleteAppointment { get; set; }

        /// <summary>
        /// Gets or sets the command for cancel appointment.
        /// </summary>
        public Command CancelAppointment { get; set; }

        /// <summary>
        /// Gets or Sets the value indicating is open or not.
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                OnPropertyChanged(nameof(this.IsOpen));
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        ///  Initializes a new instance of the <see cref="SchedulerViewModel"/> class.
        /// </summary>
        public SchedulerViewModel()
        {
            // Use static collections so data persists for the app session
            Events = _staticEvents;
            DashboardAppointments = _staticDashboardAppointments;

            // Generate appointments once when first instance created
            if (!_appointmentsInitialized)
            {
                InitializeAppointments();
                _appointmentsInitialized = true;
            }

            DisplayDate = DateTime.Now.Date.AddHours(9);

            // Link scheduler appointments to dashboard
            DashboardVM.SetAppointmentsSource(DashboardAppointments);

            this.AppointmentEditorModel = new AppointmentEditorModel();
            this.AddAppointment = new Command(AddAppointmentDetails);
            this.DeleteAppointment = new Command(DeleteSchedulerAppointment);
            this.CancelAppointment = new Command(CancelSchedulerAppointment);
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify the UI of property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Method to initialize appointments.
        /// </summary>
        private void InitializeAppointments()
        {
            Events.Clear();
            DashboardAppointments.Clear();

            TimeSpan workDayStart = TimeSpan.FromHours(9);
            TimeSpan workDayEnd = TimeSpan.FromHours(21);
            TimeSpan appointmentDuration = TimeSpan.FromHours(1);

            string[] subjects = { "General Check-Up", "Asthma", "Diagnostic Report", "Diabetes", "Hypothermia", "Angina" };
            string[] statuses = { "Pending", "Completed", "Missed" };

            int appointmentId = 1000;
            var patientPool = new List<string>(); // Existing patient names

            for (int weekOffset = -1; weekOffset <= 0; weekOffset++)
            {
                DateTime weekStartDate = DateTime.Now.Date.AddDays(weekOffset * 7);

                for (int dayOffset = 0; dayOffset < 7; dayOffset++)
                {
                    DateTime date = weekStartDate.AddDays(dayOffset);
                    var dailyAppointments = new List<(DateTime start, DateTime end)>();

                    int appointmentsPerDay = 3;
                    for (int i = 0; i < appointmentsPerDay; i++)
                    {
                        DateTime fromTime;
                        DateTime toTime;
                        int attempts = 0;

                        do
                        {
                            int totalAvailableMinutes = (int)(workDayEnd - workDayStart - appointmentDuration).TotalMinutes;
                            int randomMinuteOffset = random.Next(0, totalAvailableMinutes + 1);

                            fromTime = date.Add(workDayStart).AddMinutes(randomMinuteOffset);
                            toTime = fromTime.Add(appointmentDuration);

                            attempts++;
                            if (attempts > 50)
                                break;

                        } while (IsClashing(fromTime, toTime, dailyAppointments));

                        dailyAppointments.Add((fromTime, toTime));

                        string patientName;
                        if (patientPool.Count > 0 && random.NextDouble() < 0.3) // 30% chance to reuse a patient
                        {
                            patientName = patientPool[random.Next(patientPool.Count)];
                        }
                        else
                        {
                            patientName = $"Patient {appointmentId}";
                            patientPool.Add(patientName);
                        }

                        string subject = subjects[(appointmentId - 1000) % subjects.Length];
                        string status = statuses[(appointmentId - 1000) % statuses.Length];
                        string notes = GenerateNotes(patientName, subject, fromTime, toTime);

                        Events.Add(new Appointment
                        {
                            From = fromTime,
                            To = toTime,
                            EventName = patientName,
                            Background = new SolidColorBrush(GetColorForSubject(subject)),
                            Location = GetImage(subject),
                            IsAllDay = false,
                            Notes = notes
                        });

                        DashboardAppointments.Add(new DashAppointment
                        {
                            ID = appointmentId.ToString(),
                            PatientName = patientName,
                            PhoneNumber = GenerateMaskedPhoneNumber(),
                            Date = fromTime,
                            Subject = subject,
                            Status = status
                        });

                        appointmentId++;
                    }
                }
            }
        }

        // Helper method to check if new appointment clashes with existing daily appointments
        private bool IsClashing(DateTime newStart, DateTime newEnd, List<(DateTime start, DateTime end)> existingAppointments)
        {
            foreach (var appt in existingAppointments)
            {
                // Check if time intervals overlap
                if (newStart < appt.end && newEnd > appt.start)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to get color for subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns>The color value.</returns>
        private Color GetColorForSubject(string subject)
        {
            return subject switch
            {
                "General Check-Up" => Color.FromArgb("#2196F3"),
                "Asthma" => Color.FromArgb("#0CBD1E"),
                "Diagnostic Report" => Color.FromArgb("#E2227E"),
                "Diabetes" => Color.FromArgb("#9215F3"),
                "Hypothermia" => Color.FromArgb("#FF4E4E"),
                "Angina" => Color.FromArgb("#F44336"),
                _ => Color.FromArgb("#2196F3"),
            };
        }

        /// <summary>
        /// Method to get image.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns>The image png.</returns>
        private string GetImage(string subject)
        {
            return subject switch
            {
                "General Check-Up" => "checkup.png",
                "Asthma" => "respiratory.png",
                "Diagnostic Report" => "diagnostic.png",
                "Diabetes" => "glucose.png",
                "Hypothermia" => "temperature.png",
                "Angina" => "heart.png",
                _ => "checkup.png"
            };
        }

        /// <summary>
        /// Method to generate masked phone number.
        /// </summary>
        /// <returns>The masked phone number.</returns>
        private string GenerateMaskedPhoneNumber()
        {
            Random random = new Random();

            int areaCode = random.Next(200, 999);
            int prefix = random.Next(200, 999);
            int lineNumber = random.Next(0, 9999);

            return $"+1 ({areaCode}) {prefix}-****";
        }

        /// <summary>
        /// Method to generate notes.
        /// </summary>
        /// <param name="patientName">The patient name.</param>
        /// <param name="currentSubject">The current subject.</param>
        /// <param name="fromTime">The from time.</param>
        /// <param name="toTime">The to time.</param>
        /// <returns>The notes.</returns>
        private string GenerateNotes(string patientName, string currentSubject, DateTime fromTime, DateTime toTime)
        {
            bool isPastAppointment = fromTime < DateTime.Now && toTime < DateTime.Now;

            if (isPastAppointment)
            {
                return $"The {patientName} visited for a consultation regarding {currentSubject.ToLower()}.";
            }
            else
            {
                var pastSubjects = new HashSet<string>();

                foreach (var appt in Events)
                {
                    if (appt.EventName == patientName && appt.To < DateTime.Now)
                    {
                        if (!string.IsNullOrEmpty(appt.Notes) && appt.Notes.StartsWith($"The {patientName} visited for a consultation regarding"))
                        {
                            int idx = appt.Notes.IndexOf("regarding");
                            if (idx != -1)
                            {
                                string subject = appt.Notes[(idx + 9)..].Replace(".", "").Trim();
                                pastSubjects.Add(subject);
                            }
                        }
                    }
                }

                if (pastSubjects.Count > 0)
                {
                    return $"The {patientName} is currently scheduled for a consultation regarding {currentSubject.ToLower()}. Previously visited for {string.Join(", ", pastSubjects)}.";
                }
                else
                {
                    return $"The {patientName} is currently scheduled for a consultation regarding {currentSubject.ToLower()}. No previous records found.";
                }
            }
        }

        /// <summary>
        /// Method to update editor.
        /// </summary>
        /// <param name="appointment">The appointment.</param>
        /// <param name="selectedDate">The selected date.</param>
        internal void UpdateEditor(Appointment? appointment, DateTime selectedDate, bool isMonthView, bool isAllDayControl)
        {
            this.appointment = appointment;
            this.selectedDate = selectedDate;

            if (this.appointment != null)
            {
                AppointmentEditorModel.Subject = this.appointment.EventName;
                AppointmentEditorModel.Notes = this.appointment.Notes;
                AppointmentEditorModel.StartDate = this.appointment.From;
                AppointmentEditorModel.EndDate = this.appointment.To;
                AppointmentEditorModel.IsEditorEnabled = true;

                if (!this.appointment.IsAllDay)
                {
                    AppointmentEditorModel.StartTime = new TimeSpan(this.appointment.From.Hour, this.appointment.From.Minute, this.appointment.From.Second);
                    AppointmentEditorModel.EndTime = new TimeSpan(this.appointment.To.Hour, this.appointment.To.Minute, this.appointment.To.Second);
                    AppointmentEditorModel.IsAllDay = false;
                    AppointmentEditorModel.IsEditorEnabled = true;
                }
                else
                {
                    AppointmentEditorModel.StartTime = new TimeSpan(12, 0, 0);
                    AppointmentEditorModel.EndTime = new TimeSpan(12, 0, 0);
                    AppointmentEditorModel.IsEditorEnabled = false;
                    AppointmentEditorModel.IsAllDay = true;
                }
            }
            else
            {
                AppointmentEditorModel.Subject = "";
                AppointmentEditorModel.Notes = "";
                AppointmentEditorModel.StartDate = this.selectedDate;
                AppointmentEditorModel.EndDate = this.selectedDate;
                if (isMonthView || isAllDayControl)
                {
                    AppointmentEditorModel.IsAllDay = true;
                    AppointmentEditorModel.StartTime = new TimeSpan(this.selectedDate.Hour, this.selectedDate.Minute, this.selectedDate.Second);
                    AppointmentEditorModel.EndTime = new TimeSpan(this.selectedDate.Hour, this.selectedDate.Minute, this.selectedDate.Second);
                }
                else
                {
                    AppointmentEditorModel.IsAllDay = false;
                    AppointmentEditorModel.StartTime = new TimeSpan(this.selectedDate.Hour, this.selectedDate.Minute, this.selectedDate.Second);
                    AppointmentEditorModel.EndTime = new TimeSpan(this.selectedDate.Hour + 1, this.selectedDate.Minute, this.selectedDate.Second);
                }
            }
        }

        /// <summary>
        /// Method to add appointment details.
        /// </summary>
        private async void AddAppointmentDetails()
        {
            var endDate = AppointmentEditorModel.EndDate;
            var startDate = AppointmentEditorModel.StartDate;
            var endTime = AppointmentEditorModel.EndTime;
            var startTime = AppointmentEditorModel.StartTime;
            string eventName = AppointmentEditorModel.Subject;
            bool isAllDay = AppointmentEditorModel.IsAllDay;
            string notes = AppointmentEditorModel.Notes;

            var currentPage = Application.Current!.Windows.FirstOrDefault()?.Page;
            if (currentPage == null)
                return;


            if (!isAllDay)
            {
                if (endDate < startDate)
                {
                    await currentPage.DisplayAlert(string.Empty, "End date should be greater than start date.", "OK");
                    return;
                }

                if (endDate.Date == startDate.Date && endTime <= startTime)
                {
                    await currentPage.DisplayAlert(string.Empty, "End time should be greater than start time.", "OK");
                    return;
                }
            }
            else
            {
                if (endDate.Date < startDate.Date)
                {
                    await currentPage.DisplayAlert(string.Empty, "End date must be after start date.", "OK");
                    return;
                }
            }

            AppointmentDetails();
        }

        /// <summary>
        /// Method to appointment details.
        /// </summary>
        private void AppointmentDetails()
        {
            if (this.Events == null)
            {
                this.Events = new ObservableCollection<Appointment>();
            }

            bool isNew = false;

            if (appointment == null)
            {
                appointment = new Appointment();
                isNew = true;
            }

            if (string.IsNullOrEmpty(AppointmentEditorModel.Subject))
            {
                appointment.EventName = "(No Title)";
            }
            else
            {
                appointment.EventName = AppointmentEditorModel.Subject;
            }
            
            appointment.From = AppointmentEditorModel.StartDate.Date.Add(AppointmentEditorModel.StartTime);
            appointment.To = AppointmentEditorModel.EndDate.Date.Add(AppointmentEditorModel.EndTime);
            appointment.IsAllDay = AppointmentEditorModel.IsAllDay;
            appointment.Notes = AppointmentEditorModel.Notes;

            if (isNew)
            {
                this.Events.Add(appointment);
            }
            else
            {
                int index = this.Events.IndexOf(appointment);
                if (index >= 0)
                {
                    this.Events[index] = appointment;
                }
            }

            this.IsOpen = false;
        }

        /// <summary>
        /// Method to delete scheduler appointment.
        /// </summary>
        private void DeleteSchedulerAppointment()
        {
            if (appointment == null)
            {
                this.IsOpen = false;
                return;
            }

            //// Remove the appointments in the Scheduler.
            this.Events.Remove(this.appointment);
            
            this.IsOpen = false;
        }

        /// <summary>
        /// Method to cancel scheduler appointment.
        /// </summary>
        private void CancelSchedulerAppointment()
        {
            this.IsOpen = false;
        }
    }
}
