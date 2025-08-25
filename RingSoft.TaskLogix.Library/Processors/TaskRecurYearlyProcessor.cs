using RingSoft.DataEntryControls.Engine;
using RingSoft.DbLookup;
using RingSoft.TaskLogix.DataAccess.Model;

namespace RingSoft.TaskLogix.Library.Processors
{
    public class TaskRecurYearlyProcessor : TaskRecurProcessorBase
    {
        public YearlylyRecurTypes RecurType { get; set; }

        public MonthsInYear EveryMonthType { get; set; }

        public int MonthDay { get; set; }

        public WeekTypes WeekType { get; set; }

        public DayTypes DayType { get; set; }

        public MonthsInYear WeekMonthType { get; set; }

        public int RegenYearsAfterCompleted { get; set; }

        public TaskRecurYearlyProcessor(TaskProcessor taskProcessor) : base(taskProcessor)
        {
            var curMonth = (MonthsInYear)DateTime.Today.Month - 1;
            RecurType = YearlylyRecurTypes.EveryMonthDayX;
            EveryMonthType = curMonth;
            MonthDay = DateTime.Today.Day;
            WeekType = DateTime.Today.GetWeekType();
            DayType = DateTime.Today.GetDayType();
            WeekMonthType = curMonth;
            RegenYearsAfterCompleted = 1;
        }

        public override void DoMarkComplete()
        {
            switch (RecurType)
            {
                case YearlylyRecurTypes.EveryMonthDayX:
                    TaskProcessor.StartDate = GetDayXOfEvery(TaskProcessor.StartDate);
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    TaskProcessor.StartDate = GetWeekTypeDayTypeMonthType(TaskProcessor.StartDate);
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    var yearsToAdd = RegenYearsAfterCompleted;
                    TaskProcessor.StartDate = DateTime.Today.AddYears(yearsToAdd);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void AdjustStartDate()
        {
            switch (RecurType)
            {
                case YearlylyRecurTypes.EveryMonthDayX:
                    TaskProcessor.StartDate = GetDayXOfEvery(TaskProcessor.StartDate, 0);
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    TaskProcessor.StartDate = GetWeekTypeDayTypeMonthType(TaskProcessor.StartDate, 0);
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void LoadRecurProcessor(TlTask task)
        {
            if (task.RecurYearly.Any())
            {
                var recurYearly = task.RecurYearly.FirstOrDefault();
                if (recurYearly != null)
                {
                    RecurType = (YearlylyRecurTypes)recurYearly.RecurType;
                    EveryMonthType = (MonthsInYear)recurYearly.EveryMonthType.GetValueOrDefault();
                    MonthDay = recurYearly.MonthDay.GetValueOrDefault();
                    WeekType = (WeekTypes)recurYearly.WeekType.GetValueOrDefault();
                    DayType = (DayTypes)recurYearly.DayType.GetValueOrDefault();
                    WeekMonthType = (MonthsInYear)recurYearly.WeekMonthType.GetValueOrDefault();
                    RegenYearsAfterCompleted = recurYearly.RegenYearsAfterCompleted.GetValueOrDefault();
                }
            }
        }

        public override bool SaveRecurProcessor(TlTask task, IDbContext context)
        {
            var tlYearly = new TlTaskRecurYearly
            {
                TaskId = task.Id,
                RecurType = (byte)RecurType,
                EveryMonthType = (byte)EveryMonthType,
                MonthDay = MonthDay,
                WeekType = (byte)WeekType,
                DayType = (byte)DayType,
                WeekMonthType = (byte)WeekMonthType,
                RegenYearsAfterCompleted = RegenYearsAfterCompleted,
            };

            return context.AddSaveEntity(tlYearly, "");
        }

        public override string GetRecurText()
        {
            var text = string.Empty;

            var monthTypeTrans = new EnumFieldTranslation();
            monthTypeTrans.LoadFromEnum<MonthsInYear>();

            switch (RecurType)
            {
                case YearlylyRecurTypes.EveryMonthDayX:
                    var monthText = monthTypeTrans.TypeTranslations
                        .FirstOrDefault(p => p.NumericValue == (int)EveryMonthType)
                        .TextValue;

                    text = $"Every Year on {monthText} {MonthDay}";
                    break;
                case YearlylyRecurTypes.TheNthWeekdayTypeOfMonth:
                    var weekTypeTrans = new EnumFieldTranslation();
                    weekTypeTrans.LoadFromEnum<WeekTypes>();
                    var weekText = weekTypeTrans.TypeTranslations
                        .FirstOrDefault(p => p.NumericValue == (int)WeekType)
                        .TextValue;

                    var dayTypeTrans = new EnumFieldTranslation();
                    dayTypeTrans.LoadFromEnum<DayTypes>();
                    var dayText = dayTypeTrans.TypeTranslations
                        .FirstOrDefault(p => p.NumericValue == (int)DayType)
                        .TextValue;

                    var monthText1 = monthTypeTrans.TypeTranslations
                        .FirstOrDefault(p => p.NumericValue == (int)WeekMonthType)
                        .TextValue;

                    text = $"Every Year on the {weekText} {dayText} of {monthText1}";
                    break;
                case YearlylyRecurTypes.RegenerateXYearsAfterCompleted:
                    text = $"Every {RegenYearsAfterCompleted} Year(s) After the Task Has Been Completed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return text;
        }

        private DateTime GetDayXOfEvery(DateTime startDate, int addYears = 1)
        {
            startDate = new DateTime(startDate.Year, (int)EveryMonthType + 1, 1);
            startDate = startDate.AddYears(addYears);
            var lastDayOfMonth = startDate.GetLastDayOfMonth();
            var newDay = MonthDay;

            if (MonthDay > lastDayOfMonth)
            {
                newDay = lastDayOfMonth;
            }

            return new DateTime(startDate.Year, startDate.Month, newDay);
        }

        private DateTime GetWeekTypeDayTypeMonthType(DateTime startDate, int addYears = 1)
        {
            startDate = new DateTime(startDate.Year, (int)WeekMonthType + 1, 1);
            startDate = startDate.AddYears(addYears);

            var taskProc = new TaskProcessor();
            taskProc.RecurType = TaskRecurTypes.Monthly;
            taskProc.StartDate = startDate;

            taskProc.MonthlyProcessor.RecurType = MonthlyRecurTypes.XthWeekdayOfEveryYMonths;
            taskProc.MonthlyProcessor.DayType = DayType;
            taskProc.MonthlyProcessor.WeekType = WeekType;

            return taskProc.MonthlyProcessor.GetNthWeekdayOfEveryMonth(startDate, 0);
        }
    }
}
