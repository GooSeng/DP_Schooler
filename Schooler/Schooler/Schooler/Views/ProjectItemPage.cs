﻿using System;
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
		ProjectDao dao;
		
		public ProjectItemPage(int _idx)
		{
			NavigationPage.SetHasNavigationBar(this, true);
			idx = _idx;

			if (idx != -1)
				dao = new ProjectDao(idx);

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
			deadlinePicker.Format = "yyyy-MM-dd hh:mm";
			deadlinePicker.BindingContext = "deadline";
//			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

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
			fileAddBtn.Clicked += FileAddBtn_Clicked;

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
			commentAddBtn.Clicked += CommentAddBtn_Clicked;

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
			teamAddBtn.Clicked += TeamAddBtn_Clicked;

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

		private void FileAddBtn_Clicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void CommentAddBtn_Clicked(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void TeamAddBtn_Clicked(object sender, EventArgs e)
		{
			// Todo: Team Member add
		}

		private async void SaveButton_Clicked(object sender, EventArgs e)
		{
			Project item = (Project)BindingContext;
			UserDao userDao = new UserDao();

			if(idx < 0)
			{
				userDao.AddProject(item);
			}
			else
			{
				dao.UpdateAssignment(todoItem);
			}
			
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
