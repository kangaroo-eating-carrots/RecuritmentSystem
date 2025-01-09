using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecuritmentSystem20122791
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Management _service;

        public MainWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            //Set up the default selections for Comboboxes
            ResetWindow();

            MessageBox.Show("[Information}\nPassword is 0000\nIf double click listbox item, it will shows the detail of it");

        }

        private void ResetWindow()
        {
            ComboboxStatus.SelectedItem = JobAllStatus;
            ComboboxTypes.SelectedItem = JobAllType;
            TextboxMinCost.Text = string.Empty;
            TextboxMaxCost.Text = string.Empty;
            TextboxJobID.Text = string.Empty;
            TextboxConID.Text = string.Empty;


            ListboxJobs.ItemsSource = _service.NotDeletedJobs;
            ListboxJobs.Items.Refresh();
        }

        private void ButtonNewJob_Click(object sender, RoutedEventArgs e)
        {
            NewJobWindow op1 = new NewJobWindow();
            op1.ShowDialog();

            if (op1.isDone)
            {
                ResetWindow();
            }
        }

        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {

            if (ListboxJobs.SelectedItem is null)
            {
                MessageBox.Show("NOTE: Please select the job to assign first");
                return;
            }
            else
            {
                foreach (Job job in _service.NotDeletedJobs)
                {
                    if (job == ListboxJobs.SelectedItem)
                    {
                        _service.TargetJob = job;

                        if (!_service.CheckValidAssignJob(_service.TargetJob))
                        {
                            return;
                        }
                        
                    }
                }
            }

            JobStartWindow op1 = new JobStartWindow();
            op1.ShowDialog();

            if (!op1.IsConfirmed)
            {
                return;
            }

            _service.CheckAvailableContractors(_service.TargetJob, op1.StartDate);

            AssignWindow op2 = new AssignWindow();
            op2.ShowDialog();

            if (op2.IsDone)
            {
                _service.AssignJob(_service.TargetJob, _service.TargetContractor, op1.StartDate);
                ResetWindow();
            }
        }

        private void ButtonContractor_Click(object sender, RoutedEventArgs e)
        {
            ContractorWindow op1 = new ContractorWindow();
            op1.ShowDialog();
        }

        private void ButtonComplete_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxJobs.SelectedItem is null)
            {
                MessageBox.Show("NOTE: Please select the job to assign first.");
                return;
            }


            foreach (Job job in _service.AllJobs)
            {
                if (ListboxJobs.SelectedItem == job)
                {
                    _service.TargetJob = job;
                    
                }
            }

            if (!_service.CheckValidCompleteJob(_service.TargetJob))
            {
                return;
            }


            EndDateWindow op1 = new EndDateWindow();
            op1.ShowDialog();

            if (op1.IsConfirmed)
            {
                _service.CompleteJob(_service.TargetJob, op1.EndDate);
            }

            if (_service.IsDone)
            {
                ResetWindow();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxJobs.SelectedItem is null)
            {
                MessageBox.Show("NOTE: Please select the job to cancel");
                return;
            }

            foreach (Job job in _service.AllJobs)
            {
                if (ListboxJobs.SelectedItem == job)
                {
                    _service.TargetJob = job;
                    
                }
            }

            if (!_service.CheckValidCancelJob(_service.TargetJob))
            {
                return;
            }

            EndDateWindow op1 = new EndDateWindow();
            op1.ShowDialog();

            if (op1.IsConfirmed)
            {
                _service.CacncelJob(_service.TargetJob, op1.EndDate);
            }

            if (_service.IsDone)
            {
                ResetWindow();
            }

        }


        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxJobs.SelectedItem is null)
            {
                MessageBox.Show("NOTE: Please select the job to delete");
                return;
            }

            PasswordWindow op1 = new PasswordWindow();
            op1.ShowDialog();

            if (!op1.IsCorrect)
            {
                return;
            }

            foreach (Job job in _service.AllJobs)
            {
                if (ListboxJobs.SelectedItem == job)
                {
                    _service.TargetJob = job;
                }
            }

            _service.DeleteJob(_service.TargetJob);

            if (_service.IsDone)
            {
                ResetWindow();
            }
            
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ListboxJobs.SelectedItem is null)
            {
                MessageBox.Show("NOTE: Please select the job to edit");
                return;
            }

            MessageBox.Show(("Warning: Edit function might require manual edition of related contractor for reconciliation"));

            foreach (Job job in _service.AllJobs)
            {
                if (job == ListboxJobs.SelectedItem)
                {
                    _service.TargetJob = job;
                }
            }


            PasswordWindow op1 = new PasswordWindow();
            op1.ShowDialog();

            if (!op1.IsCorrect)
            {
                return;
            }

            JobEditWindow op2 = new JobEditWindow();
            op2.ShowDialog();

            if (_service.IsDone)
            {
                ResetWindow();
            }

        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetWindow();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string jobStatus;
            string jobType;
            string jobMinCost;
            string jobMaxCost;
            string jobID;
            string jobContractorID;


            jobStatus = string.Empty;

            if (ComboboxStatus.SelectedItem == JobAllStatus)
            {
                jobStatus = "All";
            }
            else if (ComboboxStatus.SelectedItem == JobCurrentAll)
            {
                jobStatus = "CurrentAll";
            }
            else if (ComboboxStatus.SelectedItem == JobAssigned)
            {
                jobStatus = "CurrentAssigned";
            }
            else if (ComboboxStatus.SelectedItem == JobNotAssigned)
            {
                jobStatus = "CurrentNotAssigned";
            }
            else if (ComboboxStatus.SelectedItem == JobCompleted)
            {
                jobStatus = "Completed";
            }
            else if (ComboboxStatus.SelectedItem == JobSCancelled)
            {
                jobStatus = "Cancelled";
            }

            jobType = string.Empty;

            if (ComboboxTypes.SelectedItem == JobAllType)
            {
                jobType = "All";
            }
            else if (ComboboxTypes.SelectedItem == JobDrill)
            {
                jobType = "Drill";
            }
            else if (ComboboxTypes.SelectedItem == JobExcavator)
            {
                jobType = "Excavator";
            }
            else if (ComboboxTypes.SelectedItem == JobTruck)
            {
                jobType = "Truck";
            }
            else if (ComboboxTypes.SelectedItem == JobCrusher)
            {
                jobType = "Crusher";
            }
            else if (ComboboxTypes.SelectedItem == JobReclaimer)
            {
                jobType = "Reclaimer";
            }
            else if (ComboboxTypes.SelectedItem == JobTrain)
            {
                jobType = "Train";
            }

            jobMinCost = string.Empty;
            if (!string.IsNullOrEmpty(TextboxMinCost.Text))
            {
                jobMinCost = TextboxMinCost.Text;
            }

            jobMaxCost = string.Empty;
            if (!string.IsNullOrEmpty(TextboxMaxCost.Text))
            {
                jobMaxCost = TextboxMaxCost.Text;
            }

            jobID = string.Empty;
            if (!string.IsNullOrEmpty(TextboxJobID.Text))
            {
                jobID = TextboxJobID.Text;
            }

            jobContractorID = string.Empty;
            if (!string.IsNullOrEmpty(TextboxConID.Text))
            {
                jobContractorID = TextboxConID.Text;
            }

            _service.SearchJob(jobStatus, jobType, jobMinCost, jobMaxCost, jobID, jobContractorID);


            if (_service.IsDone)
            {
                ListboxJobs.ItemsSource = _service.SearchedJobs;
                ListboxJobs.Items.Refresh();
            }
        }

        private void ListboxJobs_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (ListboxJobs.SelectedItem == null)
            {
                return;
            }
            else
            {
                foreach (Job job in _service.AllJobs)
                {
                    if (ListboxJobs.SelectedItem == job)
                    {
                        _service.TargetJob = job;
                    }
                }

                JobDetailWindow op1 = new JobDetailWindow();
                op1.ShowDialog();
            }


        }

    }


}