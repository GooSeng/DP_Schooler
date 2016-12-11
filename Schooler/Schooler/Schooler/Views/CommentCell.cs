using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class CommentCell : ViewCell
	{
		public CommentCell()
		{
			var uploaderLb = new Label();
			uploaderLb.SetBinding(Label.TextProperty, "uploadUser");

			var contentLb = new Label();
			contentLb.SetBinding(Label.TextProperty, "contents");

			var deleteBtn = new Button { Text = "-" };
			deleteBtn.Clicked += DeleteBtn_Clicked;

			View = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					uploaderLb, contentLb, deleteBtn
				}
			};
		}

		private void DeleteBtn_Clicked(object sender, EventArgs e)
		{
			// Todo: Comment delete
		}
	}
}
