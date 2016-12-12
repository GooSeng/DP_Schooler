using Schooler.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class AssignmentItemPage : ContentPage
	{
		AssignmentDao dao;
		int idx;

		Label nameLbl;
		Entry nameEntry;

		Label deadlineLbl;
		DatePicker deadlinePicker;

		ListView fileList;
		Button fileAddBtn;
		StackLayout fileLayout;

		ListView commentList;
		Button commentAddBtn;
		Entry commentEntry;
		StackLayout commentLayout;

		Button saveBtn;
		Button cancelBtn;
		Button deleteBtn;

		StackLayout layout;


		public AssignmentItemPage(int _idx)
		{
			NavigationPage.SetHasNavigationBar(this, true);
			idx = _idx;
			dao = new AssignmentDao(idx);

			// Set Title
			if (idx < 0)
			{
				Title = "Add Assignment";
			}
			else
			{
				Title = "Edit Assingment: ";
			}

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			// Assignment Class Member variable
			nameLbl = new Label { Text = "Name" };
			nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "name");

			deadlineLbl = new Label { Text = "Deadline" };
			deadlinePicker = new DatePicker();
			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

			// File List 
			fileList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Views.FileCell))
			};
			fileList.SetBinding(ListView.ItemsSourceProperty, "fileList");
			fileAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30, Margin = 0 };
			fileAddBtn.Clicked += FileAddBtn_Clicked;

			fileLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							new Label { Text = "File list" },
							fileAddBtn
						}
					},
					fileList,
				}
			};

			// Comment List
			commentList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Views.CommentCell))
			};
			commentList.SetBinding(ListView.ItemsSourceProperty, "commentList");
			commentAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30 };
			commentEntry = new Entry { WidthRequest = 300 };
			commentAddBtn.Clicked += CommentAddBtn_Clicked;

			commentLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					new Label { Text = "Comment list" },
					new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							commentEntry,
							commentAddBtn
						}
					},
					commentList,
				}
			};

			// Buttons
			saveBtn = new Button { Text = "Save" };
			saveBtn.Clicked += SaveBtn_Clicked;

			deleteBtn = new Button { Text = "Delete" };
			deleteBtn.Clicked += DeleteBtn_Clicked;

			cancelBtn = new Button { Text = "Cancel" };
			cancelBtn.Clicked += CancelBtn_Clicked;

			layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
				}
			};
			if (idx != -1)
			{
				layout.Children.Add(fileLayout);
				layout.Children.Add(commentLayout);
			}
			layout.Children.Add(saveBtn);
			layout.Children.Add(cancelBtn);
			if (idx != -1)
			{
				layout.Children.Add(deleteBtn);
			}

			Content = layout;
		}
		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			Content = null;
		}

		private void CommentAddBtn_Clicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private async void CancelBtn_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		private async void DeleteBtn_Clicked(object sender, EventArgs e)
		{
			UserDao userDao = new UserDao();

			// Todo. 

			await Navigation.PopAsync();
		}

		private async void SaveBtn_Clicked(object sender, EventArgs e)
		{
			var item = (Schooler.Class.Assignment)BindingContext;
//			DateTime time = (DateTime)(deadlinePicker.GetValue(DatePicker.DateProperty));
//			Assignment temp = new Assignment(0, nameEntry.Text, time);
			if (idx < 0)
			{
				UserDao dao = new UserDao();
				dao.AddAssignment(item);
				//				dao.AddAssignment(temp);
			}
			await Navigation.PopAsync();
		}

		private async void FileAddBtn_Clicked(object sender, EventArgs e)
		{
		}
	}
}
