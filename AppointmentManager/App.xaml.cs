using Microsoft.Maui.Controls;
using System.Linq;
using Microsoft.Maui.ApplicationModel;
using Syncfusion.Maui.Themes;

namespace ManageAppointments
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            this.RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            // Update the theme based on the requested theme change (light or dark)
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current!.Resources.MergedDictionaries;
            var theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();

            if (theme != null)
            {
                if (e.RequestedTheme == AppTheme.Dark)
                {
                    // Set the visual theme to MaterialDark for dark mode
                    theme.VisualTheme = SfVisuals.MaterialDark;
                }
                else
                {
                    // Set the visual theme to MaterialLight for light mode
                    theme.VisualTheme = SfVisuals.MaterialLight;
                }
            }
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new LoginPage());
        }
    }
}
