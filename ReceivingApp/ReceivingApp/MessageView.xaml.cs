using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ReceivingApp {
    internal partial class MessageView : UserControl {

        static System.Windows.Forms.KeysConverter keysConverter = new System.Windows.Forms.KeysConverter();

        public MessageView(Tuple<Keys, int, bool, DateTime> receivedTuple) {
            InitializeComponent();

            CultureInfo cultureInfo = new CultureInfo(receivedTuple.Item2);

            string key = (receivedTuple.Item1 >= Keys.KEY_0 && receivedTuple.Item1 <= Keys.KEY_Z) ?
                    keysConverter.ConvertToString(null, cultureInfo, (int)receivedTuple.Item1) :
                    receivedTuple.Item1.ToString();

            dateTimeTextBlock.Text = receivedTuple.Item4.ToString();
            textBlock.Text = string.Format(
                "Нажатие клавиши: {0}{3}Зажата клавиша SHIFT: {1}{3}Раскладка: {2}",
                key,
                receivedTuple.Item3 ? "Да" : "Нет",
                cultureInfo.DisplayName, Environment.NewLine);

            Opacity = 0.0;
            Loaded += KeyView_Loaded;
        }

        public MessageView(string text, Brush color) {
            InitializeComponent();

            dateTimeTextBlock.Text = DateTime.Now.ToString();
            textBlock.Text = text;
            textBlock.Foreground = color;

            Opacity = 0.0;
            Loaded += KeyView_Loaded;
        }

        void KeyView_Loaded(object sender, RoutedEventArgs e) {
            Loaded -= KeyView_Loaded;

            DoubleAnimation animation = new DoubleAnimation(1.0, TimeSpan.FromSeconds(0.3));
            animation.Freeze();
            BeginAnimation(OpacityProperty, animation);
        }
    }
}