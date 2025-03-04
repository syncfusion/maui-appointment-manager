using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Syncfusion.Maui.TabView;
using Syncfusion.Maui.Scheduler;

namespace ManageAppointments.Behaviors
{
    /// <summary>
    /// A behavior class that manages the appointment filtering logic 
    /// </summary>
    public class AppointmentBehavior : Behavior<ContentView>
    {
        /// <summary>
        /// Reference to the <see cref="SfTabView"/> used to switch between upcoming and past appointments.
        /// </summary>
        private SfTabView tabView;

        /// <summary>
        /// ViewModel that contains appointment data for the scheduler.
        /// </summary>
        public SchedulerViewModel viewModel { get; set; }

        /// <summary>
        /// Called when the behavior is attached to a <see cref="ContentView"/>.
        /// </summary>
        /// <param name="bindable">The ContentView to which the behavior is attached.</param>
        protected override void OnAttachedTo(ContentView bindable)
        {
            base.OnAttachedTo(bindable);

            // Set ViewModel if the BindingContext is of type SchedulerViewModel
            if (bindable.BindingContext is SchedulerViewModel schedulerViewModel)
            {
                viewModel = schedulerViewModel;
            }

            // Find the TabView within the ContentView
            this.tabView = bindable.FindByName<SfTabView>("AppointmentsTabView");

            // Initialize the scheduler with upcoming appointments
            UpdateSchedulerAppointments(tabView, 0);

            // Subscribe to the tab selection change event
            tabView.SelectionChanged += OnTabSelectionChanged;
        }

        /// <summary>
        /// Called when the behavior is detached from a <see cref="ContentView"/>.
        /// </summary>
        /// <param name="bindable">The ContentView from which the behavior is detached.</param>
        protected override void OnDetachingFrom(ContentView bindable)
        {
            base.OnDetachingFrom(bindable);

            // Unsubscribe from the event to avoid memory leaks
            tabView.SelectionChanged -= OnTabSelectionChanged;
            tabView = null;
        }

        /// <summary>
        /// Handles tab selection changes and updates the scheduler appointments accordingly.
        /// </summary>
        /// <param name="sender">The source of the event, an <see cref="SfTabView"/>.</param>
        /// <param name="e">Event arguments containing the new selected tab index.</param>
        private void OnTabSelectionChanged(object sender, TabSelectionChangedEventArgs e)
        {
            if (sender is SfTabView tabView)
            {
                UpdateSchedulerAppointments(tabView, e.NewIndex);
            }
        }

        /// <summary>
        /// Updates the <see cref="SfScheduler"/> appointments based on the selected tab index.
        /// </summary>
        /// <param name="tabView">The <see cref="SfTabView"/> containing the scheduler.</param>
        /// <param name="tabIndex">The selected tab index (0 for upcoming, 1 for past appointments).</param>
        private void UpdateSchedulerAppointments(SfTabView tabView, double tabIndex)
        {
            if (viewModel == null || viewModel.Events == null)
                return;

            // Find the scheduler control inside the tab view
            var scheduler = tabView.FindByName<SfScheduler>("Scheduler");
            if (scheduler == null) return;

            ObservableCollection<Appointment> filteredAppointments;

            // Filter appointments based on the selected tab
            if (tabIndex == 0) // Upcoming appointments
            {
                filteredAppointments = new ObservableCollection<Appointment>(
                    viewModel.Events.OfType<Appointment>().Where(x => x.From >= DateTime.Now));
            }
            else // Past appointments
            {
                filteredAppointments = new ObservableCollection<Appointment>(
                    viewModel.Events.OfType<Appointment>().Where(x => x.From < DateTime.Now));
            }

            // Update the scheduler's data source
            scheduler.AppointmentsSource = filteredAppointments;

            // Set scheduler date range based on selected tab
            scheduler.MinimumDateTime = tabIndex == 0 ? DateTime.Now : DateTime.Now.AddMonths(-1);
            scheduler.MaximumDateTime = tabIndex == 0 ? DateTime.Now.AddMonths(1) : DateTime.Now;
        }
    }
}
