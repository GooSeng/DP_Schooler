using Schooler.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
    public class SettingPage : ContentPage
    {
        public SettingPage()
        {
            UserDao dao = new UserDao();
            var signOutBtn = new Button()
            {
                Text = "Sign out"
            };
            signOutBtn.Clicked += (sender, argv) =>
            {
               
                dao.SignOut();
                Navigation.PopAsync(false);
            };

            Title = "Setting";

            var setting = new TableView
            {
				Root = new TableRoot
                {
                    new TableSection("User Info")
                    {
                        new TextCell
						{
							Text = dao.GetLoginedUser()
                        }
					}
				}
			};
			Content = new StackLayout
            {
//                VerticalOptions = LayoutOptions.Center,

                Children = {
                    setting,
					signOutBtn
                }
            };
            Padding = new Thickness(10, 10, 10, 10);
        }
    }
}
