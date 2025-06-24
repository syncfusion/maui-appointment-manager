using Syncfusion.Maui.DataForm;
using Syncfusion.Maui.Toolkit.Buttons;

namespace ManageAppointments.Behaviors
{
    /// <summary>
    /// Behavior that handles the login functionality for a ContentPage.
    /// </summary>
    public class LoginBehavior : Behavior<ContentPage>
    {
        /// <summary>
        /// The login button used to trigger authentication.
        /// </summary>
        private SfButton? button;

        /// <summary>
        /// Gets or sets the login form used to collect user credentials.
        /// </summary>
        public SfDataForm? LoginForm { get; set; }

        /// <summary>
        /// Gets or sets the ProfileViewModel containing user login data.
        /// </summary>
        public ProfileViewModel? ProfileViewModel { get; set; }

        /// <summary>
        /// Attaches the behavior to the specified ContentPage and initializes event handlers.
        /// </summary>
        /// <param name="bindable">The ContentPage to which the behavior is attached.</param>
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable.BindingContext is ProfileViewModel)
            {
                ProfileViewModel = (ProfileViewModel)bindable.BindingContext;
            }

            this.LoginForm = bindable.FindByName<SfDataForm>("loginForm");
            this.button = bindable.FindByName<SfButton>("loginButton");
            
            if (button != null)
            {
                button.Clicked += OnButtonClicked;
            }
        }
       
        /// <summary>
        /// Detaches the behavior from the ContentPage and cleans up event handlers.
        /// </summary>
        /// <param name="bindable">The ContentPage from which the behavior is detached.</param>
        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);

            if (this.button != null)
            {
                this.button.Clicked -= OnButtonClicked;
                this.button = null;
            }
        }

        /// <summary>
        /// Handles the login button click event, validates user credentials, and navigates to the main page if successful.
        /// </summary>
        /// <param name="sender">The button triggering the event.</param>
        /// <param name="e">Event data.</param>
        private async void OnButtonClicked(object? sender, EventArgs e)
        {
            if (this.LoginForm != null)
            {
                LoginForm.Commit();

                var IsValid = LoginForm.Validate();
                if (IsValid)
                {
                    var mainWindow = App.Current?.Windows.FirstOrDefault();
                    var mainPage = mainWindow?.Page;

                    if (mainPage != null)
                    {
#if WINDOWS || MACCATALYST
                        await mainPage.Navigation.PushModalAsync(new MainPageDesktop());
#else
                        await mainPage.Navigation.PushModalAsync(new MainPage());
#endif
                    }
                }
            }
        }
    }
}
