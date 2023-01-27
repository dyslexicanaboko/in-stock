namespace InStock.Lib.Services
{
	//TODO: This is a service I used for a different program. It may not be suitable for banker holidays
	/// <summary>
	/// https://en.wikipedia.org/wiki/Holidays_with_paid_time_off_in_the_United_States#Holiday_listing_as_paid_time_off
	/// </summary>
	public class AmericanHolidaySearch
	{
		public const string UnknownHoliday = "Other holiday or sabbatical";

		private readonly SortedDictionary<DateTime, string> _holidays;

		private readonly int _year;

		public AmericanHolidaySearch(int year)
		{
			_year = year;

			_holidays = BuildHolidayList();
		}

		public bool IsHoliday(DateTime todaysDate, out string? holiday)
		{
			holiday = null;

			var d = todaysDate.Date;

			if (!_holidays.ContainsKey(d)) return false;

			holiday = _holidays[d];

			return true;
		}

		private SortedDictionary<DateTime, string> BuildHolidayList()
		{
			var dict = new SortedDictionary<DateTime, string>
			{
				{ D(1, 1) , "New Year's Day" },
			//No one is celebrating Inauguration Day - I didn't even know this was a thing
			//	{ D(1, 20) , "Inauguration Day" },
			//	{ D(1, 21) , "Inauguration Day" },
				{ D(7, 4) , "Independence Day" },
				{ D(11, 11) , "Veterans Day" },
				{ D(12, 25) , "Christmas" },
			};

			//Check for holidays that fall on weekends
			var fallsOnWeekend = dict
					.Where(x => 
						x.Key.DayOfWeek == DayOfWeek.Sunday ||
						x.Key.DayOfWeek == DayOfWeek.Saturday)
					.ToList();

			foreach (var (date, holiday) in fallsOnWeekend)
			{
				//If it's Saturday add two days to move to Monday.
				//If it's Sunday, add one day to move to Monday.
				var add = date.DayOfWeek == DayOfWeek.Saturday ? 2 : 1;
						
				dict.Add(date.AddDays(add), holiday);
			}

			//Floating holidays
			// January 15 - 21
			FloatingHoliday(dict, 1, 15, 21, DayOfWeek.Monday, "Birthday of Martin Luther King, Jr.");
	
			// February 15 - 21
			FloatingHoliday(dict, 2, 15, 21, DayOfWeek.Monday, "Washington's Birthday");
	
			// May 25 - 31
			FloatingHoliday(dict, 5, 25, 31, DayOfWeek.Monday, "Memorial Day");
	
			// September 1 - 7
			FloatingHoliday(dict, 9, 1, 7, DayOfWeek.Monday, "Labor Day");
	
			// October 8 - 14
			FloatingHoliday(dict, 10, 8, 14, DayOfWeek.Monday, "Indigenous Peoples' Day");
	
			// November 22 - 28
			FloatingHoliday(dict, 11, 22, 28, DayOfWeek.Thursday, "Thanksgiving Day");
	
			return dict;
		}

		private DateTime D(int month, int day)
		{
			return new DateTime(_year, month, day);
		}

		/// <summary>
		/// Find the exact day for this year for the specified floating holiday
		/// </summary>
		/// <param name="dict">Dictionary reference</param>
		/// <param name="month">Month holiday occurs on</param>
		/// <param name="startDays">Start day range for holiday</param>
		/// <param name="endDays">End day range for holiday</param>
		/// <param name="dayOfWeek">Day of week holiday occurs on</param>
		/// <param name="holiday">Name of holiday</param>
		private void FloatingHoliday(IDictionary<DateTime, string> dict, int month, int startDays, int endDays, DayOfWeek dayOfWeek, string holiday)
		{
			var start = D(month, startDays);
			var end = D(month, endDays);

			//Search for the day of the week for this year
			for (var dtm = start; dtm <= end; dtm = dtm.AddDays(1))
			{
				if (dtm.DayOfWeek != dayOfWeek) continue;
		
				//When there is a match for the day of the week, then this
				//the date to use for this year for this holiday
				dict.Add(dtm, holiday);

				//Stop searching
				break;
			}
		}
	}
}
