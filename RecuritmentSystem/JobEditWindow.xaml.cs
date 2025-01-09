using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for JobEditWindow.xaml
    /// </summary>
    public partial class JobEditWindow : Window
    {
        public int jobLength;
        public int JobCost;
        public DateTime? JobStartDate;
        public DateTime? JobEndDate;
        public JobStatus StatusOfJob;
        public string JobDescription;

        Management _service;


        public JobEditWindow()
        {
            InitializeComponent();

            _service = Management.Instance;


            if (_service.TargetJob.Type == JobTypes.Drill)
            {
                ComboboxTypes.SelectedItem = JobDrill;
            }
            else if (_service.TargetJob.Type == JobTypes.Excavator)
            {
                ComboboxTypes.SelectedItem = JobExcavator;
            }
            else if (_service.TargetJob.Type == JobTypes.Truck)
            {
                ComboboxTypes.SelectedItem = JobTruck;
            }
            else if (_service.TargetJob.Type == JobTypes.Crusher)
            {
                ComboboxTypes.SelectedItem = JobCrusher;
            }
            else if (_service.TargetJob.Type == JobTypes.Reclaimer)
            {
                ComboboxTypes.SelectedItem = JobReclaimer;
            }
            else if (_service.TargetJob.Type == JobTypes.Train)
            {
                ComboboxTypes.SelectedItem = JobTrain;
            }

            TextboxLength.Text = $"{_service.TargetJob.Length}";

            TextboxCost.Text = $"{_service.TargetJob.Cost}";

            if (_service.TargetJob.IsAssigned == true)
            {
                DateTime tempDate = DateTime.Parse(_service.TargetJob.StartDate.ToString());
                TextboxStart.Text = $"{tempDate.Day:00}/{tempDate.Month:00}/{tempDate.Year}";
            }
            else
            {
                TextboxStart.Text = string.Empty;
            }

            if (_service.TargetJob.Status == JobStatus.Cancelled || _service.TargetJob.Status == JobStatus.Completed)
            {
                DateTime tempDate = DateTime.Parse(_service.TargetJob.EndDate.ToString());
                TextboxEnd.Text = $"{tempDate.Day:00}/{tempDate.Month:00}/{tempDate.Year}";
            }
            else
            {
                TextboxEnd.Text = string.Empty;
            }

            if (_service.TargetJob.Status == JobStatus.Current)
            {
                ComboboxStatus.SelectedItem = CurrentJob;
            }
            else if (_service.TargetJob.Status == JobStatus.Completed)
            {
                ComboboxStatus.SelectedItem = CompleteJob;
            }
            else if (_service.TargetJob.Status == JobStatus.Cancelled)
            {
                ComboboxStatus.SelectedItem = CancelledJob;
            }

            TextboxDescription.Text = _service.TargetJob.Description;

        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            string jobType;
            string jobLength;
            string jobCost;
            string jobStartDate;
            string jobEndDate;
            string jobStatus;
            string jobDescription;

            jobType = string.Empty;

            if (ComboboxTypes.SelectedItem == JobDrill)
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

            if (string.IsNullOrEmpty(TextboxLength.Text))
            {
                jobLength = string.Empty;
            }
            else
            {
                jobLength = TextboxLength.Text;
            }

            if (string.IsNullOrEmpty(TextboxCost.Text))
            {
                jobCost = string.Empty;
            }
            else
            {
                jobCost = TextboxCost.Text;
            }

            if (string.IsNullOrEmpty(TextboxStart.Text))
            {
                jobStartDate = string.Empty;
            }
            else
            {
                jobStartDate = TextboxStart.Text;
            }

            if (string.IsNullOrEmpty(TextboxEnd.Text))
            {
                jobEndDate = string.Empty;
            }
            else
            {
                jobEndDate = TextboxEnd.Text;
            }


            
            if (string.IsNullOrEmpty(TextboxDescription.Text))
            {
                jobDescription = string.Empty;
            }
            else
            {
                jobDescription = TextboxDescription.Text;
            }


            jobStatus = string.Empty;

            if (ComboboxStatus.SelectedItem == CurrentJob)
            {
                jobStatus = "Current";
            }
            else if (ComboboxStatus.SelectedItem == CompleteJob)
            {
                jobStatus = "Completed";
            }
            else if (ComboboxStatus.SelectedItem == CancelledJob)
            {
                jobStatus = "Cancelled";
            }

            _service.EditJob(_service.TargetJob, jobType, jobLength, jobCost, jobStartDate, jobEndDate, jobStatus, jobDescription);

            if (_service.IsDone)
            {
                Close();
            }
            
        }
        
    }
}
