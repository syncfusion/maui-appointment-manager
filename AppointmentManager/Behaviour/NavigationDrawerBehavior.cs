using Syncfusion.Maui.Toolkit.NavigationDrawer;
using MauiListView = Microsoft.Maui.Controls.ListView;

namespace ManageAppointments.Behaviors
{
    /// <summary>
    /// Behavior that manages the navigation drawer functionality for a ContentPage.
    /// It handles menu button clicks, navigation item selection, and page transitions.
    /// </summary>
    public class NavigationDrawerBehavior : Behavior<ContentPage>
    {
        /// <summary>
        /// The navigation drawer component used for side menu navigation.
        /// </summary>
        private SfNavigationDrawer? navigationDrawer;

        /// <summary>
        /// The ListView that holds the navigation menu items.
        /// </summary>
        private MauiListView? listView;

        /// <summary>
        /// The main content view where selected pages will be displayed.
        /// </summary>
        private ContentView? mainContentView;

        /// <summary>
        /// The menu button that toggles the navigation drawer.
        /// </summary>
        private Label? menuButton;

        /// <summary>
        /// The recognizer holds tap gesture for menu button. 
        /// </summary>
        private TapGestureRecognizer? menuTapGestureRecognizer;

        /// <summary>
        /// Attaches the behavior to the specified ContentPage and initializes UI elements.
        /// </summary>
        /// <param name="bindable">The ContentPage to which the behavior is attached.</param>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            // Find UI elements from MainPage
            navigationDrawer = bindable.FindByName<SfNavigationDrawer>("navigationDrawer");
            listView = bindable.FindByName<MauiListView>("listView");
            mainContentView = bindable.FindByName<ContentView>("mainContentView");
            menuButton = bindable.FindByName<Label>("menuButton"); // Find the ImageButton

            if (menuButton != null)
            {
                menuTapGestureRecognizer = new TapGestureRecognizer();
                menuTapGestureRecognizer.Tapped += OnMenuClicked;
                menuButton.GestureRecognizers.Add(menuTapGestureRecognizer);
            }

            // Set navigation items
            listView.ItemsSource = new List<CustomNavigationItem>
            {
                new CustomNavigationItem { Title = "Dashboard", Glyph = "\uE760" },
                new CustomNavigationItem { Title = "Calendar", Glyph = "\uE758" },
                new CustomNavigationItem { Title = "Appointments", Glyph = "\uE7B0"  },
                new CustomNavigationItem { Title = "Log out", Glyph = "\uE7FB" }
            };

            // Attach event handlers
            listView.ItemSelected += OnListItemSelected;
        }

        /// <summary>
        /// Detaches the behavior from the ContentPage and cleans up event handlers.
        /// </summary>
        /// <param name="bindable">The ContentPage from which the behavior is detached.</param>
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            if (menuButton != null && menuTapGestureRecognizer != null)
            {
                menuTapGestureRecognizer.Tapped -= OnMenuClicked;
                menuButton.GestureRecognizers.Remove(menuTapGestureRecognizer);
            }


            if (listView != null)
            {
                listView.ItemSelected -= OnListItemSelected;
            }
        }

        /// <summary>
        /// Handles the menu button click event to toggle the navigation drawer.
        /// </summary>
        /// <param name="sender">The menu button triggering the event.</param>
        /// <param name="e">Event data.</param>
        private void OnMenuClicked(object? sender, EventArgs e)
        {
            if (navigationDrawer != null)
                navigationDrawer.IsOpen = !navigationDrawer.IsOpen;
        }

        /// <summary>
        /// Handles the navigation item selection and updates the main content accordingly.
        /// </summary>
        /// <param name="sender">The ListView containing the menu items.</param>
        /// <param name="e">Event data for the selected item.</param>
        private void OnListItemSelected(object? sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var selectedItem = (CustomNavigationItem)e.SelectedItem;

            if (mainContentView == null || listView == null || navigationDrawer == null )
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
            navigationDrawer.IsOpen = false;
            listView.SelectedItem = null;
        }
    }
}
