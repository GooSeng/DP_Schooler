using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class ScheduleCell : ViewCell
	{
		public ScheduleCell()
		{
			var name = new Label();
			name.SetBinding(Label.TextProperty, "name");

			var place = new Label();
			place.TextColor = Color.Silver;
			place.SetBinding(Label.TextProperty, "place");
			
			View = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Margin = 5,
				Children =
				{
					name, place
				}
			};
		}
	}
}
