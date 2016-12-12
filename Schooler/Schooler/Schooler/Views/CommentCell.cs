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
		public static readonly BindableProperty idxProperty = 
			BindableProperty.Create("idx", typeof(int), typeof(CommentCell));
		public int idx
		{
			get
			{
				return (int)base.GetValue(CommentCell.idxProperty);
			}
			set
			{
				base.SetValue(CommentCell.idxProperty, value);
			}
		}

		public CommentCell()
		{

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
