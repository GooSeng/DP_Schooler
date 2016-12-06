using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler
{
    public class login_page : ContentPage
    {
        public login_page()
        { 
            var loginBtn = new Button();
            loginBtn.Text = "Sign In";
            loginBtn.Clicked += LoginBtn_Clicked;

            var signupBtn = new Button() {
                Text = "Sign Up"
            };
            signupBtn.Clicked += SignupBtn_Clicked;

            var idLb = new Label()
            {
                Text = "ID"
            };
            var id = new Entry()
            {
                Placeholder = "ID"
            };
            var pw = new Entry()
            {
                Placeholder = "Password",
                IsPassword = true
            };
           
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Login Page",

                         HorizontalOptions = LayoutOptions.Center
                    },
                    id,
                    pw,
                    loginBtn,
                    signupBtn

          
                }
            };

            Padding = new Thickness(10, 10, 10, 10);
            
        }

        private void SignupBtn_Clicked(object sender, EventArgs e)
        {
        }

        private void LoginBtn_Clicked(object sender, EventArgs e)
        {

        }
    }
}
