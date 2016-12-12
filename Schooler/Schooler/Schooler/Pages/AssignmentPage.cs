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
	public class AssignmentPage : ContentPage
	{
		UserDao dao;

		ListView listView;
		Button addBtn;
		StackLayout layout;

		public AssignmentPage()
		{
			dao = new UserDao();
			Title = "Assignment";
			NavigationPage.SetHasNavigationBar(this, false);
		}
		
		private async void AddBtn_Clicked(object sender, EventArgs e)
		{
			var newAssignment = new Assignment();
			var assignmentPage = new AssignmentItemPage(-1);
			assignmentPage.BindingContext = (Assignment)newAssignment;
			await Navigation.PushAsync(assignmentPage);
		}
		
		private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				var assignmentItemPage = new AssignmentItemPage(((Assignment)e.SelectedItem).getIdx());
				assignmentItemPage.BindingContext = ((Assignment)e.SelectedItem);
				await Navigation.PushAsync(assignmentItemPage);
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(AssignmentItemCell)),
				IsPullToRefreshEnabled = true
			};
			listView.ItemsSource = dao.GetAssignment(dao.GetLoginedUser());
			listView.ItemSelected += ListView_ItemSelected;
			listView.RefreshCommand = new Command(() =>
			{
				listView.ItemsSource = dao.GetAssignment();
				listView.IsRefreshing = false;
			});

			addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += AddBtn_Clicked;

			layout = new StackLayout();
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);

			Content = layout;

		}
		protected override void OnDisappearing()
		{
			Content = null;

			base.OnDisappearing();
		}
	}
}
