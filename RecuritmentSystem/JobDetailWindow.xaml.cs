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
    /// Interaction logic for JobDetailWindow.xaml
    /// </summary>
    public partial class JobDetailWindow : Window
    {

        Management _service;

        public JobDetailWindow()
        {
            InitializeComponent();

            _service = Management.Instance;

            string tempString = string.Empty;

            foreach (Job job in _service.AllJobs)
            {
                if (job == _service.TargetJob)
                {
                    tempString = $"[Job ID {job.ID:000}; {job.Type} Repair]  ";

                    tempString = tempString + "Period: ";
                    if (!DateTime.TryParse(job.StartDate.ToString(), out DateTime startDate))
                    {
                        if (job.Status == JobStatus.Current)
                        {
                            tempString = tempString + "To Be Updated\n";
                        }
                        else
                        {
                            tempString = tempString + "N/A\n";
                        }
                    }
                    else
                    {
                        if (!DateTime.TryParse(job.EndDate.ToString(), out DateTime endDate))
                        {
                            tempString = tempString + $"{startDate.Day:00}/{startDate.Month:00}/{startDate.Year} ~ Not Yet Completed\n";
                        }
                        else
                        {
                            if (job.Status == JobStatus.Cancelled)
                            {
                                tempString = tempString + $"{startDate.Day:00}/{startDate.Month:00}/{startDate.Year} ~ {endDate.Day:00}/{endDate.Month:00}/{endDate.Year} (Cancelled)\n";
                            }
                            else
                            {
                                tempString = tempString + $"{startDate.Day:00}/{startDate.Month:00}/{startDate.Year} ~ {endDate.Day:00}/{endDate.Month:00}/{endDate.Year} (Completed)\n";
                            }
                        }
                    }

                    tempString = tempString + $"Repair Cost & Length: ${job.Cost} / {job.Length} Days (Daily Cost ${job.Cost / job.Length})\n";


                    if (!job.IsAssigned)
                    {
                        tempString = tempString + "Contractor ID: Not Yet Assigned\n";
                    }
                    else
                    {
                        if (job.ContractorID < 10)
                        {
                            tempString = tempString + $"Contractor Info: [Contractor ID 00{job.ContractorID}] ";
                        }
                        else if (job.ContractorID < 100)
                        {
                            tempString = tempString + $"Contractor Info: [Contractor ID 0{job.ContractorID}] ";
                        }
                        else
                        {
                            tempString = tempString + $"Contractor Info: [Contractor ID {job.ContractorID}] ";
                        }

                        foreach (Contractor contractor in _service.AllContractors)
                        {
                            if (contractor.ID == job.ContractorID)
                            {
                                tempString = tempString + $"{contractor.Name}\n";
                            }
                        }
                    }


                    if (job.Status == JobStatus.Current)
                    {
                        if (!job.IsAssigned)
                        {
                            tempString = tempString + $"Status: Not Yet Started\n";
                        }
                        else
                        {
                            tempString = tempString + $"Status: Currently In Process (Contractor ID {job.ContractorID})\n";
                        }
                    }
                    else
                    {
                        if (!DateTime.TryParse(job.EndDate.ToString(), out DateTime endDate))
                        {
                            tempString = tempString + $"Status: Cancelled Before Repair Start\n";
                        }
                        else
                        {
                            if (job.Status == JobStatus.Cancelled)
                            {
                                tempString = tempString + $"Status: Cancelled On {endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
                            }
                            else
                            {
                                tempString = tempString + $"Status: Completed On {endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
                            }
                        }
                    }

                    if (String.IsNullOrEmpty(job.Description))
                    {
                        tempString = tempString + "Description: N/A";
                    }
                    else
                    {
                        tempString = tempString + $"Description: {job.Description}";
                    }

                    LabelDetail.Content = tempString;
             
                }
            }
        }
    }
}
