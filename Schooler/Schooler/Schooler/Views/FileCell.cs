using Schooler.Class;
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
        public static readonly BindableProperty idxProperty =
            BindableProperty.Create("idx", typeof(int), typeof(FileCell), -1);
        public int idx
        {
            get
            {
                return (int)base.GetValue(FileCell.idxProperty);
            }
            set
            {
                base.SetValue(FileCell.idxProperty, value);
            }
        }
        Label nameLbl;
        public FileCell()
		{
			nameLbl = new Label();
			nameLbl.SetBinding(Label.TextProperty, "name");
            
            var dateLbl = new Label();
			dateLbl.SetBinding(Label.TextProperty, "uploadUserId");


            var deleteBtn = new Button { Text = "-" };
            deleteBtn.Clicked += DeleteBtn_Clicked;
            var downBtn = new Button { Text = "down" };
            downBtn.Clicked += DownBtn_Clicked;
            downBtn.TextColor = Color.White;
            downBtn.BackgroundColor = Color.Blue;

            View = new StackLayout
            {
                
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					nameLbl, dateLbl, deleteBtn, downBtn
				}
			};
		}

        private void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            AssignmentDao dao = new AssignmentDao(-1);
            dao.DeleteFile(idx);
        }
        private void DownBtn_Clicked(object sender, EventArgs e)
        {
            AssignmentDao dao = new AssignmentDao(-1);
            dao.DownloadFile(idx, nameLbl.Text);
        }
    }
}
