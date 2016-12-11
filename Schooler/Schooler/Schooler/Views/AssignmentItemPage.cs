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
		public AssignmentItemPage()
		{
			this.SetBinding(ContentPage.TitleProperty, "name");

			NavigationPage.SetHasNavigationBar(this, true);

			/**
			var table = new TableView
			{
				Root = new TableRoot
				{
					new TableSection("Assignment Info")
					{
						new TextCell
						{
							Text = "Name"
						},
						nameEntry,
						new TextCell
						{
							Text = "Deadline"
						},
						deadlinePicker,
					},
					new TableSection("File list")
					{
						new FileList
					}	
				}
			};
 */

			// Assignment Class Member variable
			var nameLbl = new Label { Text = "Name" };
			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "name");

			var deadlineLbl = new Label { Text = "Deadline" };
			var deadlinePicker = new DatePicker();
			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

			ListView fileList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Views.FileCell))
			};
			fileList.SetBinding(ListView.ItemsSourceProperty, "fileList");
			var fileAddBtn = new Button { Text = "+", WidthRequest = 20, HeightRequest = 20 };
			fileAddBtn.Clicked += (sender, argv) =>
			{
				// Todo: File add
			};
			var fileLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					new StackLayout
					{
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

			ListView CommentList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Views.CommentCell))
			};
			CommentList.SetBinding(ListView.ItemsSourceProperty, "commentList");
			var commentAddBtn = new Button { Text = "+", WidthRequest = 20, HeightRequest = 20 };
			commentAddBtn.Clicked += (sender, argv) =>
			{
				// Todo: Comment add
			};
			var commentLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					new StackLayout
					{
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							new Label { Text = "Comment list" },
							commentAddBtn
						}
					},
					CommentList,
				}
			};

			// Buttons
			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) =>
			{
				var todoItem = (Schooler.Class.Assignment)BindingContext;
				//				App.Database.SaveItem(todoItem);
				Navigation.PopAsync();
			};
			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += (sender, e) =>
			{
				var todoItem = (Schooler.Class.Assignment)BindingContext;
				//				App.Database.DeleteItem(todoItem.ID);
				Navigation.PopAsync();
			};
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) =>
			{
				Navigation.PopAsync();
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
					fileLayout,
					commentLbl, CommentList,
					saveButton, deleteButton, cancelButton,
				}
			};
		}
	}
}
