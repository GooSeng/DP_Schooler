using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class CalenderItemCell : ContentView
	{
		public DateTime date;
		public CalenderItemCell(Class.Schedule schedule)
		{
			var label = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Center,
				Text = schedule.day.Day.ToString()
			};

//			label.SetBinding(Label.TextProperty, "date");

			var box = new BoxView
			{
				BackgroundColor = Color.Lime,
				HeightRequest = 5,
				WidthRequest = 5,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			box.SetBinding(BoxView.IsVisibleProperty, "Visible");

			var layout = new StackLayout
			{
				Children = { label, box }
			};
//			View = layout;
			Content = layout;
//			Content = new Label { Text = "Hello View" };
		}
	}
}
