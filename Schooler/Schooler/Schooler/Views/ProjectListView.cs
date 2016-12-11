using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class ProjectListView : ContentView
	{
		ListView listView;

		public ProjectListView()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			Schooler.Class.UserDao dao = new Class.UserDao();

			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(ProjectItemCell))
			};
			listView.ItemsSource = dao.GetProject(dao.GetLoginedUser());
			listView.ItemSelected += (sender, e) =>
			{
				if(e.SelectedItem != null)
				{
					var projectItemPage = new ProjectItemPage(false);
					projectItemPage.BindingContext = e.SelectedItem;
					Navigation.PushAsync(projectItemPage);
					listView.SelectedItem = null;
				}
			};

			var addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += (sender, args) =>
			{
				//				var todoItem = new Assignment();
				var projectItemPage = new ProjectItemPage(true);
				//				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(projectItemPage);
			};

			var layout = new StackLayout();
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;
		}
	}
}
