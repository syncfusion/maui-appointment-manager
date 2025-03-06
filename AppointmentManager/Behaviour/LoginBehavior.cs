using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
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
        private SfButton button;

        /// <summary>
        /// The predefined valid email for authentication.
        /// </summary>
        private const string ValidEmail = "admin@gmail.com";

        /// <summary>
        /// The predefined valid password for authentication.
        /// </summary>
        private const string ValidPassword = "12345678";

        /// <summary>
        /// Gets or sets the login form used to collect user credentials.
        /// </summary>
        public SfDataForm LoginForm { get; set; }

        /// <summary>
        /// Gets or sets the ProfileViewModel containing user login data.
        /// </summary>
        public ProfileViewModel ProfileViewModel { get; set; }

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
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            LoginForm.Commit();

            var enteredEmail = ProfileViewModel.LoginFormModel.Email;
            var enteredPassword = ProfileViewModel.LoginFormModel.Password;

            bool isValid = ValidateCredentials(enteredEmail, enteredPassword);

            if (isValid)
            {
                await App.Current.Windows[0].Page.Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Login Failed", "Invalid email or password", "OK");
            }
        }

        /// <summary>
        /// Validates the entered email and password against predefined credentials.
        /// </summary>
        /// <param name="email">The email entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        /// <returns>True if credentials are valid; otherwise, false.</returns>
        private bool ValidateCredentials(string email, string password)
        {
            return email == ValidEmail && password == ValidPassword;
        }

        /// <summary>
        /// Displays an alert dialog to the user.
        /// </summary>
        /// <param name="title">The title of the alert dialog.</param>
        /// <param name="message">The message to display.</param>
        /// <param name="cancel">The text for the cancel button.</param>
        /// <returns>A task representing the asynchronous alert display operation.</returns>
        private Task DisplayAlert(string title, string message, string cancel)
        {
            return App.Current.Windows[0].Page.DisplayAlert(title, message, cancel);
        }
    }
}
