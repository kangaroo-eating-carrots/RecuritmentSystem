using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ContractorWindow.xaml
    /// </summary>
    
    

    public partial class ContractorWindow : Window
    {



        Management _service;

        public ContractorWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            ListboxContractors.ItemsSource = _service.NotDeletedContractors;
            ListboxContractors.Items.Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddContractorWindow op1 = new AddContractorWindow();
            op1.ShowDialog();

            if (op1.isDone)
            {
                ListboxContractors.ItemsSource = _service.NotDeletedContractors;
                ListboxContractors.Items.Refresh();
            }

        }

        private void ButtonTerminate_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxContractors.SelectedItem == null)
            {
                MessageBox.Show("NOTE: No contractor selected");
                return;
            }

            foreach (Contractor contractor in _service.AllContractors)
            {
                if (contractor == ListboxContractors.SelectedItem)
                {
                    _service.TargetContractor = contractor;
                }
            }

            if (!_service.CheckValidTeminateContractor(_service.TargetContractor))
            {
                return;
            }



            TeminateWindow op1 = new TeminateWindow();
            op1.ShowDialog();

            if (!op1.IsConfirmed)
            {
                return;
            }
            else
            {
                _service.TerminateContractor(_service.TargetContractor, op1.terminationDate);
            }

            ListboxContractors.ItemsSource = _service.NotDeletedContractors;
            ListboxContractors.Items.Refresh();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            

            if (ListboxContractors.SelectedItem == null)
            {
                MessageBox.Show("NOTE: No contractor selected");
                return;
            }

            foreach (Contractor contractor in _service.AllContractors)
            {
                if (contractor == ListboxContractors.SelectedItem)
                {
                    _service.TargetContractor = contractor;
                }
            }

            if (!_service.CheckValidDeleteContractor(_service.TargetContractor))
            {
                return;
            }

            PasswordWindow op1 = new PasswordWindow();
            op1.ShowDialog();

            if (!op1.IsCorrect)
            {
                return;
            }

            _service.DeleteContractor(_service.TargetContractor);

            ListboxContractors.ItemsSource = _service.NotDeletedContractors;
            ListboxContractors.Items.Refresh();

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxContractors.SelectedItem == null)
            {
                MessageBox.Show("NOTE: No contractor selected");
                return;
            }

            MessageBox.Show("NOTE: Edit function might require manual edition of related job for reconciliation");

            foreach (Contractor contractor in _service.AllContractors)
            {
                if (contractor == ListboxContractors.SelectedItem)
                {
                    _service.TargetContractor = contractor;
                }
            }

            PasswordWindow op1 = new PasswordWindow();
            op1.ShowDialog();

            if (!op1.IsCorrect)
            {
                return;
            }

            ContractorEditWindow op2 = new ContractorEditWindow();
            op2.ShowDialog();

            

            ListboxContractors.ItemsSource = _service.NotDeletedContractors;
            ListboxContractors.Items.Refresh();


        }

        private void ListboxContractors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
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
