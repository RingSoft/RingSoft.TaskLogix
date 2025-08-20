using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RingSoft.TaskLogix.App
{
    public class TlControlRadioButton : RadioButton
    {
        public static readonly DependencyProperty NextDownRadioButtonProperty =
            DependencyProperty.Register(nameof(NextDownRadioButton), typeof(TlControlRadioButton), typeof(TlControlRadioButton),
                new FrameworkPropertyMetadata(NextDownRadioButtonChangedCallback));

        /// <summary>
        /// Gets or sets the UI label.  This is a bind-able property.
        /// </summary>
        /// <value>The UI label.</value>
        public TlControlRadioButton NextDownRadioButton
        {
            get { return (TlControlRadioButton)GetValue(NextDownRadioButtonProperty); }
            set { SetValue(NextDownRadioButtonProperty, value); }
        }

        /// <summary>
        /// UIs the label changed callback.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="args">The <see cref="DependencyPropertyChangedEventArgs" /> instance containing the event data.</param>
        private static void NextDownRadioButtonChangedCallback(DependencyObject obj,
            DependencyPropertyChangedEventArgs args)
        {
            var recurRadioButton = (TlControlRadioButton)obj;
            if (recurRadioButton != null)
            {

            }
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
            };
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
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
                    e.Handled |= true;
                }
            }

            base.OnKeyDown(e);
        }
    }
}
