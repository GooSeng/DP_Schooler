using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class AssignmentItemCell : ViewCell
	{
		public AssignmentItemCell()
		{
			var label = new Label
			{
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};

			label.SetBinding(Label.TextProperty, "name");
			
			var layout = new StackLayout
			{
				Padding = new Thickness(20, 0, 20, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = { label }
			};

			View = layout;
		}
	}
}
