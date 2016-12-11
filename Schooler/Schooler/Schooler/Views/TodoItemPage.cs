using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class TodoItemPage : ContentPage
	{
		public TodoItemPage()
		{
			this.SetBinding(ContentPage.TitleProperty, "todoName");

			NavigationPage.SetHasNavigationBar(this, true);

			var nameLbl = new Label { Text = "Name" };
			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "todoName");

			var deadlineLbl = new Label { Text = "Deadline" };
			var deadlinePicker = new DatePicker();
			deadlinePicker.SetBinding(DatePicker.DateProperty, "deadline");

			var essentialLbl = new Label { Text = "Essential" };
			var essentialEntry = new Xamarin.Forms.Switch();
			essentialEntry.SetBinding(Switch.IsToggledProperty, "isEssential");

			var progressEntry = new Xamarin.Forms.Picker() {
				Title = "Progress",
				Items =
				{
					"A", "B", "C"
				}
			};
			progressEntry.SetBinding(Picker.SelectedIndexProperty, "progress");

			var managerLbl = new Label { Text = "Manager" };
			var managerEntry = new Entry();
			managerEntry.SetBinding(Entry.TextProperty, "managerUser");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += (sender, e) =>
			{
				var todoItem = (Schooler.Class.Todo)BindingContext;
				//				App.Database.SaveItem(todoItem);
				Navigation.PopAsync();
			};
			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += (sender, e) => {
				var todoItem = (Schooler.Class.Todo)BindingContext;
//				App.Database.DeleteItem(todoItem.ID);
				Navigation.PopAsync();
			};
		
		var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {
				var todoItem = (Schooler.Class.Todo)BindingContext;
				Navigation.PopAsync();
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLbl, nameEntry,
					deadlineLbl, deadlinePicker,
					essentialLbl, essentialEntry,
					progressEntry,
					managerLbl, managerEntry,
					saveButton, deleteButton, cancelButton,
				}
			};

	}
}
}
