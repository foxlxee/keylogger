using System.Windows;

namespace ReceivingApp {
    internal partial class TextInputWindow : Window {

        public TextInputWindow() {
            InitializeComponent();
        }

        public void Append(string text) {
            tb.Text += text;
        }
    }
}