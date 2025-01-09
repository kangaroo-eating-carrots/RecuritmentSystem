using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for ContractorDetailWindow.xaml
    /// </summary>
    public partial class ContractorDetailWindow : Window
    {
        //Bring the detail of selected contractor

        Management _service;
        
        public ContractorDetailWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            foreach (Contractor contractor in _service.AllContractors)
            {
                if (contractor == _service.TargetContractor)
                {
                    string tempString;
                    tempString = $"[Contractor ID {contractor.ID:000}; Name {contractor.Name}]  ";
                    
                    tempString = tempString + $"Period: {contractor.StartDate.Day:00}/{contractor.StartDate.Month:00}/{contractor.StartDate.Year} ~ ";

                    if (DateTime.TryParse(contractor.EndDate.ToString(), out DateTime endDate))
                    {
                        tempString = tempString + $"{endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
                    }
                    else
                    {
                        tempString = tempString + "Now\n";
                    }

                    tempString = tempString + "Skill List: ";
                    int i = 0;
                    foreach (JobTypes skill in contractor.Skills)
                    {
                        ++i;
                        if (i < contractor.Skills.Count)
                        {
                            tempString = tempString + $"{skill}, ";
                        }
                        else
                        {
                            tempString = tempString + $"{skill}\n";
                        }
                    }


                    tempString = tempString + $"Dialy Rate: ${contractor.MinRate} ~ ${contractor.MaxRate}\n";


                    if (contractor.Status == ContractorStatus.Working)
                    {
                        tempString = tempString + $"Status: Currently Working On {contractor.CurrentJobType} Repair (Job ID {contractor.CurrentJobID})\n";
                    }
                    else if (contractor.Status == ContractorStatus.Waiting)
                    {
                        tempString = tempString + $"Status: Currently Not Working On Any Repair Job\n";
                    }
                    else
                    {
                        endDate = DateTime.Parse(contractor.EndDate.ToString());
                        tempString = tempString + $"Status: Constract Ended On {endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
                    }

                    if (String.IsNullOrEmpty(contractor.Description))
                    {
                        tempString = tempString + "Description: N/A\n";
                    }
                    else
                    {
                        tempString = tempString + $"Description: {contractor.Description}\n";
                    }

                    tempString = tempString + "\n[Job History] ";
                    List<string> jobHistory = new List<string>();

                    foreach (Job job in _service.AllJobs)
                    {
                        if(job.IsAssigned && job.ContractorID == contractor.ID)
                        {
                            jobHistory.Add($"Job ID {job.ID:000}; {job.Type} Repair ({job.Status})");
                        }
                    }

                    if (jobHistory.Count == 0)
                    {
                        tempString = tempString + "N/A";
                    }
                    else
                    {
                        foreach (string str in jobHistory)
                        {
                            tempString = tempString + "\n" + str;
                            
                        }
                    }                   

                    LabelDetail.Content = tempString;

                    

                }
            }
        }
    }
}
