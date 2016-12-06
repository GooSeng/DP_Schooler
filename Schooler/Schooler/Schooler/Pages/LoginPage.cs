using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var loginBtn = new Button() {
                Text = "Sign In"
            };
            loginBtn.Clicked += LoginBtn_Clicked;

            var joinBtn = new Button() {
                Text = "Sign out"
            };
            joinBtn.Clicked += joinBtn_Clicked;
            
            var id = new Entry()
            {
                Placeholder = "ID"
            };
            var pw = new Entry()
            {
                Placeholder = "Password",
                IsPassword = true
            };

            Title = "Schooler";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,

                Children = {
                    new Label {
                        Text = "Login Page",
                        HorizontalOptions = LayoutOptions.Center,
                   },
                    id,
                    pw,
                    loginBtn,
                    joinBtn
                }
            };

            Padding = new Thickness(10, 10, 10, 10);
            
        }

        private void joinBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {
            // 로긴처리
            //App.Current.MainPage = new MainPage();
            this.Navigation.PushAsync(new MainPage());
        }
    }
}
