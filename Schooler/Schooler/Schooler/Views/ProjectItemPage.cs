using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Schooler.Class;

namespace Schooler.Views
{
	public class ProjectItemPage : ContentPage
	{
		int idx;
		AssignmentDao dao;

		#region UI Variable


		#endregion

		public ProjectItemPage(int _idx)
		{
			NavigationPage.SetHasNavigationBar(this, true);
			idx = _idx;

			if (idx != -1)
				dao = new AssignmentDao(idx);

			// Set Title
			Title = (idx < 0) ? "Add Project" : "Edit Project";

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			/// Project Class Member variable
			var nameLbl = new Label { Text = "Name" };
			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "name");

			var deadlineLbl = new Label { Text = "Deadline" };
			var deadlinePicker = new DatePicker();
			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

			var isTeamLbl = new Label { Text = "Team Project" };
			var isTeamSwitch = new Switch();
			isTeamSwitch.SetBinding(Switch.IsToggledProperty, "isTeam");

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

			// Team List
			var teamList = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(Label))
			};
			teamList.SetBinding(ListView.ItemsSourceProperty, "teamList");
			var teamAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30 };
			var teamEntry = new Entry { WidthRequest = 300 };
			teamAddBtn.Clicked += (sender, argv) =>
			{
				// Todo: Team Member add

			};
			var teamLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					new Label { Text = "Team Member list" },
					new StackLayout
					{
						VerticalOptions = LayoutOptions.Center,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							teamEntry,
							teamAddBtn
						}
					},
					teamList,
				}
			};

			// Todo List
			TodoListView todolistView = new TodoListView();

			// Buttons
			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += SaveButton_Clicked;

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += DeleteButton_Clicked;

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += CancelButton_Clicked;

			var layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
					isTeamLbl, isTeamSwitch
				}
			};
			if (idx != -1)
			{
				layout.Children.Add(fileLayout);
				layout.Children.Add(commentLayout);
				layout.Children.Add(todolistView);
			}
			layout.Children.Add(saveButton);
			layout.Children.Add(cancelButton);
			if (idx != -1)
			{
				layout.Children.Add(deleteButton);
			}

			Content = layout;
		}

		private async void SaveButton_Clicked(object sender, EventArgs e)
		{
			var todoItem = (Schooler.Class.Project)BindingContext;

			if(idx < 0)
			{

			}
			else
			{
				dao.Update(todoItem);
			}

			//				App.Database.SaveItem(todoItem);

			await Navigation.PopAsync();
		}

		private async void DeleteButton_Clicked(object sender, EventArgs e)
		{
			dao.Delete();
			await Navigation.PopAsync();
		}

		private async void CancelButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}
