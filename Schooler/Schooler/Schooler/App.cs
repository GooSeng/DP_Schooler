using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Schooler.Pages;
using Schooler.Class;

namespace Schooler
{
    public class App : Application
    {
        public App()
        {
            AssignmentDao ad = new AssignmentDao(9);
            ad.GetCommentList();
            Class.UserDao d = new Class.UserDao();

			Class.UserDao dao = new Class.UserDao();
			dao.SignIn("id05", "password05");
			//MainPage = new NavigationPage(new LoginPage());
			MainPage = new NavigationPage(new MainPage());
//            MainPage = new NavigationPage(new LoginPage());
			   //         MainPage = new LoginPage();
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
