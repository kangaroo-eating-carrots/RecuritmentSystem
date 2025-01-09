using System;
using System.Collections.Generic;
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
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public bool IsCorrect;

        Management _service;

        public PasswordWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            IsCorrect = false;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!_service.CheckPassword(TextboxPassword.Text))
            {
                MessageBox.Show("Warning: Wrong password");
                return;
            }

            else
            {
                IsCorrect = true;
                Close();
            }
        }
    }
}
