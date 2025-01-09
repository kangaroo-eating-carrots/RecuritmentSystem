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
    /// Interaction logic for AssignWindow.xaml
    /// </summary>
    public partial class AssignWindow : Window
    {

        public bool IsAssignedSucessfully;
        public int TempContractorID;
        public string TempContractorName;


        public bool IsDone;

        Management _service;

        public AssignWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            ListboxContractors.ItemsSource = _service.AvailableContractors;
            ListboxContractors.Items.Refresh();
        }

        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxContractors.SelectedItem == null)
            {
                MessageBox.Show("NOTE: No contractor selected");
                return;
            }


            foreach (Contractor contractor in _service.AllContractors)
            {
                if (ListboxContractors.SelectedItem == contractor)
                {
                    _service.TargetContractor = contractor;
                }
            }

            IsDone = true;
            Close();
        }

        private void ButtonAuto_Click(object sender, RoutedEventArgs e)
        {
            //This button is clicked to find the contractor with the lowest daily rate and assgin the job to them
            IsDone = false;
            
            if (_service.AvailableContractors.Count == 0)
            {
                MessageBox.Show("NOTE: No available contractor");
                return;
            }
            else
            {
                IsDone = true;
                _service.AutoChoice();
                Close();
            }

        }

        private void ListboxContractors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Double click brings the detial of contractor

            if (ListboxContractors.SelectedItem == null)
            {
                return;
            }
            else
            {
                foreach (Contractor contractor in _service.AllContractors)
                {
                    if (ListboxContractors.SelectedItem == contractor)
                    {
                        _service.TargetContractor = contractor;
                    }
                }

                ContractorDetailWindow op1 = new ContractorDetailWindow();
                op1.ShowDialog();
            }
        }
    }
}
