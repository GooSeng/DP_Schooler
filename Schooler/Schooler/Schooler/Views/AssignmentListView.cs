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
			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(AssignmentItemCell))
			};
			//			listView.ItemsSource = ;
			listView.ItemSelected += (sender, args) =>
			{
				var assignmentItemPage = new AssignmentItemPage();
				assignmentItemPage.BindingContext = listView.SelectedItem;
				Navigation.PushAsync(assignmentItemPage);
				listView.SelectedItem = null;
			};

			var addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += (sender, args) =>
			{
				//				var todoItem = new Assignment();
				var todoPage = new AssignmentItemPage();
				//				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			};

			var layout = new StackLayout();
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;
		}
	}
}
