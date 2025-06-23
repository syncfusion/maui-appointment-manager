namespace ManageAppointments
{
    /// <summary>
    /// Represents an appointment in the dashboard.
    /// </summary>
    public class DashAppointment
    {
        /// <summary>
        /// Gets or sets the unique identifier for the appointment.
        /// </summary>
        public string ID { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the patient associated with the appointment.
        /// </summary>
        public string PatientName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the contact phone number of the patient.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time of the appointment.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the subject or purpose of the appointment.
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current status of the appointment (e.g., Confirmed, Pending, Cancelled).
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
