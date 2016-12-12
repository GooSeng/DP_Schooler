using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Pages
{
	public class SchedulePage : ContentPage
	{
		#region variables

		enum dayWeek { SUN, MON, TUE, WED, THU, FRI, SAT }
		public DateTime selectDate { get; set; }
		Class.UserDao dao = new Class.UserDao();
		ListView listView = new ListView
		{
			ItemTemplate = new DataTemplate(typeof(Views.ScheduleCell)),
			IsEnabled = false,
		};
		Label selectedDateLbl = new Label { Text = "", FontAttributes = FontAttributes.Bold, Margin = 10 };

		Button beforeBtn = new Button()
		{
			Text = "<"
		};
		Button nextBtn = new Button()
		{
			Text = ">"
		};

		Label monthLB = new Label
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand,
			VerticalOptions = LayoutOptions.CenterAndExpand,
			MinimumWidthRequest = 1
		};

		Grid calenderGrid = new Grid
		{
			HorizontalOptions = LayoutOptions.FillAndExpand,
		};


		#endregion

		bool isChangedMonth;

		public SchedulePage()
		{
			selectDate = DateTime.Now;

			Title = "Schedule";

			isChangedMonth = true;


			// set Button
			beforeBtn.Clicked += BeforeBtn_Clicked;
			nextBtn.Clicked += NextBtn_Clicked;

			// set grid layout for calendar
			RowDefinitionCollection rowDef = new RowDefinitionCollection();
			for (int i = 0; i < 6; i++)
				rowDef.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
			ColumnDefinitionCollection colDef = new ColumnDefinitionCollection();
			for (int i = 0; i < 7; i++)
				colDef.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star) });
			calenderGrid.RowDefinitions = rowDef;
			calenderGrid.ColumnDefinitions = colDef;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			Content = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			
			// set variables
			if(isChangedMonth)
				changedMonth();

			Grid topLayout = new Grid
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions =
				{
					new RowDefinition { Height = GridLength.Auto }
				},
				ColumnDefinitions =
				{
					new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)},
					new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)},
					new ColumnDefinition {Width = new GridLength(20, GridUnitType.Star)},
				}
			};
			topLayout.Children.Add(beforeBtn, 0, 0);
			topLayout.Children.Add(monthLB, 1, 0);
			topLayout.Children.Add(nextBtn, 2, 0);

			StackLayout layout = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Children =
				{
					topLayout,
					calenderGrid
				}
			};

			var addBtn = new Button
			{
				Text = "+"
			};
			addBtn.Clicked += AddBtn_Clicked;
			View view = new ScrollView
			{
				Content = new StackLayout
				{
					Children =
					{
						layout,
						new StackLayout
						{
							Orientation = StackOrientation.Horizontal,
							VerticalOptions = LayoutOptions.Center,
							Children =
							{
								selectedDateLbl,
								addBtn,
							}
						},
						listView,
					}
				}
			};

			this.Padding = new Thickness(10, 5, 10, 5);
			this.Content = view;
		}
		
		private async void AddBtn_Clicked(object sender, EventArgs e)
		{
			// Todo: Add Schedule
			var newItem = new Class.Schedule(selectDate);
			var page = new Views.ScheduleItemPage();
			page.BindingContext = (Class.Schedule)newItem;

			isChangedMonth = true;
			await Navigation.PushAsync(page);
		}

		private void BeforeBtn_Clicked(object sender, EventArgs e)
		{
			selectDate = selectDate.AddMonths(-1);
			changedMonth();
		}

		private void NextBtn_Clicked(object sender, EventArgs e)
		{
			selectDate = selectDate.AddMonths(1);
			changedMonth();
		}

		private void changedMonth()
		{
			monthLB.Text = selectDate.Year.ToString() + "/" + selectDate.Month.ToString();

			calenderGrid.Children.Clear();

			// set dayinweek label
			for (dayWeek i = 0; (int)i < 7; i++)
			{
				calenderGrid.Children.Add(new Label { Text = i.ToString(), HorizontalOptions = LayoutOptions.Center }, (int)i, 0);
			}
			// set days
			int startM, endM;
			int[] map = getCalendar(out startM, out endM);
			for (int i = 0; i < startM; i++)
			{
				calenderGrid.Children.Add(new Label { Text = map[i].ToString(), TextColor = Color.Silver, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, i % 7, i / 7 + 1);
				//	var scheduleItems = new listDa Class.Schedule();
				//	calenderGrid.Children.Add(new Views.CalenderItemCell(scheduleItem), i % 7, i / 7 + 1);
			}
			List<Class.Schedule> list;
			for (int i = startM; i < endM; i++)
			{
				list = dao.GetSchedule(selectDate.Year, selectDate.Month, map[i]);

				var btn = new Button
				{
					Text = map[i].ToString(),
					BackgroundColor = Color.White
				};
				if (list.Count > 0)
					btn.BackgroundColor = Color.Silver;

				btn.Clicked += Btn_Clicked;

				calenderGrid.Children.Add(btn, i % 7, i / 7 + 1);
			}
			for (int i = endM; i < 42; i++)
			{
				calenderGrid.Children.Add(new Label { Text = map[i].ToString(), TextColor = Color.Silver, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Center }, i % 7, i / 7 + 1);
			}

			isChangedMonth = false;
		}
		
		public static readonly BindableProperty listProperty =
			BindableProperty.Create("list", typeof(List<Class.Schedule>), typeof(SchedulePage), null, BindingMode.TwoWay);
		public List<Class.Schedule> list
		{
			get
			{
				return (List<Class.Schedule>)base.GetValue(SchedulePage.listProperty);
			}
			set
			{
				base.SetValue(SchedulePage.listProperty, value);
			}
		}

		private void Btn_Clicked(object sender, EventArgs e)
		{
			int day = Int32.Parse(((Button)sender).Text);
			selectDate = new DateTime(selectDate.Year, selectDate.Month, day);
			selectedDateLbl.Text = selectDate.ToString("yyyy-MM-dd");
			list = dao.GetSchedule(selectDate.Year, selectDate.Month, day);
			
			listView.ItemsSource = list;
			listView.BindingContext = (List<Class.Schedule>)list;
//			listView.SetBinding(ListView.ItemsSourceProperty, "list");
//			listView.SetBinding(ListView.ItemsSourceProperty, Binding.Create<Class.Schedule>(list[day], BindingMode.OneWay));

			if (list.Count > 0)
				OnAppearing();
		}

		private int[] getCalendar(out int startM, out int endM)
		{
			int[] map = new int[42];

			DateTime newDate = new DateTime(selectDate.Year, selectDate.Month, 1);
			startM = (int)newDate.DayOfWeek;
			int daysInM = DateTime.DaysInMonth(newDate.Year, newDate.Month);
			DateTime beforeDate = newDate.AddMonths(-1);
			int lastDayBeforeM = DateTime.DaysInMonth(beforeDate.Year, beforeDate.Month);

			int i;
			for (i = startM - 1; i >= 0; i--)
			{
				map[i] = lastDayBeforeM--;
			}
			i = startM;
			for (int j = 1; j <= daysInM; i++)
			{
				map[i] = j++;
			}
			endM = i;
			for (int j = 1; i < 42; i++)
			{
				map[i] = j++;
			}
			return map;
		}
	}
}
