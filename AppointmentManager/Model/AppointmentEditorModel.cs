using System.ComponentModel;

namespace ManageAppointments
{
    /// <summary>
    /// Represents an appointment editor.
    /// </summary>
    public class AppointmentEditorModel : INotifyPropertyChanged
    {
        private string subject = string.Empty, notes = string.Empty;
        private TimeSpan startTime, endTime;
        private bool isAllDay, isEditorEnabled = true;
        private DateTime startDate, endDate;

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                RaisePropertyChanged(nameof(this.Subject));
            }
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                RaisePropertyChanged(nameof(this.Notes));
            }
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public TimeSpan StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                RaisePropertyChanged(nameof(this.StartTime));
            }
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public TimeSpan EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                RaisePropertyChanged(nameof(this.EndTime));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating all day or not.
        /// </summary>
        public bool IsAllDay
        {
            get { return isAllDay; }
            set
            {
                isAllDay = value;
                RaisePropertyChanged(nameof(this.IsAllDay));
            }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                RaisePropertyChanged(nameof(this.StartDate));
            }
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                RaisePropertyChanged(nameof(this.EndDate));
            }
        }

        /// <summary>
        /// Gets or sets the value indicating editor is enable or not.
        /// </summary>
        public bool IsEditorEnabled
        {
            get { return isEditorEnabled; }
            set
            {
                isEditorEnabled = value;
                RaisePropertyChanged(nameof(this.IsEditorEnabled));
            }
        }

        /// <summary>
        /// The property changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Method to raise property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
