using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

using Schooler.Class;

namespace Schooler.Views
{
	public class TodoListView : ContentView
	{
		ListView listView;

		public TodoListView()
		{
//			Title = "Todo";

//			NavigationPage.SetHasNavigationBar(this, true);

			listView = new ListView
			{
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(TodoItemCell))
			};
			listView.ItemsSource = new Todo[]
			{
				new Todo { Name = "A" },
				new Todo { Name = "B" }
			};
			listView.ItemSelected += ListView_ItemSelected;

			var addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += (sender, args) => {
				var todoItem = new Todo();
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
			};

			var layout = new StackLayout();
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;
		}

		private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var todoPage = new TodoItemPage();
			todoPage.BindingContext = this.listView.SelectedItem;
			Navigation.PushAsync(todoPage);
			listView.SelectedItem = null;
		}
	}
}
