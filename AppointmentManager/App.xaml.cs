using System.Reflection;
using Syncfusion.Maui.Themes;

namespace ManageAppointments
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ApplyTheme(Application.Current!.RequestedTheme);
            this.RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            // Update the theme based on the requested theme change (light or dark)
            ApplyTheme(e.RequestedTheme);
        }

        private void ApplyTheme(AppTheme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current!.Resources.MergedDictionaries;
            var syncfusionTheme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();

            if (syncfusionTheme != null)
            {
                if (theme == AppTheme.Dark || theme == AppTheme.Unspecified)
                {
                    // Set the visual theme to MaterialDark for dark mode
                    syncfusionTheme.VisualTheme = SfVisuals.MaterialDark;
                }
                else
                {
                    // Set the visual theme to MaterialLight for light mode
                    syncfusionTheme.VisualTheme = SfVisuals.MaterialLight;
                }
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new LoginPage());
        }
    }
}