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
			
			Content = new StackLayout
			{
				Children = {
					new Views.ProjectListView(),
				}
			};
		}
	}
}
