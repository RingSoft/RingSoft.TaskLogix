using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurWeeklyProcessor : TaskRecurProcessorBase
    {
        public WeeklyRecurTypes RecurType { get; set; }

        public int RecurWeeks { get; set; }

        public bool Sunday { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }

        public bool Saturday { get; set; }

        public int RegenWeeksAfterCompleted { get; set; }

        public TaskRecurWeeklyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
            RecurType = WeeklyRecurTypes.EveryXWeeks;
            RecurWeeks = 1;
            switch (TaskProcessor.StartDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Sunday = true;
                    break;
                case DayOfWeek.Monday:
                    Monday = true;
                    break;
                case DayOfWeek.Tuesday:
                    Tuesday = true;
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday = true;
                    break;
                case DayOfWeek.Thursday:
                    Thursday = true;
                    break;
                case DayOfWeek.Friday:
                    Friday = true;
                    break;
                case DayOfWeek.Saturday:
                    Saturday = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetPropsFromEntity(TlTaskRecurWeekly entity)
        {
            throw new NotImplementedException();
        }

        public override void DoMarkComplete()
        {
            switch (RecurType)
            {
                case WeeklyRecurTypes.EveryXWeeks:
                    TaskProcessor.StartDate = GetNextDate(TaskProcessor.StartDate);
                    break;
                case WeeklyRecurTypes.RegenerateXWeeksAfterCompleted:
                    TaskProcessor.StartDate = GetNextDateAfterCompleted(DateTime.Today);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AdjustStartDate()
        {
            switch (RecurType)
            {
                case WeeklyRecurTypes.EveryXWeeks:
                    TaskProcessor.StartDate = GetNextDate(TaskProcessor.StartDate.AddDays(-1));
                    break;
            }
        }

        public override void LoadRecurProcessor(TlTask task)
        {
            if (task.RecurWeekly.Any())
            {
                var recurWeekly = task.RecurWeekly.FirstOrDefault();
                if (recurWeekly != null)
                {
                    this.RecurType = (WeeklyRecurTypes)recurWeekly.RecurType;
                    this.RecurWeeks = recurWeekly.RecurWeeks.GetValueOrDefault();
                    this.RegenWeeksAfterCompleted = recurWeekly.RegenWeeksAfterCompleted.GetValueOrDefault();

                    this.Sunday = recurWeekly.Sunday.GetValueOrDefault();
                    this.Monday = recurWeekly.Monday.GetValueOrDefault();
                    this.Tuesday = recurWeekly.Tuesday.GetValueOrDefault();
                    this.Wednesday = recurWeekly.Wednesday.GetValueOrDefault();
                    this.Thursday = recurWeekly.Thursday.GetValueOrDefault();
                    this.Friday  = recurWeekly.Friday.GetValueOrDefault();
                    this.Saturday = recurWeekly.Saturday.GetValueOrDefault();
                }
            }
        }

        public override bool SaveRecurProcessor(TlTask task, IDbContext context)
        {
            var tlTaskRecurWeekly = new TlTaskRecurWeekly
            {
                TaskId = task.Id,
                RecurType = (byte)RecurType,
                RecurWeeks = RecurWeeks,
                RegenWeeksAfterCompleted = RegenWeeksAfterCompleted,
                Sunday = Sunday,
                Monday = Monday,
                Tuesday = Tuesday,
                Wednesday = Wednesday,
                Thursday = Thursday,
                Friday = Friday,
                Saturday = Saturday,
            };

            return context.AddSaveEntity(tlTaskRecurWeekly, "");
        }

        public override string GetRecurText()
        {
            var text = string.Empty;

            switch (RecurType)
            {
                case WeeklyRecurTypes.EveryXWeeks:
                    text = $"{RecurWeeks} Week(s) On ";
                    var days = new List<string>();
                    if (Sunday)
                    {
                        days.Add("Sunday");
                    }

                    if (Monday)
                    {
                        days.Add("Monday");
                    }

                    if (Tuesday)
                    {
                        days.Add("Tuesday");
                    }

                    if (Wednesday)
                    {
                        days.Add("Wednesday");
                    }

                    if (Thursday)
                    {
                        days.Add("Thursday");
                    }

                    if (Friday)
                    {
                        days.Add("Friday");
                    }

                    if (Saturday)
                    {
                        days.Add("Saturday");
                    }

                    var index = 0;
                    foreach (var day in days)
                    {
                        if (days.IndexOf(day) == days.Count - 2)
                        {
                            text += $"{day} and ";
                        }
                        else if (days.IndexOf(day) == days.Count - 1)
                        {
                            text += day;
                        }
                        else
                        {
                            text += $"{day}, ";
                        }
                        index++;
                    }
                    break;
                case WeeklyRecurTypes.RegenerateXWeeksAfterCompleted:
                    text = $"{RegenWeeksAfterCompleted} Week(s) After the Task Has Been Completed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return text;
        }

        private DateTime GetNextDateAfterCompleted(DateTime startDate)
        {
            var daysToAdd = RegenWeeksAfterCompleted * 7;
            return startDate.AddDays(daysToAdd);
        }

        internal DateTime GetNextDate(DateTime startDate)
        {
            var daysToAdd = GetDaysToAdd(startDate);

            return startDate.AddDays(daysToAdd);
        }

        private int GetDaysToAdd(DateTime startDate)
        {
            var nextWeekday = GetNextWeekDay(startDate);
            var result = 0;
            if (nextWeekday <= (int)startDate.DayOfWeek)
            {
                result = (int)DayOfWeek.Saturday - (int)startDate.DayOfWeek;
                result += nextWeekday + 1;
                result += (7 * (RecurWeeks - 1));
            }
            else
            {
                result = nextWeekday - (int)startDate.DayOfWeek;
            }
            return result;
        }

        private int GetNextWeekDay(DateTime startDate)
        {
            var startIndex = (int)startDate.DayOfWeek + 1;

            var selList = new List<bool>();
            selList.Add(Sunday);
            selList.Add(Monday);
            selList.Add(Tuesday);
            selList.Add(Wednesday);
            selList.Add(Thursday);
            selList.Add(Friday);
            selList.Add(Saturday);

            var result = selList.FindIndex(startIndex, p => p.Equals(true));

            if (result == -1)
            {
                result = selList.FindIndex(0, p => p.Equals(true));
            }
            return result;
        }
    }
}
