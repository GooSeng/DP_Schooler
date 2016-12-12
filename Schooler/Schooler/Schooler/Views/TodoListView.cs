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
		ProjectDao dao;

		protected override void OnParentSet()
		{
			base.OnParentSet();
			listView = new ListView
			{
				HeightRequest = 100,
				RowHeight = 40,
				ItemTemplate = new DataTemplate(typeof(TodoItemCell)),
				IsPullToRefreshEnabled = true
			};
			listView.ItemsSource = dao.GetTodo();
//			listView.ItemsSource = ((List<Todo>)this.BindingContext);
			listView.ItemSelected += ListView_ItemSelected;
			listView.RefreshCommand = new Command(() =>
			{
				listView.ItemsSource = ((List<Todo>)this.BindingContext);
				listView.IsRefreshing = false;
			});

			var addBtn = new Button();
			addBtn.Text = "+";
			addBtn.Clicked += AddBtn_Clicked;

			var layout = new StackLayout();
			layout.Children.Add(addBtn);
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;

		}

		int projectIdx;
		public TodoListView(int _pIdx)
		{
			projectIdx = _pIdx;
			dao = new ProjectDao(projectIdx);
		}
		
		private async void AddBtn_Clicked(object sender, EventArgs e)
		{
			var item = new Todo(projectIdx);
			var todoPage = new TodoItemPage();
			todoPage.BindingContext = (Todo)item;
			await Navigation.PushAsync(todoPage);
		}

		private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var todoPage = new TodoItemPage();
			todoPage.BindingContext = ((Todo)e.SelectedItem);
			await Navigation.PushAsync(todoPage);
//			listView.SelectedItem = null;
		}
	}
}
