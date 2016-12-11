using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
	public class AssignmentPage : ContentPage
	{
		public AssignmentPage()
		{
			Title = "Assignment";
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello Page" }
				}
			};
		}
	}
}
