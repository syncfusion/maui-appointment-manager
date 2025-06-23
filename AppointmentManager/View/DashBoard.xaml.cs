namespace ManageAppointments
{
    public partial class DashBoard : ContentView
    {
        public DashBoard()
        {
            InitializeComponent();
            var schedulerVM = new SchedulerViewModel();
            this.BindingContext = schedulerVM.DashboardVM;
        }
        
    }
}