namespace ManageAppointments
{
    /// <summary>
    /// ViewModel for managing user profile-related data.
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// Gets or sets the login form model.
        /// </summary>
        public LoginFormModel LoginFormModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileViewModel"/> class.
        /// </summary>
        public ProfileViewModel()
        {
            this.LoginFormModel = new LoginFormModel();
        }
    }
}
