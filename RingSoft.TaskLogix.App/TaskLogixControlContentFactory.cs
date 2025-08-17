using System.Windows;
using System.Windows.Controls;
using RingSoft.DbLookup;
using RingSoft.DbLookup.Controls.WPF;

namespace RingSoft.TaskLogix.App
{
    public class TaskLogixControlContentFactory : LookupControlContentTemplateFactory
    {
        private Application _application;

        public TaskLogixControlContentFactory(Application application)
        {
            _application = application;
        }

        public override Image GetImageForAlertLevel(AlertLevels alertLevel)
        {
            try
            {
                Image result = null;
                switch (alertLevel)
                {
                    case AlertLevels.Green:
                        result = _application.Resources["GreenAlertImage"] as Image;
                        return result;
                    case AlertLevels.Yellow:
                        result = _application.Resources["YellowAlertImage"] as Image;
                        return result;
                    case AlertLevels.Red:
                        result = _application.Resources["GreenAlertImage"] as Image;
                        return result;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(alertLevel), alertLevel, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return base.GetImageForAlertLevel(alertLevel);
        }
    }
}
