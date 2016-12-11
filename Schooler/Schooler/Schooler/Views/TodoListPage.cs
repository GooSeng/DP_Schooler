using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

using Schooler.Class;

namespace Schooler.Views
{
	public class TodoListPage : ContentPage
	{
		ListView listView;

		public TodoListPage()
		{
			Title = "Todo";

//			NavigationPage.SetHasNavigationBar(this, true);

			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(TodoItemCell))
			};

			var layout = new StackLayout();
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

			ToolbarItem tbi = null;
//			if (Device.OS == TargetPlatform.Android)
			{
				tbi = new ToolbarItem("+", "plus", () =>
				{
					var todoItem = new Todo();
					var todoPage = new TodoItemPage();
					todoPage.BindingContext = todoItem;
					Navigation.PushAsync(todoPage);
				}, 0, 0);
			}
			ToolbarItems.Add(tbi);
			
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
//			listView.ItemsSource = App.Database.GetItems();
		}
	}
}
