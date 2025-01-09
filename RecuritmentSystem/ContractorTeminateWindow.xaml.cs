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
    /// Interaction logic for TeminateWindow.xaml
    /// </summary>
    public partial class TeminateWindow : Window
    {
        public DateTime terminationDate;
        public bool IsConfirmed;

        Management _service;
        public TeminateWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            IsConfirmed = false;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (TextboxTemination.Text.Length != 10)
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }
            else if (TextboxTemination.Text[2] != '/' || TextboxTemination.Text[5] != '/')
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            string strDay = $"{TextboxTemination.Text[0]}{TextboxTemination.Text[1]}";
            string strMonth = $"{TextboxTemination.Text[3]}{TextboxTemination.Text[4]}";
            string strYear = $"{TextboxTemination.Text[6]}{TextboxTemination.Text[7]}{TextboxTemination.Text[8]}{TextboxTemination.Text[9]}";
            string dateString = $"{strMonth}/{strDay}/{strYear}";

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateString, culture, styles, out DateTime temiDate))
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            if (temiDate.Year < 1950 || temiDate.Year > 2050)
            {
                MessageBox.Show("NOTE: Please enter the end date between Year 1950 and 2050");
                return;
            }

            terminationDate = temiDate;
            IsConfirmed = true;
            Close();
        }

        private void TextboxTemination_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextboxTemination.Text = string.Empty;
        }
    }
}
