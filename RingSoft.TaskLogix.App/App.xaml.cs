﻿using System.Windows;

namespace RingSoft.TaskLogix.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });

            var appStart = new TaskLogixAppStart(this);
            appStart.Start();

            base.OnStartup(e);
        }
    }
}
