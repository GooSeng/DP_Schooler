using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class TeamCell : ViewCell
	{
		public TeamCell()
		{
			var nameLbl = new Label();
			nameLbl.SetBinding(Label.TextProperty, "String");
			
			View = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					nameLbl
				}
			};
		}
	}
}
