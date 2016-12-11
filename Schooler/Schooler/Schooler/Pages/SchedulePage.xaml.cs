using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Schooler.Pages
{
    public partial class SchedulePage : ContentPage
	{ 
        public SchedulePage()
        {
			Title = "Schedlue";



			StackLayout layout = new StackLayout
			{
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,

				Children =
				{
					new StackLayout
					{
						Spacing = 0,
						Orientation = StackOrientation.Horizontal,
						Children =
						{
							new CalendarView(DateTime.Now)
						}
					}
				}
			};

			Content = layout;

//			InitializeComponent();
        }

	}
}
