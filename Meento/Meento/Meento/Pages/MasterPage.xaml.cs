using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Meento.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        private void HomeTap_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new HomePage());
            IsPresented = false;
        }

        private void PasswordTap_Tapped(object sender, EventArgs e)
        {
             Detail = new NavigationPage(new ChangePasswordPage());
               IsPresented = false;
        }

        private void TutorTap_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new BecomeTutorPage());
            IsPresented = false;
        }

        private void FaqsTap_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new FaqPage());
            IsPresented = false;
        }

        private void LogoutTap_Tapped(object sender, EventArgs e)
        {
            Preferences.Set("useremail", string.Empty);
            Preferences.Set("password", string.Empty);
            Preferences.Set("accesstoken", string.Empty);
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        /*   private void BtnHome_Clicked(object sender, EventArgs e)
           {
               Detail = new NavigationPage(new HomePage());
               IsPresented = false;
           }

           private void BtnChangePassword_Clicked(object sender, EventArgs e)
           {
               Detail = new NavigationPage(new ChangePasswordPage());
               IsPresented = false;
           }*/
    }
}