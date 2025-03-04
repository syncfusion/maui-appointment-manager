using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace ManageAppointments
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}