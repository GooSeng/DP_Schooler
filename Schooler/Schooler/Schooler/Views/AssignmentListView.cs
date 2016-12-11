using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

using Schooler.Class;

namespace Schooler.Views
{
	public class AssignmentListView : ContentView
	{
		ListView listView;

		public AssignmentListView()
		{
			NavigationPage.SetHasNavigationBar(this, false);
			UserDao dao = new UserDao();
			
			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(AssignmentItemCell))
			};
			listView.ItemsSource = dao.GetAssignment(dao.GetLoginedUser());
			listView.ItemSelected += ListView_ItemSelected;
		
			var addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += AddBtn_Clicked;

			var layout = new StackLayout();
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;
		}

		private void AddBtn_Clicked(object sender, EventArgs e)
		{
			//				var todoItem = new Assignment();
			var assignmentPage = new AssignmentItemPage(true);
			//				todoPage.BindingContext = todoItem;
			Navigation.PushAsync(assignmentPage);
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if(e.SelectedItem != null)
			{
				var assignmentItemPage = new AssignmentItemPage(false);
				assignmentItemPage.BindingContext = e.SelectedItem;
				Navigation.PushAsync(assignmentItemPage);
				listView.SelectedItem = null;
			}
		}
	}
}
