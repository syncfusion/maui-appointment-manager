namespace ManageAppointments;

public partial class MainPageDesktop : ContentPage
{
	public MainPageDesktop()
	{
		InitializeComponent();
	}

#if WINDOWS || MACCATALYST
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (App.Current == null || App.Current.Windows[0] == null || App.Current.Windows.Count == 0)
        {
            return;
        }

#if WINDOWS
        App.Current.Windows[0].MinimumWidth = 1300;
        App.Current.Windows[0].MinimumHeight = 650;
#else
        App.Current.Windows[0].MinimumWidth = 1500;
        App.Current.Windows[0].MinimumHeight = 850;
#endif
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (App.Current == null || App.Current.Windows[0] == null || App.Current.Windows.Count == 0)
        {
            return;
        }

        App.Current.Windows[0].MinimumWidth = 0;
        App.Current.Windows[0].MinimumHeight = 0;
    }
#endif
}