﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler
{
	public class CalendarView : ContentPage
	{
		enum dayWeek { SUN, MON, TUE, WED, THU, FRI, SAT}
		public DateTime selectDate { get; set; }
		Class.UserDao dao = new Class.UserDao();
		ListView listView = new ListView
		{
			RowHeight = 40,
			ItemTemplate = new DataTemplate(typeof(Views.ScheduleCell)),
			BackgroundColor = Color.Gray
		};

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

		public CalendarView(DateTime date)
		{
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

			// set variables
			selectDate = date;
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
					/**
					new StackLayout
					{
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							beforeBtn,
							monthLB,
							nextBtn
						}
					},
					 */

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
						listView,
						addBtn,
					}
				}
			};

			this.Padding = new Thickness(10, 5, 10, 5);
			this.Content = view;
		}

		private void AddBtn_Clicked(object sender, EventArgs e)
		{
			// Todo: Add Schedule
		}

		private void Tgr_Tapped(object sender, EventArgs e)
		{
			calenderGrid.BackgroundColor = Color.Blue;
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
				calenderGrid.Children.Add(new Button { Text = i.ToString(), HorizontalOptions = LayoutOptions.Center }, (int)i, 0);
			}
			// set days
			int startM, endM;
			int[] map = getCalendar(out startM, out endM);
			for (int i=0; i<startM; i++)
			{
				calenderGrid.Children.Add(new Label { Text = map[i].ToString(), TextColor = Color.Silver }, i%7, i/7+1);
			//	var scheduleItems = new listDa Class.Schedule();
			//	calenderGrid.Children.Add(new Views.CalenderItemCell(scheduleItem), i % 7, i / 7 + 1);
			}
			List<Class.Schedule> list;
			for (int i = startM; i < endM; i++)
			{
				list = dao.GetSchedule(selectDate.Year, selectDate.Month, i);

				var btn = new Button
				{
					Text = map[i].ToString()					
				};
				if (list == null)
					btn.BackgroundColor = Color.Blue;
				btn.Clicked += Btn_Clicked;

				calenderGrid.Children.Add(btn, i % 7, i / 7 + 1);
			}
			for (int i = endM; i < 42; i++)
			{
				calenderGrid.Children.Add(new Label { Text = map[i].ToString(), TextColor = Color.Silver }, i % 7, i / 7 + 1);
			}
		}

		private void Btn_Clicked(object sender, EventArgs e)
		{
			int day = Int32.Parse(((Button)sender).Text);

			List<Class.Schedule> list = dao.GetSchedule(selectDate.Year, selectDate.Month, day);

			if(list != null)
			{
				listView.ItemsSource = list;
			}
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
