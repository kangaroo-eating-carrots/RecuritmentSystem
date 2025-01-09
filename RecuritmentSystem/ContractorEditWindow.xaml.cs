using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Interaction logic for ContractorEditWindow.xaml
    /// </summary>
    public partial class ContractorEditWindow : Window
    {

        public List<JobTypes> Skills;
        JobTypes skillType;

        Management _service;

        public ContractorEditWindow()
        {
            InitializeComponent();
            _service = Management.Instance;

            TextboxName.Text = _service.TargetContractor.Name;

            Skills = _service.TargetContractor.Skills;
            ListboxSkills.ItemsSource = Skills;
            ListboxSkills.Items.Refresh();

            TextboxMinRate.Text = $"{_service.TargetContractor.MinRate}";
            TextboxMaxRate.Text = $"{_service.TargetContractor.MaxRate}";

            TextboxStart.Text = $"{_service.TargetContractor.StartDate.Day:00}/{_service.TargetContractor.StartDate.Month:00}/{_service.TargetContractor.StartDate.Year}";

            TextboxDescription.Text = _service.TargetContractor.Description;

            if (_service.TargetContractor.Status == ContractorStatus.ContractEnd)
            {
                DateTime tempDate = DateTime.Parse(_service.TargetContractor.EndDate.ToString());
                TextboxEnd.Text = $"{tempDate.Day:00}/{tempDate.Month:00}/{tempDate.Year}";
            }
            else
            {
                TextboxEnd.Text = string.Empty;
            }

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxSkills.SelectedItem == null)
            {
                MessageBox.Show("NOTE: No skill selected");
                return;
            }
            else if (ListboxSkills.Items.Count == 3)
            {
                MessageBox.Show("NOTE: No more than 3 skills");
                return;
            }



            if (ComboboxSkills.SelectedItem == JobDrill)
            {
                skillType = JobTypes.Drill;
            }
            else if (ComboboxSkills.SelectedItem == JobExcavator)
            {
                skillType = JobTypes.Excavator;
            }
            else if (ComboboxSkills.SelectedItem == JobTruck)
            {
                skillType = JobTypes.Truck;
            }
            else if (ComboboxSkills.SelectedItem == JobCrusher)
            {
                skillType = JobTypes.Crusher;
            }
            else if (ComboboxSkills.SelectedItem == JobReclaimer)
            {
                skillType = JobTypes.Reclaimer;
            }
            else if (ComboboxSkills.SelectedItem == JobTrain)
            {
                skillType = JobTypes.Train;
            }

            foreach (JobTypes skill in Skills)
            {
                if (skill == skillType)
                {
                    MessageBox.Show("NOTE: Already added");
                    return;
                }
            }

            Skills.Add(skillType);

            ListboxSkills.ItemsSource = Skills;
            ListboxSkills.Items.Refresh();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            List <JobTypes> tempSkills = new List <JobTypes>();
            string tempStr;

            if (ListboxSkills.SelectedItem == null)
            {
                MessageBox.Show("Warning: No skill selected");
                return;
            }
            else
            {
                tempStr = ListboxSkills.SelectedItem.ToString();
            }


            foreach (JobTypes skill in Skills)
            {
                if (tempStr != skill.ToString())
                {
                    tempSkills.Add(skill);
                }
            }

            Skills = tempSkills;

            ListboxSkills.ItemsSource = Skills;
            ListboxSkills.Items.Refresh();


        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            string contractorName;

            if (string.IsNullOrEmpty(TextboxName.Text))
            {
                contractorName = string.Empty;
            }
            else
            {
                contractorName = TextboxName.Text;
            }


            string contactorMinRate;

            if (string.IsNullOrEmpty(TextboxMinRate.Text))
            {
                contactorMinRate = string.Empty;
            }
            else
            {
                contactorMinRate = TextboxMinRate.Text;
            }


            string contactorMaxRate;

            if (string.IsNullOrEmpty(TextboxMaxRate.Text))
            {
                contactorMaxRate = string.Empty;
            }
            else
            {
                contactorMaxRate = TextboxMaxRate.Text;
            }


            string contractorStartDate;

            if (string.IsNullOrEmpty(TextboxStart.Text))
            {
                contractorStartDate = string.Empty;
            }
            else
            {
                contractorStartDate = TextboxStart.Text;
            }

            string contractorEndDate;

            if (string.IsNullOrEmpty(TextboxEnd.Text))
            {
                contractorEndDate = string.Empty;
            }
            else
            {
                contractorEndDate = TextboxEnd.Text;
            }

            List<JobTypes> contractorSkills = Skills;

            _service.EditContractor(_service.TargetContractor, contractorName, contractorSkills, contactorMinRate, contactorMaxRate, contractorStartDate, contractorEndDate);

            if (_service.IsDone)
            {
                Close();
            }
        }
    }
}
