using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Schooler.Class;

namespace Schooler.Views
{
	public class TodoItemPage : ContentPage
	{
		int idx;
		ProjectDao dao;
		Picker progressEntry;

		public TodoItemPage()
		{
			NavigationPage.SetHasNavigationBar(this, true);

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			idx = ((Todo)this.BindingContext).Idx;
			dao = new ProjectDao(((Todo)BindingContext).ProjectIdx);

			Title = (idx == -1) ? "New Todo" : "Edit Todo";

			var nameLbl = new Label { Text = "Name" };
			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "Name");

			var deadlineLbl = new Label { Text = "Deadline" };
			var deadlinePicker = new DatePicker();
			deadlinePicker.SetBinding(DatePicker.DateProperty, "DeadLine");

			var essentialLbl = new Label { Text = "Essential" };
			var essentialEntry = new Xamarin.Forms.Switch();
			essentialEntry.SetBinding(Switch.IsToggledProperty, "IsEssential");

			var progressLbl = new Label { Text = "Progress" };
			progressEntry = new Xamarin.Forms.Picker()
			{
				Items =
				{
					"Before Starting", "Proceeding", "END"
				}
			};		
//			progressEntry.SetBinding(Picker.SelectedIndexProperty, "Progress");

			var managerLbl = new Label { Text = "Manager" };
			var managerEntry = new Entry();
			managerEntry.SetBinding(Entry.TextProperty, "ManageUserId");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += SaveButton_Clicked;

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += DeleteButton_Clicked;

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += CancelButton_Clicked; 

			var layout = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
//				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
					essentialLbl, essentialEntry,
					progressLbl, progressEntry,
					managerLbl, managerEntry,
					saveButton, cancelButton,
				}
			};
			if(idx != -1)
			{
				layout.Children.Add(deleteButton);
			}

			Content = layout;
		}

		private async void SaveButton_Clicked(object sender, EventArgs e)
		{
			var item = (Schooler.Class.Todo)BindingContext;
            item.Content = "N";
			item.Progress = progressEntry.SelectedIndex == 0 ? "B" : progressEntry.SelectedIndex == 1 ? "P" : "E";

			if(idx == -1)   // new Item
			{
				dao.AddTodo(item);
			}
			else			// edit item
			{
				dao.UpdateTodo(idx, item);
			}

			await Navigation.PopAsync();
		}

		private async void CancelButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		private async void DeleteButton_Clicked(object sender, EventArgs e)
		{
			var item = (Schooler.Class.Todo)BindingContext;

			dao.DeleteTodo(idx);

			await Navigation.PopAsync();
		}


	}
}
