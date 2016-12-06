using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Children.Add(new SchedulePage());
            Children.Add(new ProjectPage());
            Children.Add(new TodoPage());
            Children.Add(new SettingPage());
        }
    }
}
