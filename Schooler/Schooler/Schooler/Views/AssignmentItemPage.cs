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
			nameEntry.SetBinding(EntryCell.TextProperty, "name");

			var deadlineLbl = new Label { Text = "Deadline" };
			var deadlinePicker = new DatePicker();
			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

			var fileLbl = new Label { Text = "File list" };
			ListView fileList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Class.File))
			};
			fileList.SetBinding(ListView.ItemsSourceProperty, "fildList");

			var commentLbl = new Label { Text = "Comment list" };
			ListView CommentList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Class.File))
			};
			fileList.SetBinding(ListView.ItemsSourceProperty, "fildList");
			

			// Buttons
			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) =>
			{
				var todoItem = (Schooler.Class.Todo)BindingContext;
				//				App.Database.SaveItem(todoItem);
				Navigation.PopAsync();
			};
			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += (sender, e) =>
			{
				var todoItem = (Schooler.Class.Todo)BindingContext;
				//				App.Database.DeleteItem(todoItem.ID);
				Navigation.PopAsync();
			};
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) =>
			{
				var todoItem = (Schooler.Class.Todo)BindingContext;
				Navigation.PopAsync();
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
					fileLbl, fileList,
					commentLbl, CommentList,
					saveButton, deleteButton, cancelButton,
				}
			};
		}
	}
}
