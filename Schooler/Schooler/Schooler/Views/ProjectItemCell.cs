using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class ProjectItemCell : ViewCell
	{
		public ProjectItemCell()
		{
			var nameLbl = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			nameLbl.SetBinding(Label.TextProperty, "name");

			var typeLbl = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				Text = " is Team Project",
				TextColor = Color.Silver
			};
			typeLbl.SetBinding(Label.IsVisibleProperty, "isTeam");

			var layout = new StackLayout
			{
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { nameLbl, typeLbl }
			};

			View = layout;
		}
	}
}
