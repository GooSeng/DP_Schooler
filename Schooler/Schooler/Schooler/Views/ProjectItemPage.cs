using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class ProjectItemPage : ContentPage
	{
		public ProjectItemPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello Page" }
				}
			};
		}
	}
}
