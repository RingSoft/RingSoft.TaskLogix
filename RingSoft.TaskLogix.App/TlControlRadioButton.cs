using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RingSoft.TaskLogix.App
{
    public class TlControlRadioButton : RadioButton
    {
        public static readonly DependencyProperty NextDownRadioButtonProperty =
            DependencyProperty.Register(nameof(NextDownRadioButton), typeof(TlControlRadioButton), typeof(TlControlRadioButton),
                new FrameworkPropertyMetadata());

        public TlControlRadioButton NextDownRadioButton
        {
            get { return (TlControlRadioButton)GetValue(NextDownRadioButtonProperty); }
            set { SetValue(NextDownRadioButtonProperty, value); }
        }

        public static readonly DependencyProperty TabRightControlProperty =
            DependencyProperty.Register(nameof(TabRightControl), typeof(Control), typeof(TlControlRadioButton),
                new FrameworkPropertyMetadata());

        public Control TabRightControl
        {
            get { return (Control)GetValue(TabRightControlProperty); }
            set { SetValue(TabRightControlProperty, value); }
        }

        public TlControlRadioButton NextUpRadioButton { get; private set; }

        public TlControlRadioButton()
        {
            Loaded += (sender, args) =>
            {
                if (NextDownRadioButton != null)
                {
                    NextDownRadioButton.NextUpRadioButton = this;
                }

                if (TabRightControl != null)
                {
                    TabRightControl.KeyDown += (o, eventArgs) =>
                    {
                        if (eventArgs.Key == Key.Tab)
                        {
                            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                            {
                                Focus();
                                eventArgs.Handled = true;
                            }
                        }
                    };
                }
            };
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!e.Handled)
            {
                if (e.Key == Key.Tab)
                {
                    if (TabRightControl != null && TabRightControl.IsEnabled)
                    {
                        if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)))
                        {
                            TabRightControl.Focus();
                            e.Handled = true;
                        }
                    }
                }

                if (e.Key == Key.Down)
                {
                    if (NextDownRadioButton != null)
                    {
                        NextDownRadioButton.Focus();
                        e.Handled |= true;
                    }
                }

                if (e.Key == Key.Up)
                {
                    if (NextUpRadioButton != null)
                    {
                        NextUpRadioButton.Focus();
                        e.Handled = true;
                    }
                }
            }

            base.OnKeyDown(e);
        }
    }
}
