using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
	public class ProjectPage : ContentPage
	{
		public ProjectPage()
		{
			Title = "Project";

			//			var nav = new NavigationPage(new Views.TodoListPage());

			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello Page" },
					new Views.TodoListView()
				}
			};
		}
	}
}
