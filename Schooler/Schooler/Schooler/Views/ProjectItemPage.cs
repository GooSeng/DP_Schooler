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
		ProjectDao dao;
		ScrollView scrollView;
		Entry teamEntry;


        ListView fileList;
        Button fileAddBtn;
        Entry fileUrlEntry;
        StackLayout fileLayout;

        ListView commentList;
        Button commentAddBtn;
        Entry commentEntry;
        StackLayout commentLayout;

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
			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

			var isTeamLbl = new Label { Text = "Team Project" };
			var isTeamSwitch = new Switch();
			isTeamSwitch.SetBinding(Switch.IsToggledProperty, "isTeam");
			if (idx != -1)
				isTeamSwitch.IsEnabled = false;

            // File List 
            fileList = new ListView
            {
                RowHeight = 40,
                ItemTemplate = new DataTemplate(typeof(Views.FileCell))
            };
            fileList.ItemTemplate.SetBinding(FileCell.idxProperty, "Idx");
            fileList.SetBinding(ListView.ItemsSourceProperty, "fileList");
            fileList.ItemsSource = dao.GetFileList();
			fileList.ItemSelected += FileList_ItemSelected;
            fileUrlEntry = new Entry { Placeholder = "URL", WidthRequest = 100, HeightRequest = 30 };
            fileAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30, Margin = 0 };
            fileAddBtn.Clicked += FileAddBtn_Clicked;

            fileLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    new Label { Text = "File list" },
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            fileUrlEntry,
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
                ItemTemplate = new DataTemplate(typeof(Views.CommentCell)),
				IsPullToRefreshEnabled = true
			};
            commentList.ItemTemplate.SetBinding(CommentCell.idxProperty, "Idx");
            commentList.SetBinding(ListView.ItemsSourceProperty, "commentList");
            commentList.ItemsSource = dao.GetCommentList();
			commentList.RefreshCommand = new Command(() =>
			{
				commentList.ItemsSource = dao.GetCommentList();
				commentList.IsRefreshing = false;
			});
			commentList.ItemSelected += CommentList_ItemSelected;
			commentAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30 };
            commentEntry = new Entry { WidthRequest = 300 };
            commentEntry.SetBinding(Entry.TextProperty, "comment");
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


            // Team List
            var teamList = new ListView
			{
				HeightRequest = 100,
				RowHeight = 40,
//				ItemTemplate = new DataTemplate(typeof(TeamCell)),
			};
			teamList.ItemsSource = dao.GetTeamUser();
//			teamList.SetBinding(ListView.ItemsSourceProperty, "teamList");
			var teamAddBtn = new Button { Text = "+", WidthRequest = 30, HeightRequest = 30 };
			teamEntry = new Entry { WidthRequest = 300 };
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
			TodoListView todolistView = new TodoListView(idx);
			var todoLayout = new StackLayout
			{
				Children =
				{
					new Label { Text = "Todo list" },
					todolistView
				}
			};

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
				todolistView.BindingContext = dao.GetTodo();

				layout.Children.Add(fileLayout);
				layout.Children.Add(commentLayout);
				layout.Children.Add(todoLayout);

				if (((Project)this.BindingContext).isTeam)
					layout.Children.Add(teamLayout);
			}
			layout.Children.Add(saveButton);
			layout.Children.Add(cancelButton);
			if (idx != -1)
			{
				layout.Children.Add(deleteButton);
			}

			scrollView = new ScrollView();
			scrollView.Content = layout;

			Content = scrollView;
		}

		private void FileList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			OnAppearing();
		}

		private void CommentList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			OnAppearing();
		}

		private async void FileAddBtn_Clicked(object sender, EventArgs e)
        {
            UserDao userdao = new UserDao();
            var item = new File();
            item.uploadUserId = userdao.GetLoginedUser();
            item.projectIdx = idx;
            item.name = fileUrlEntry.Text;
            item.url = item.name;


            bool isOk = await dao.UploadFile(item);

            if (isOk)
                await DisplayAlert("Good", "File Upload", "OK");
            else
                await DisplayAlert("Error", "File size error, Please Check the File Size", "Ok");

			this.OnAppearing();
		}

        private void CommentAddBtn_Clicked(object sender, EventArgs e)
        {
            UserDao userdao = new UserDao();
            var item = new Comment { comment = commentEntry.Text };
            item.uploadUserId = userdao.GetLoginedUser();
            item.projectIdx = idx;

            dao.UploadComment(item);

			this.OnAppearing();
        }

        private void TeamAddBtn_Clicked(object sender, EventArgs e)
		{
            bool isNotHave = true;
            foreach (var item in dao.GetTeamUser())
            {
                if (item.Equals(teamEntry.Text))
                {
                    isNotHave = false;
                    break;
                }
            }
            if(isNotHave)
			    dao.AddTeam(teamEntry.Text);
			this.OnAppearing();
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
				dao.Update(item);
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
