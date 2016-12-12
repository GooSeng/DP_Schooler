using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Schooler.Class;
using Schooler.Views;

namespace Schooler.Pages
{
	public class ProjectPage : ContentPage
	{
		ListView listView;
		UserDao dao = new UserDao();

		Button addBtn;

		StackLayout layout;

		public ProjectPage()
		{
			Title = "Project";

			NavigationPage.SetHasNavigationBar(this, false);
			Schooler.Class.UserDao dao = new Class.UserDao();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// project list 
			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(ProjectItemCell))
			};
			listView.ItemsSource = dao.GetProject(dao.GetLoginedUser());
			listView.ItemSelected += ListView_ItemSelected;

			// add button
			addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += AddBtn_Clicked;

			// main layout
			layout = new StackLayout();
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			
			Content = layout;
		}

		private async void AddBtn_Clicked(object sender, EventArgs e)
		{
			var newItem = new Project();
			var projectItemPage = new ProjectItemPage(-1);
			projectItemPage.BindingContext = ((Project)newItem);
			await Navigation.PushAsync(projectItemPage);
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				var projectItemPage = new ProjectItemPage(((Project)e.SelectedItem).getIdx());
				projectItemPage.BindingContext = ((Project)e.SelectedItem);
				Navigation.PushAsync(projectItemPage);
				listView.SelectedItem = null;
			}
		}
	}
}
