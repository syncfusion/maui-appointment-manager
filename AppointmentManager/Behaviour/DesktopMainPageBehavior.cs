using Syncfusion.Maui.ListView;

namespace ManageAppointments.Behaviors
{
    public class DesktopMainPageBehavior : Behavior<ContentPage>
    {
        /// <summary>
        /// The gird that holds the dash board.
        /// </summary>
        private Grid? grid;

        /// <summary>
        /// The ListView that holds the navigation menu items.
        /// </summary>
        private SfListView? listView;

        /// <summary>
        /// The main content view where selected pages will be displayed.
        /// </summary>
        private ContentView? mainContentView;

        /// <summary>
        /// Attaches the behavior to the specified ContentPage and initializes UI elements.
        /// </summary>
        /// <param name="bindable">The ContentPage to which the behavior is attached.</param>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            // Find UI elements from MainPage
            grid = bindable.FindByName<Grid>("dashBoardGrid");
            listView = bindable.FindByName<SfListView>("listViewWindows");
            mainContentView = bindable.FindByName<ContentView>("mainContentView");

            // Set navigation items
            listView.ItemsSource = new List<CustomNavigationItem>
            {
                new CustomNavigationItem { Title = "Dashboard", Glyph = "\uE760" },
                new CustomNavigationItem { Title = "Calendar", Glyph = "\uE758" },
                new CustomNavigationItem { Title = "Appointments", Glyph = "\uE7B0"  },
                new CustomNavigationItem { Title = "Log out", Glyph = "\uE7FB" }
            };

            // Attach event handlers
            listView.SelectionChanged += ListView_SelectionChanged;
        }

        /// <summary>
        /// The list view selection changed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event args.</param>
        private void ListView_SelectionChanged(object? sender, ItemSelectionChangedEventArgs e)
        {
            if (e.AddedItems == null)
                return;

            var selectedItem = e.AddedItems.FirstOrDefault() as CustomNavigationItem;
            if (selectedItem == null)
            {
                return;
            }

            if (mainContentView == null || listView == null)
            {
                return;
            }

            if (selectedItem.Title == "Dashboard")
            {
                mainContentView.Content = new DashBoard();
            }
            else if (selectedItem.Title == "Calendar")
            {
                mainContentView.Content = new CalendarPage();
            }
            else if (selectedItem.Title == "Appointments")
            {
                mainContentView.Content = new AppointmentsPage();
            }
            else if (selectedItem.Title == "Log out")
            {
                // Reset navigation to Login Page
                App.Current?.Windows[0].Navigation.PopModalAsync(true);
            }

            // Reset selection (Prevents item from staying highlighted)
            listView.SelectedItem = selectedItem;
        }

        /// <summary>
        /// Detaches the behavior from the ContentPage and cleans up event handlers.
        /// </summary>
        /// <param name="bindable">The ContentPage from which the behavior is detached.</param>
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            if (listView != null)
            {
                listView.SelectionChanged -= ListView_SelectionChanged;
            }
        }
    }
}
