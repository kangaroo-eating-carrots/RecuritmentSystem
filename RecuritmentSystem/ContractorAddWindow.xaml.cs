using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddContractorWindow.xaml
    /// </summary>

    public partial class AddContractorWindow : Window
    {

        string contractorName;
        List<string> contractorSkills;
        string contractorStartDate;
        string contractorMinRate;
        string contractorMaxRate;
        string contractorDescription;

        string startDescription;
        string startDateString;
        string skillType;

        public bool isDone;

        Management _service;



        public AddContractorWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            contractorSkills = new List<string>();

            startDescription = TextboxDescription.Text;
            startDateString = TextboxStart.Text;

            isDone = false;

        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxSkills.SelectedItem == null)
            {
                MessageBox.Show("Warning: No skill selected");
                return;
            }
            else if (ListboxSkills.Items.Count == 3)
            {
                MessageBox.Show("Warning: No more than 3 skills");
                return;
            }



            if (ComboboxSkills.SelectedItem == JobDrill)
            {
                skillType = "Drill";
            }
            else if (ComboboxSkills.SelectedItem == JobExcavator)
            {
                skillType = "Excavator";
            }
            else if (ComboboxSkills.SelectedItem == JobTruck)
            {
                skillType = "Truck";
            }
            else if (ComboboxSkills.SelectedItem == JobCrusher)
            {
                skillType = "Crusher";
            }
            else if (ComboboxSkills.SelectedItem == JobReclaimer)
            {
                skillType = "Reclaimer";
            }
            else if (ComboboxSkills.SelectedItem == JobTrain)
            {
                skillType = "Train";
            }

            foreach (string skill in contractorSkills)
            {
                if (skill == skillType)
                {
                    MessageBox.Show("NOTE: Already added");
                    return;
                }
            }

            contractorSkills.Add(skillType);

            ListboxSkills.ItemsSource = contractorSkills;
            ListboxSkills.Items.Refresh();

        }

        private void ButtonEnrol_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextboxName.Text))
            {
                contractorName = string.Empty;
            }
            else
            {
                contractorName = TextboxName.Text;
            }
            

            if (string.IsNullOrEmpty(TextboxMinRate.Text))
            {
                contractorMinRate = string.Empty;
            }
            else
            {
                contractorMinRate = TextboxMinRate.Text;
            }

            if (string.IsNullOrEmpty(TextboxMaxRate.Text))
            {
                contractorMaxRate = string.Empty;
            }
            else
            {
                contractorMaxRate = TextboxMaxRate.Text;
            }

            //From this point, the code to check whether the date input follow DD/MM/YYYY format.
            if (string.IsNullOrEmpty(TextboxStart.Text))
            {
                contractorStartDate = string.Empty;
            }
            else
            {
                contractorStartDate = TextboxStart.Text;
            }


            if (string.IsNullOrEmpty(TextboxDescription.Text) || TextboxDescription.Text == startDescription)
            {
                contractorDescription = string.Empty;
            }
            else
            {
                contractorDescription = TextboxDescription.Text;
            }


            _service.AddContractor(contractorName, contractorSkills, contractorStartDate, contractorMinRate, contractorMaxRate, contractorDescription);

            if (_service.IsDone)
            {
                isDone = true;
                Close();
            }

        }

        private void TextboxStart_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextboxStart.Text == startDateString)
            {
                TextboxStart.Text = string.Empty;
            }
        }

        private void TextboxDescription_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextboxDescription.Text == startDescription)
            {
                TextboxDescription.Text = string.Empty;
            }
        }
    }
}
