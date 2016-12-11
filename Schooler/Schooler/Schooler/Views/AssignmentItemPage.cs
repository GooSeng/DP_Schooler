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
		public AssignmentItemPage(bool isNew)
		{
			NavigationPage.SetHasNavigationBar(this, true);

			// Set Title
			if (isNew)
			{
				Title = "Add Assignment";
			}
			else
			{
				Title = "Edit Assingment: ";
			}
			

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
			
		
			// File List 
			var fileList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Views.FileCell))
			};
			fileList.SetBinding(ListView.ItemsSourceProperty, "fileList");
			var fileAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30, Margin = 0 };
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
			var commentList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Views.CommentCell))
			};
			commentList.SetBinding(ListView.ItemsSourceProperty, "commentList");
			var commentAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30 };
			var commentEntry = new Entry { WidthRequest = 300 };
			commentAddBtn.Clicked += (sender, argv) =>
			{
				// Todo: Comment add

			};
			var commentLayout = new StackLayout
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
			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) =>
			{
				//var todoItem = (Schooler.Class.Assignment)BindingContext;
                DateTime time = (DateTime)(deadlinePicker.GetValue(DatePicker.DateProperty));
                Assignment temp = new Assignment(0, nameEntry.Text, time);
                if(isNew)
                {
                    UserDao dao = new UserDao();
                    dao.AddAssignment(temp);
                }
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
			
			var layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
				}
			};
			if(!isNew)
			{
				layout.Children.Add(fileLayout);
				layout.Children.Add(commentLayout);
			}
			layout.Children.Add(saveButton);
			layout.Children.Add(cancelButton);
			if (!isNew)
			{
				layout.Children.Add(deleteButton);
			}
			Content = layout;
		}
	}
}
