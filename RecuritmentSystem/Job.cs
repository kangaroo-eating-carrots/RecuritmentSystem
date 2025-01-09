using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;

namespace RecuritmentSystem20122791
{
    public class Job
    {
        private static int iD = 1;
        public int ID { get; set; }
        public JobTypes Type { get; set; }
        public int Length { get; set; }
        public int? ContractorID { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Cost { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public JobStatus Status { get; set; }




        public Job(JobTypes type, int length, int cost, string description)
        {
            ID = iD;
            iD++;

            Type = type;
            Length = length;
            Cost = cost;
            Description = description;

            IsAssigned = false;
            IsDeleted = false;
            Status = JobStatus.Current;

        }


        public void Assign(int contractorID, DateTime startDate)
        {
            StartDate = startDate;
            ContractorID = contractorID;
            IsAssigned = true;
        }


        public void Compelete(DateTime endDate)
        {
            EndDate = endDate;
            Status = JobStatus.Completed;

        }

        public void Cancel(DateTime cancelDate)
        {
            EndDate = cancelDate;
            Status = JobStatus.Cancelled;
        }

        
        public void Delete()
        {
            if (Status == JobStatus.Current && IsAssigned == true)
            {
                return;
            }

            IsDeleted = true;
        }

        public override string ToString()
        {
            string tempString;

            tempString = $"[Job ID {ID:000}; {Type} Repair]  ";
                        
            tempString = tempString + "Period: ";
            if (!DateTime.TryParse(StartDate.ToString(), out DateTime startDate))
            {
                if (Status == JobStatus.Current)
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
                if (!DateTime.TryParse(EndDate.ToString(), out DateTime endDate))
                {
                    tempString = tempString + $"{startDate.Day:00}/{startDate.Month:00}/{startDate.Year} ~ Not Yet Completed\n";
                }
                else
                {
                    if (Status == JobStatus.Cancelled)
                    {
                        tempString = tempString + $"{startDate.Day:00}/{startDate.Month:00}/{startDate.Year} ~ {endDate.Day:00}/{endDate.Month:00}/{endDate.Year} (Cancelled)\n";
                    }
                    else
                    {
                        tempString = tempString + $"{startDate.Day:00}/{startDate.Month:00}/{startDate.Year} ~ {endDate.Day:00}/{endDate.Month:00}/{endDate.Year} (Completed)\n";
                    }
                }
            }

            tempString = tempString + $"Repair Cost & Length: ${Cost} / {Length} Days (Daily Cost ${Cost / Length})\n";


            if (!IsAssigned)
            {
                tempString = tempString + "Contractor ID: Not Yet Assigned\n";
            }
            else
            {
                if (ContractorID < 10)
                {
                    tempString = tempString + $"Contractor ID: 00{ContractorID}\n";
                }
                else if (ContractorID < 100)
                {
                    tempString = tempString + $"Contractor ID: 0{ContractorID}\n";
                }
                else
                {
                    tempString = tempString + $"Contractor ID: {ContractorID}\n";
                }
            }


            if (Status == JobStatus.Current)
            {
                if (!IsAssigned)
                {
                    tempString = tempString + $"Status: Not Yet Started\n";
                }
                else
                {
                    tempString = tempString + $"Status: Currently In Process (Contractor ID {ContractorID})\n";
                }
            }
            else
            {
                if (!DateTime.TryParse(EndDate.ToString(), out DateTime endDate))
                {
                    tempString = tempString + $"Status: Cancelled Before Repair Start\n";
                }
                else
                {
                    if (Status == JobStatus.Cancelled)
                    {
                        tempString = tempString + $"Status: Cancelled On {endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
                    }
                    else
                    {
                        tempString = tempString + $"Status: Completed On {endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
                    }
                }
            }

            return tempString;
        }


    }




    public enum JobTypes
    {
        Drill,
        Excavator,
        Truck,
        Crusher,
        Convenyor,
        Reclaimer,
        Train,
    }


    public enum JobStatus
    {
        Current,
        Completed,
        Cancelled,
    }


}
