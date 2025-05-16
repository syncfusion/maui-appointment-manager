using Microsoft.Maui.Controls;
using Syncfusion.Maui.Scheduler;
using System;
using System.ComponentModel;

namespace ManageAppointments
{
    /// <summary>
    /// Represents an appointment in the scheduler.
    /// </summary>
    public class Appointment : INotifyPropertyChanged
    {
        /// <summary>
        /// The start date and time of the appointment.
        /// </summary>
        private DateTime _from;

        /// <summary>
        /// The end date and time of the appointment.
        /// </summary>
        private DateTime _to;

        /// <summary>
        /// Indicates whether the appointment is an all-day event.
        /// </summary>
        private bool _isAllDay;

        /// <summary>
        /// The subject or title of the appointment.
        /// </summary>
        private string _eventName;

        /// <summary>
        /// The background color of the appointment.
        /// </summary>
        private Brush _background;

        /// <summary>
        /// The location of the appointment.
        /// </summary>
        private string _location;

        /// <summary>
        /// Initializes a new instance of the <see cref="Appointment"/> class.
        /// </summary>
        public Appointment()
        {
            _from = DateTime.Now;
            _to = DateTime.Now;
            _eventName = string.Empty;
            _isAllDay = false;
            _background = Brush.Transparent;
            _location = string.Empty;
        }

        /// <summary>
        /// Gets or sets the start date and time of the appointment.
        /// </summary>
        public DateTime From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        /// <summary>
        /// Gets or sets the end date and time of the appointment.
        /// </summary>
        public DateTime To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the appointment is an all-day event.
        /// </summary>
        public bool IsAllDay
        {
            get { return _isAllDay; }
            set
            {
                _isAllDay = value;
                OnPropertyChanged(nameof(IsAllDay));
            }
        }

        /// <summary>
        /// Gets or sets the subject or title of the appointment.
        /// </summary>
        public string EventName
        {
            get { return _eventName; }
            set
            {
                _eventName = value;
                OnPropertyChanged(nameof(EventName));
            }
        }

        /// <summary>
        /// Gets or sets the background color of the appointment.
        /// </summary>
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        /// <summary>
        /// Gets or sets the location of the appointment.
        /// </summary>
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event to notify the UI of property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
