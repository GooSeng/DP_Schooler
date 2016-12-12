using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Schooler.Class;

namespace Schooler.Views
{
	public class CalenderItemCell : ContentView
	{
		public CalenderItemCell(int day, bool isSchduled)
		{
			var label = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Center,
				Text = day.ToString()
			};

			var box = new BoxView
			{
				BackgroundColor = Color.Lime,
				HeightRequest = 5,
				WidthRequest = 5,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				IsVisible = isSchduled
			};
//			box.SetBinding(BoxView.IsVisibleProperty, "Visible");

			var layout = new StackLayout
			{
				Children = { label, box }
			};

			Content = layout;
		}
		
	}
}
