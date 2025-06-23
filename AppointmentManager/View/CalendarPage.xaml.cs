using Syncfusion.Maui.Core;
using Syncfusion.Maui.Scheduler;

namespace ManageAppointments
{
	public partial class CalendarPage : ContentView
	{
		public CalendarPage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// The scheduler double tapped event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void Scheduler_DoubleTapped(object sender, SchedulerDoubleTappedEventArgs e)
        {
            if (e.Element == SchedulerElement.Appointment)
            {
                if (this.BindingContext is SchedulerViewModel schedulerViewModel)
                {
                    Appointment? appointment = null;
                    DateTime selectedDate;

                    if (e.Appointments != null && e.Appointments.Count > 0)
                    {
                        appointment = (Appointment)e.Appointments[0];
                        selectedDate = appointment.From;
                    }
                    else
                    {
                        selectedDate = e.Date ?? DateTime.Now;
                    }

                    schedulerViewModel.UpdateEditor(appointment, selectedDate, Scheduler.View == SchedulerView.Month, e.Element == SchedulerElement.AllDayViewControl);

#if ANDROID || IOS
                sfPopup.PopupStyle.CornerRadius = 0;
                sfPopup.IsFullScreen = true;
#if IOS
                sfPopup.HeaderHeight = 100;
#endif
#endif
                    sfPopup.Show();
                }
            }
        }

        /// <summary>
        /// The popup closing event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void sfPopup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (endTimePicker != null)
            {
                endTimePicker.IsVisible = false;
            }

            if (startTimePicker != null)
            {
                startTimePicker.IsVisible = false;
            }

            if (endDatePicker != null)
            {
                endDatePicker.IsVisible = false;
            }

            if (startDatePicker != null)
            {
                startDatePicker.IsVisible = false;
            }
        }

        /// <summary>
        /// The tap gesture recognizer event for end time picker.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var tappedLabel = sender as SfTextInputLayout;
            if (tappedLabel != null)
            {
                endTimePicker.RelativeView = tappedLabel;
                endTimePicker.IsOpen = true;
                endTimePicker.IsVisible = true;
            }
        }

        /// <summary>
        /// The tap gesture recognizer event for start time picker.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
        {
            var tappedLabel = sender as SfTextInputLayout;
            if (tappedLabel != null)
            {
                startTimePicker.RelativeView = tappedLabel;
                startTimePicker.IsOpen = true;
                startTimePicker.IsVisible = true;
            }
        }

        /// <summary>
        /// The tap gesture recognizer event for end date picker.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TapGestureRecognizer_Tapped_2(object sender, TappedEventArgs e)
        {
            var tappedLabel = sender as SfTextInputLayout;
            if (tappedLabel != null)
            {
                endDatePicker.RelativeView = tappedLabel;
                endDatePicker.IsOpen = true;
                endDatePicker.IsVisible = true;
            }
        }

        /// <summary>
        /// The tap gesture recognizer event for start date picker.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TapGestureRecognizer_Tapped_3(object sender, TappedEventArgs e)
        {
            var tappedLabel = sender as SfTextInputLayout;
            if (tappedLabel != null)
            {
                startDatePicker.RelativeView = tappedLabel;
                startDatePicker.IsOpen = true;
                startDatePicker.IsVisible = true;
            }
        }

        /// <summary>
        /// The end date picker selection changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Thr cancel event args.</param>
        private void endDatePicker_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (endDatePicker != null)
            {
                endDatePicker.IsOpen = false;
            }
        }

        /// <summary>
        /// The start date picker selection changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Thr cancel event args.</param>
        private void startDatePicker_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
        {
            if (startDatePicker != null)
            {
                startDatePicker.IsOpen = false;
            }
        }

        /// <summary>
        /// The tap gesture recognizer event for cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void TapGestureRecognizer_Tapped_4(object sender, TappedEventArgs e)
        {
            if (sfPopup != null)
            {
                sfPopup.IsOpen = false;
            }
        }

        /// <summary>
        /// The all day switch state changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The state changed event args.</param>
        private void switchAllDay_StateChanged(object sender, Syncfusion.Maui.Buttons.StateChangedEventArgs e)
        {
            if (this.BindingContext is SchedulerViewModel schedulerViewModel)
            {
                var appointmentEditorModel = schedulerViewModel.AppointmentEditorModel;

                if (appointmentEditorModel == null)
                {
                    return;
                }

                if (appointmentEditorModel.IsAllDay)
                {
                    appointmentEditorModel.StartTime = new TimeSpan(0, 0, 0);
                    appointmentEditorModel.IsEditorEnabled = false;
                    appointmentEditorModel.EndTime = new TimeSpan(0, 0, 0);
                }
                else
                {
                    appointmentEditorModel.IsEditorEnabled = true;
                }
            }
        }
    }
}