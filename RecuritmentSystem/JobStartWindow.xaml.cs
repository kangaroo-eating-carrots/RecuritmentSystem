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
    /// JobStartWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class JobStartWindow : Window
    {
        public DateTime StartDate;
        public bool IsConfirmed;
        public JobStartWindow()
        {
            InitializeComponent();
            IsConfirmed = false;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (TextboxStart.Text.Length != 10)
            {
                MessageBox.Show("Warning: Please enter the end date (DD/MM/YYYY)");
                return;
            }
            else if (TextboxStart.Text[2] != '/' || TextboxStart.Text[5] != '/')
            {
                MessageBox.Show("Warning: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            string strDay = $"{TextboxStart.Text[0]}{TextboxStart.Text[1]}";
            string strMonth = $"{TextboxStart.Text[3]}{TextboxStart.Text[4]}";
            string strYear = $"{TextboxStart.Text[6]}{TextboxStart.Text[7]}{TextboxStart.Text[8]}{TextboxStart.Text[9]}";
            string dateString = $"{strMonth}/{strDay}/{strYear}";

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateString, culture, styles, out DateTime startDate))
            {
                MessageBox.Show("Warning: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            if (startDate.Year < 1950 || startDate.Year > 2050)
            {
                MessageBox.Show("Warning: Please enter the end date between Year 1950 and 2050");
                return;
            }

            StartDate = startDate;
            IsConfirmed = true;
            Close();
        }

        private void TextboxStart_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextboxStart.Text = string.Empty;
        }
    }
}
