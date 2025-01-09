using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecuritmentSystem20122791
{
    public class Contractor
    {
        private static int iD = 1;
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }


        public DateTime? EndDate { get; set; }
        public int? CurrentJobID { get; set; }
        public JobTypes? CurrentJobType { get; set; }

        // Each contractor needs to have at leats 1 skill and at most 3 skills
        // The skills are used when a job is assigned. 
        public List<JobTypes> Skills { get; set; }

        // Each Contractor must provide their daily rate range. 

        public int MinRate { get; set; }
        public int MaxRate { get; set; }

        public ContractorStatus Status { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }



        public Contractor(string name, List<JobTypes> skills, DateTime startDate, int minRate, int maxRate, string description)
        {


            //Job id starts from 1 and increases by 1 when creating a new job.

            ID = iD;
            iD++;

            Name = name;
            Skills = skills;
            StartDate = startDate;
            MinRate = minRate;
            MaxRate = maxRate;
            Description = description;

            Status = ContractorStatus.Waiting;
            IsDeleted = false;
        }

        public void GetJob(int jobID, JobTypes jobType)
        {
            
            CurrentJobID = jobID;
            CurrentJobType = jobType;
            Status = ContractorStatus.Working;
        }

        public void EmptyJob()
        {
            CurrentJobID = null;
            CurrentJobType = null;
            Status = ContractorStatus.Waiting;
        }


        public void EndContract(DateTime endDate)
        {

            EndDate = endDate;
            Status = ContractorStatus.ContractEnd;
        }

        public void DeleteContractor()
        {
            if (Status != ContractorStatus.ContractEnd)
            {
                return;
            }

            IsDeleted = true;
        }

        
        public override string ToString()
        {
            
            string tempString;

            tempString = $"[Contractor ID {ID:000}; Name {Name}]  ";

            tempString = tempString + $"Period: {StartDate.Day:00}/{StartDate.Month:00}/{StartDate.Year} ~ ";

            if (DateTime.TryParse(EndDate.ToString(), out DateTime endDate))
            {
                tempString = tempString + $"{endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
            }
            else
            {
                tempString = tempString + "Now\n";
            }


            tempString = tempString + "Skill List: ";
            int i = 0;
            foreach (JobTypes skill in Skills)
            {
                ++i;
                if (i < Skills.Count)
                {
                    tempString = tempString + $"{skill}, ";
                }
                else
                {
                    tempString = tempString + $"{skill}\n";
                }
            }


            tempString = tempString + $"Dialy Rate: ${MinRate} ~ ${MaxRate}\n";


            if (Status == ContractorStatus.Working)
            {
                tempString = tempString + $"Status: Currently Working On {CurrentJobType} Repair (Job ID {CurrentJobID})\n";
            }
            else if (Status == ContractorStatus.Waiting)
            {
                tempString = tempString + $"Status: Currently Not Working On Any Repair Job\n";
            }
            else
            {
                endDate = DateTime.Parse(EndDate.ToString());
                tempString = tempString + $"Status: Constract Ended On {endDate.Day:00}/{endDate.Month:00}/{endDate.Year}\n";
            }
            return tempString;

        }


    }


    public enum ContractorStatus
    {
        Waiting,
        Working,
        ContractEnd,
    }


}
