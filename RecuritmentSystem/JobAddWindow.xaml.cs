using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecuritmentSystem20122791
{
    /// <summary>
    /// Interaction logic for NewJobWindow.xaml
    /// </summary>
    public partial class NewJobWindow : Window
    {
        private string jobType;
        private string jobLength;
        private string jobCost;
        private string jobDescription;
        private string startDescription;

        public bool isDone;

        Management _service;

        public NewJobWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            startDescription = TextboxDescription.Text;
            isDone = false;
        }


        private void TextboxDescription_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (TextboxDescription.Text == startDescription) TextboxDescription.Text = string.Empty;
            
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (ComboboxNewTypes.SelectedItem == null)
            {
                jobType = string.Empty;
            }
            else if (ComboboxNewTypes.SelectedItem == JobDrill)
            {
                jobType = "Drill";
            }
            else if (ComboboxNewTypes.SelectedItem == JobExcavator)
            {
                jobType = "Excavator";
            }
            else if (ComboboxNewTypes.SelectedItem == JobTruck)
            {
                jobType = "Truck";
            }
            else if (ComboboxNewTypes.SelectedItem == JobCrusher)
            {
                jobType = "Crusher";
            }
            else if (ComboboxNewTypes.SelectedItem == JobReclaimer)
            {
                jobType = "Reclaimer";
            }
            else if (ComboboxNewTypes.SelectedItem == JobTrain)
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


            if (TextboxDescription.Text == startDescription || string.IsNullOrEmpty(TextboxDescription.Text))
            {
                jobDescription = string.Empty;
            }
            else
            {
                jobDescription = TextboxDescription.Text;
            }

            _service.AddJob(jobType, jobLength, jobCost, jobDescription);

            if (_service.IsDone)
            {
                isDone = true;
                Close();
            }

        }
    }
}
