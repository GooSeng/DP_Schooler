using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class TodoItemCell : ViewCell
	{
		public TodoItemCell()
		{
			var label = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			label.SetBinding(Label.TextProperty, "Content");

			var typeLbl = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				Text = " is Essencial",
				TextColor = Color.Silver
			};
			typeLbl.SetBinding(Label.IsVisibleProperty, "isEssencial");

			var layout = new StackLayout
			{
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label, typeLbl }
			};

			View = layout;
		}
	}
}
