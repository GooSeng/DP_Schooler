using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Schooler.Views
{
	public class DatePickerCell : ViewCell
	{
		public static readonly BindableProperty datePickerProperty
			= BindableProperty.Create(
				"datePicker", 
				typeof(DatePicker), 
				typeof(DatePickerCell), 
				null, 
				BindingMode.OneWay, null, null, null, null);
		public DatePicker datePicker
		{
			get
			{
				return (DatePicker)base.GetValue(DatePickerCell.datePickerProperty);
			}
			set
			{
				base.SetValue(DatePickerCell.datePickerProperty, value);
			}
		}

		public DatePickerCell()
		{
			View = datePicker;
		}
	}
}
