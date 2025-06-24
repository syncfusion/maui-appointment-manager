using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace ManageAppointments
{
    /// <summary>
    /// Represents the login form model containing user credentials.
    /// </summary>
    public class LoginFormModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the email address of the admin.
        /// </summary>
        private string _email = "doctor123@gmail.com";

        /// <summary>
        /// Stores the password of the admin.
        /// </summary>
        private string _password = "12345678";


        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <remarks>
        /// The email must be in a valid format, e.g., "example@mail.com".
        /// </remarks>
        [Display(Prompt = "example@mail.com", Name = "Email")]
        [EmailAddress(ErrorMessage = "Enter a valid email - example@mail.com")]
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        /// <remarks>
        /// The password must be exactly 8 characters long.
        /// </remarks>
        [Display(Prompt = "Enter your password", Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password length must be equal or greater than 8 characters")]
        [Required(ErrorMessage = "Enter a valid password")]
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies subscribers about property value changes.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
