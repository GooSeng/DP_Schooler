using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class FileCell : ViewCell
	{
		public FileCell()
		{
			var nameLbl = new Label();
			nameLbl.SetBinding(Label.TextProperty, "fileName");

			var dateLbl = new Label();
			dateLbl.SetBinding(Label.TextProperty, "uploadTime");

			var deleteBtn = new Button { Text = "-" };
			deleteBtn.Clicked += DeleteBtn_Clicked;

			View = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					nameLbl, dateLbl, deleteBtn
				}
			};
		}

		private void DeleteBtn_Clicked(object sender, EventArgs e)
		{
			// Todo: File delete
		}
	}
}
