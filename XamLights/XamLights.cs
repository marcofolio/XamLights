using System;

using Xamarin.Forms;
using XamLights.Views;

namespace XamLights
{
    public class App : Application
    {
        public App()
        {
            // Game based on
            // https://en.wikipedia.org/wiki/Lights_Out_(game)
            // https://marcofolio.net/lights-off-html-puzzle/
            // https://marcofolio.net/xamarin-forms-animated-profile-cards/

            MainPage = new NavigationPage(new GamePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
