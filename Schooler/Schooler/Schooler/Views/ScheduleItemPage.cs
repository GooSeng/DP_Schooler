using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Schooler.Class;

namespace Schooler.Views
{
	public class ScheduleItemPage : ContentPage
	{
		public ScheduleItemPage()
		{
			Title = "New Schedule";
			
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var name = new Entry();
			name.SetBinding(Entry.TextProperty, "name");

			var place = new Entry();
			place.SetBinding(Entry.TextProperty, "place");

			var addBtn = new Button()
			{
				Text = "Add"
			};
			var CancelBtn = new Button
			{
				Text = "Cancel"
			};
			addBtn.Clicked += AddBtn_Clicked;
			CancelBtn.Clicked += CancelBtn_Clicked;

			var layout = new StackLayout
			{
				Children =
				{
					new Label { Text = "Name" },
					name,
					new Label {Text = "Place" },
					place,
					addBtn,
					CancelBtn,
				}

			};

			Content = layout;
		}

		private async void CancelBtn_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		private async void AddBtn_Clicked(object sender, EventArgs e)
		{
			var item = (Schedule)BindingContext;
			UserDao dao = new UserDao();

			dao.AddSchedule(item);

			await Navigation.PopAsync();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			Content = null;
		}
	}
}
