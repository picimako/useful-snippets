using System;

namespace WeeklyTodo
{
    /// <summary>
    /// Generates a generic list of TODO items for the next week, formatted with each day's date.
    /// This is designed for the Sublime Text plugin called PlainTasks, but it might work for other tools as well.
    ///
    /// You may also include Sprint and Release numbers or other dynamic data if you'd like.
    /// </summary>
    class WeeklyTodoGenerator
    {
        private const string WEEK = @"Monday {0}:
☐ Daily standup () @meeting
☐ 17:00 Client standup () @meeting
Tuesday {1}:
☐ Daily standup () @meeting
☐ 17:00 Client standup () @meeting
Wednesday {2}:
☐ Daily standup () @meeting
☐ 17:00 Client standup () @meeting
Thursday {3}:
☐ Daily standup () @meeting
☐ 17:00 Client standup () @meeting
Friday {4}:
☐ Daily standup () @meeting
☐ 17:00 Client standup () @meeting
☐ Fill in time report";

        public string Generate()
        {
            DateTime nextMonday = NextMonday(DateTime.Today);
            return String.Format(WEEK, Format(nextMonday), Format(nextMonday, 1), Format(nextMonday, 2), Format(nextMonday, 3), Format(nextMonday, 4));
        }

        /// <summary>
        /// Returns a formatted date string for the day shifted {extraDays} from next monday.
        /// </summary>
        /// <param name="dateTime">The date/time for next Monday.</param>
        /// <param name="extraDays">The number of days to shift from next Monday.</param>
        private string Format(DateTime dateTime, int extraDays = 0)
        {
            DateTime extraDateTime = dateTime.AddDays(extraDays);
            return $"{extraDateTime.Year}.{extraDateTime.Month}.{extraDateTime.Day}";
        }

        /// <summary>
        /// Returns the date for the next monday. See https://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
        /// </summary>
        /// <param name="start">The current date/time to calculate the next Monday for.</param>
        private DateTime NextMonday(DateTime start)
        {
            int daysToAdd = ((int)DayOfWeek.Monday - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
    }
}
