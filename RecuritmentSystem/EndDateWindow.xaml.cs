using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecuritmentSystem20122791
{
    /// <summary>
    /// Interaction logic for EndDateWindow.xaml
    /// </summary>
    public partial class EndDateWindow : Window
    {
        public string EndDate;
        public bool IsConfirmed;
        public EndDateWindow()
        {
            InitializeComponent();
            IsConfirmed = false;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextboxEndDate.Text))
            {
                EndDate = string.Empty;
            }
            else
            {
                EndDate = TextboxEndDate.Text;
            }

            IsConfirmed = true;
            Close();
        }

        private void TextboxTemination_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
                TextboxEndDate.Text = string.Empty;
        }
    }
}
