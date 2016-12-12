using Schooler.Class;
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
        int idx;
		public CommentCell()
		{
            //idx에가다 값만 들어가면댐

            var uploaderLb = new Label();
			uploaderLb.SetBinding(Label.TextProperty, "uploadUserId");

			var contentLb = new Label();
			contentLb.SetBinding(Label.TextProperty, "comment");

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
            AssignmentDao dao = new AssignmentDao(-1);
            dao.DeleteComment(idx);
			// Todo: Comment delete
		}
	}
}
