using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Shell;

namespace RecuritmentSystem20122791
{
    public class Management
    {

        public static Management Instance { get; private set; } = new Management();

        //For the improvement of transparancy and verification ability
        private List<Job> originalJobCreatingList = new List<Job>();

        private string password = "0000";

        // Under conditions, jobs will can be included in multiple Lists.
        public List<Job> AllJobs { get; set; }
        public List<Job> NotDeletedJobs { get; set; }
        public List<Job> CancelledJobs { get; set; }
        public List<Job> CompletedJobs { get; set; }
        public List<Job> CurrentJobs { get; set; }
        public List<Job> CurrentlyAssignedJobs { get; set; }
        public List<Job> CurrentlyNotAssignedJobs { get; set; }

        public List<Job> SearchedJobs { get; set; }

        //For the improvement of transparancy and verification ability
        private List<Contractor> originalContractorCreatingList { get; set; }

        public List<Contractor> AllContractors { get; set; }
        public List<Contractor> NotDeletedContractors;
        public List<Contractor> NotTerminatedContractors;
        public List<Contractor> TerminatedContractors;
        public List<Contractor> WorkingContractors;
        public List<Contractor> WaitingContractors;

        public List<Contractor> AvailableContractors;
        public List<Contractor> DisplayedContractors;

        public Job TargetJob;
        public Contractor TargetContractor;

        public bool IsDone;

        public Management()
        {
            originalJobCreatingList = new List<Job>();
            AllJobs = new List<Job>();
            NotDeletedJobs = new List<Job>();
            CancelledJobs = new List<Job>();
            CompletedJobs = new List<Job>();
            CurrentJobs = new List<Job>();
            CurrentlyNotAssignedJobs = new List<Job>();
            CurrentlyAssignedJobs = new List<Job>();
            SearchedJobs = new List<Job>();

            originalContractorCreatingList = new List<Contractor>();
            AllContractors = new List<Contractor>();
            NotDeletedContractors = new List<Contractor>();
            NotTerminatedContractors = new List<Contractor>();
            TerminatedContractors = new List<Contractor>();
            WorkingContractors = new List<Contractor>();
            WaitingContractors = new List<Contractor>();
            AvailableContractors = new List<Contractor>(); ;
            DisplayedContractors = new List<Contractor>(); ;
        }


        public void ResetJobList()
        {
            NotDeletedJobs = [];
            CancelledJobs = [];
            CompletedJobs = [];
            CurrentJobs = [];
            CurrentlyNotAssignedJobs = [];
            CurrentlyAssignedJobs = [];

            foreach (Job job in AllJobs)
            {

                if (!job.IsDeleted)
                {
                    NotDeletedJobs.Add(job);

                    if (job.Status == JobStatus.Cancelled)
                    {
                        CancelledJobs.Add(job);
                    }
                    else if (job.Status == JobStatus.Completed)
                    {
                        CompletedJobs.Add(job);
                    }
                    else
                    {
                        CurrentJobs.Add(job);

                        if (!job.IsAssigned)
                        {
                            CurrentlyNotAssignedJobs.Add(job);
                        }
                        else
                        {
                            CurrentlyAssignedJobs.Add(job);
                        }
                    }
                }
            }
        }


        public void ResetContractorList()
        {
            NotDeletedContractors = [];
            NotTerminatedContractors = [];
            TerminatedContractors = [];
            WorkingContractors = [];
            WaitingContractors = [];

            foreach (Contractor contractor in AllContractors)
            {
                if (!contractor.IsDeleted)
                {
                    NotDeletedContractors.Add(contractor);

                    if (contractor.Status == ContractorStatus.ContractEnd)
                    {
                        TerminatedContractors.Add(contractor);
                    }
                    else
                    {
                        NotTerminatedContractors.Add(contractor);

                        if (contractor.Status == ContractorStatus.Working)
                        {
                            WorkingContractors.Add(contractor);
                        }
                        else
                        {
                            WaitingContractors.Add(contractor);
                        }
                    }
                }
            }
        }

        public bool CheckPassword(string inputPassword)
        {
            if (inputPassword == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddJob(string jobType, string jobLength, string jobCost, string jobDescription)
        {
            JobTypes inputJobType;
            int inputJobLength;
            int inputJobCost;
            string inputJobDescription;

            IsDone = false;

            if (string.IsNullOrEmpty(jobType))
            {
                MessageBox.Show("NOTE: Please select the job type.");
                return;
            }

            if (string.IsNullOrEmpty(jobLength) || !int.TryParse(jobLength, out inputJobLength))
            {
                MessageBox.Show("NOTE: Please enter the length of job (equal or more than '1' day; 1, 2, 3...)");
                return;
            }
            else if (inputJobLength <= 0)
            {
                MessageBox.Show("NOTE: Please enter the length of job (equal or more than '1' day; 1, 2, 3...)");
                return;
            }

            if (string.IsNullOrEmpty(jobCost) || !int.TryParse(jobCost, out inputJobCost))
            {
                MessageBox.Show("NOTE: Please enter the cost of job (equal or more than '1' dollar; 1, 2, 3...)");
                return;
            }
            else if (inputJobCost <= 0)
            {
                MessageBox.Show("NOTE: Please enter the cost of job (equal or more than '1' dollar; 1, 2, 3...)");
                return;
            }
            else if (inputJobCost < inputJobLength)
            {
                MessageBox.Show("NOTE: Job cost(dallors) must be equal or more than job length(days)");
                return;
            }

            //Avoid an error shown in new Job
            inputJobType = JobTypes.Drill;

            if (jobType == "Drill")
            {
                inputJobType = JobTypes.Drill;
            }
            else if (jobType == "Excavator")
            {
                inputJobType = JobTypes.Excavator;
            }
            else if (jobType == "Truck")
            {
                inputJobType = JobTypes.Truck;
            }
            else if (jobType == "Crusher")
            {
                inputJobType = JobTypes.Crusher;
            }
            else if (jobType == "Reclaimer")
            {
                inputJobType = JobTypes.Reclaimer;
            }
            else if (jobType == "Train")
            {
                inputJobType = JobTypes.Train;
            }

            if (String.IsNullOrEmpty(jobDescription))
            {
                inputJobDescription = "N/A";
            }
            else
            {
                inputJobDescription = jobDescription;
            }

            Job job = new Job(inputJobType, inputJobLength, inputJobCost, inputJobDescription);
            originalJobCreatingList.Add(job);
            AllJobs.Add(job);
            ResetJobList();

            IsDone = true;
            MessageBox.Show($"[Job ID {job.ID}; {job.Type} Repair] Added");
        }

        public void AddContractor(string contractorName, List<string> contractorSkills, string contractorStartDate, string contractorMinRate, string contractorMaxRate, string contractorDescription)
        {
            string inputContractorName;
            List<JobTypes> inputContractorSkills = new List<JobTypes>();
            DateTime inputContractorStartDate;
            int inputContractorMinRate;
            int inputContractorMaxRate;
            string inputContractorDescription;

            IsDone = false;

            if (string.IsNullOrEmpty(contractorName))
            {
                MessageBox.Show("NOTE: Please enter contractor name");
                return;
            }
            else
            {
                inputContractorName = contractorName;
            }

            if (contractorSkills.Count == 0)
            {
                MessageBox.Show("NOTE: Please add at least 1 skill");
                return;
            }
            else
            {
                foreach (string contractorSkill in contractorSkills)
                {
                    if (contractorSkill == "Drill")
                    {
                        inputContractorSkills.Add(JobTypes.Drill);
                    }
                    else if (contractorSkill == "Excavator")
                    {
                        inputContractorSkills.Add(JobTypes.Excavator);
                    }
                    else if (contractorSkill == "Truck")
                    {
                        inputContractorSkills.Add(JobTypes.Truck);
                    }
                    else if (contractorSkill == "Crusher")
                    {
                        inputContractorSkills.Add(JobTypes.Crusher);
                    }
                    else if (contractorSkill == "Reclaimer")
                    {
                        inputContractorSkills.Add(JobTypes.Reclaimer);
                    }
                    else if (contractorSkill == "Train")
                    {
                        inputContractorSkills.Add(JobTypes.Train);
                    }
                }
            }

            if (contractorStartDate.Length != 10 || contractorStartDate[2] != '/' || contractorStartDate[5] != '/')
            {
                MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                return;
            }

            string strDay = $"{contractorStartDate[0]}{contractorStartDate[1]}";
            string strMonth = $"{contractorStartDate[3]}{contractorStartDate[4]}";
            string strYear = $"{contractorStartDate[6]}{contractorStartDate[7]}{contractorStartDate[8]}{contractorStartDate[9]}";
            string dateString = $"{strMonth}/{strDay}/{strYear}";

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateString, culture, styles, out inputContractorStartDate))
            {
                MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                return;
            }
            else if (inputContractorStartDate.Year < 1950 || inputContractorStartDate.Year > 2050)
            {
                MessageBox.Show("NOTE: Please enter the start date between Year 1950 and 2050");
                return;
            }



            if (!int.TryParse(contractorMinRate, out inputContractorMinRate) || inputContractorMinRate < 1)
            {
                MessageBox.Show("NOTE: Please enter daily minimum rate equal or more than 1 dollar");
                return;
            }

            if (!int.TryParse(contractorMaxRate, out inputContractorMaxRate) || inputContractorMaxRate < 1)
            {
                MessageBox.Show("NOTE: Please enter daily maximum rate equal or more than 1 dollar");
                return;
            }
            else if (inputContractorMinRate > inputContractorMaxRate)
            {
                MessageBox.Show("NOTE: Maximum rate must be equal or more than minimum rate");
                return;
            }


            if (string.IsNullOrEmpty(contractorDescription))
            {
                inputContractorDescription = "N/A";
            }
            else
            {
                inputContractorDescription = contractorDescription;
            }

            Contractor contractor = new Contractor(inputContractorName, inputContractorSkills, inputContractorStartDate, inputContractorMinRate, inputContractorMaxRate, inputContractorDescription);
            originalContractorCreatingList.Add(contractor);
            AllContractors.Add(contractor);

            ResetContractorList();
            IsDone = true;

            MessageBox.Show($"[Contractor ID {contractor.ID}; Name {contractor.Name}] Added");

        }


        public bool CheckValidAssignJob(Job targetJob)
        {
            foreach (Job job in CurrentlyAssignedJobs)
            {
                if (targetJob == job)
                {
                    MessageBox.Show("NOTE: Not Available (Already Assigned)");
                    return false;
                }
            }

            foreach (Job job in CompletedJobs)
            {
                if (targetJob == job)
                {
                    MessageBox.Show("NOTE: Not Available (Completed Job)");
                    return false;
                }
            }

            foreach (Job job in CancelledJobs)
            {
                if (targetJob == job)
                {
                    MessageBox.Show("NOTE: Not Available (Cancelled Job)");
                    return false;
                }
            }

            if (WaitingContractors == null)
            {
                MessageBox.Show("NOTE: No contractors waiting for job");
                return false;
            }

            if (WaitingContractors.Count == 0)
            {
                MessageBox.Show("NOTE: No contractors waiting for job");
                return false;
            }

            return true;
        }

        public void CheckAvailableContractors(Job targetJob, DateTime startDate)
        {
            AvailableContractors = new List<Contractor>();

            foreach (Contractor contractor in WaitingContractors)
            {
                if (contractor.MinRate <= (targetJob.Cost / targetJob.Length)
                    && contractor.MaxRate >= (targetJob.Cost / targetJob.Length)
                    && contractor.Skills.Contains(targetJob.Type)
                    && contractor.StartDate <= startDate)
                {
                    AvailableContractors.Add(contractor);
                }
            }
        }

        public void AssignJob(Job targetJob, Contractor targetContractor, DateTime targetStartDate)
        {
            IsDone = false;
            targetJob.Assign(targetContractor.ID, targetStartDate);
            targetContractor.GetJob(targetJob.ID, targetJob.Type);

            ResetJobList();
            ResetContractorList();
            MessageBox.Show($"[Job ID {targetJob.ID}; {targetJob.Type} Repair] assigned to [Contractor ID {targetContractor.ID}; Name {targetContractor.Name}]");

        }

        public void AutoChoice()
        {
            int minimumAvgRate = -1;

            foreach (Contractor contractor in AvailableContractors)
            {
                if (minimumAvgRate == -1)
                {
                    minimumAvgRate = (contractor.MinRate + contractor.MaxRate) / 2;
                    TargetContractor = contractor;
                }
                else
                {
                    if ((contractor.MinRate + contractor.MaxRate) / 2 < minimumAvgRate)
                    {
                        minimumAvgRate = (contractor.MinRate + contractor.MaxRate) / 2;
                        TargetContractor = contractor;
                    }
                }
            }

        }

        public bool CheckValidCompleteJob(Job targetJob)
        {
            if (targetJob.Status == JobStatus.Completed)
            {
                MessageBox.Show("NOTE: Already Completed");
                return false;
            }

            if (targetJob.Status == JobStatus.Cancelled)
            {
                MessageBox.Show("NOTE: Cancelled job cannot be completed");
                return false;
            }

            return true;
        }

        public void CompleteJob(Job targetJob, string completeDate)
        {
            IsDone = false;

            if (targetJob.Status == JobStatus.Completed)
            {
                MessageBox.Show("NOTE: Already Completed");
                return;
            }

            if (targetJob.Status == JobStatus.Cancelled)
            {
                MessageBox.Show("NOTE: Cancelled job cannot be completed");
                return;
            }

            if (completeDate.Length != 10)
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }
            else if (completeDate[2] != '/' || completeDate[5] != '/')
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            string strDay = $"{completeDate[0]}{completeDate[1]}";
            string strMonth = $"{completeDate[3]}{completeDate[4]}";
            string strYear = $"{completeDate[6]}{completeDate[7]}{completeDate[8]}{completeDate[9]}";
            string dateString = $"{strMonth}/{strDay}/{strYear}";

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateString, culture, styles, out DateTime endDate))
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            if (endDate.Year < 1950 || endDate.Year > 2050)
            {
                MessageBox.Show("NOTE: Please enter the end date between Year 1950 and 2050");
                return;
            }

            DateTime tempStartDate = DateTime.Parse(targetJob.StartDate.ToString());

            if (tempStartDate > endDate)
            {
                MessageBox.Show("NOTE: End date must not be earlier than start date");
                return;
            }

            if (((endDate - tempStartDate).Days + 1) < targetJob.Length)
            {
                MessageBox.Show("NOTE: Length between start date and end complete date must be longer than required job length");
                return;
            }

            targetJob.Compelete(endDate);

            foreach (Contractor contractor in AllContractors)
            {
                if (contractor.ID == targetJob.ContractorID)
                {
                    contractor.EmptyJob();
                    MessageBox.Show($"[Job ID {targetJob.ID}; {targetJob.Type} repair] is completed by [Contractor ID {contractor.ID}; Name {contractor.Name}]");
                }
            }


            ResetJobList();
            ResetContractorList();

            IsDone = true;

        }

        public bool CheckValidCancelJob(Job targetJob)
        {
            if (targetJob.Status == JobStatus.Completed)
            {
                MessageBox.Show("NOTE: Completed job cannot be cancelled");
                return false;
            }

            if (targetJob.Status == JobStatus.Cancelled)
            {
                MessageBox.Show("NOTE: Already cancelled");
                return false;
            }

            return true;
        }

        public void CacncelJob(Job targetJob, string cancelDate)
        {
            IsDone = false;

            if (targetJob.Status == JobStatus.Completed)
            {
                MessageBox.Show("NOTE: Completed job cannot be cancelled");
                return;
            }

            if (targetJob.Status == JobStatus.Cancelled)
            {
                MessageBox.Show("NOTE: Already cancelled");
                return;
            }


            if (cancelDate.Length != 10)
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }
            else if (cancelDate[2] != '/' || cancelDate[5] != '/')
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            string strDay = $"{cancelDate[0]}{cancelDate[1]}";
            string strMonth = $"{cancelDate[3]}{cancelDate[4]}";
            string strYear = $"{cancelDate[6]}{cancelDate[7]}{cancelDate[8]}{cancelDate[9]}";
            string dateString = $"{strMonth}/{strDay}/{strYear}";

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateString, culture, styles, out DateTime endDate))
            {
                MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                return;
            }

            if (endDate.Year < 1950 || endDate.Year > 2050)
            {
                MessageBox.Show("NOTE: Please enter the end date between Year 1950 and 2050");
                return;
            }

            DateTime tempStartDate;

            if (TargetJob.StartDate != null)
            {
                tempStartDate = DateTime.Parse(TargetJob.StartDate.ToString());

                if (tempStartDate > endDate)
                {
                    MessageBox.Show("NOTE: Cancel date must not be earlier than start date");
                    return;
                }
            }

            targetJob.Cancel(endDate);

            foreach (Contractor contractor in AllContractors)
            {
                if (contractor.ID == targetJob.ContractorID)
                {
                    contractor.EmptyJob();
                    if (TargetJob.StartDate != null)
                    {
                        MessageBox.Show($"[Job ID {targetJob.ID}; {targetJob.Type} repair] worked by[Contractor ID {contractor.ID}; Name {contractor.Name}] is cancelled.");
                    }
                    else
                    {
                        MessageBox.Show($"[Job ID {targetJob.ID}; {targetJob.Type} repair]  is cancelled");
                    }
                }
            }

            ResetJobList();
            ResetContractorList();

            IsDone = true;
        }


        public void EditJob(Job targetJob, string jobType, string jobLength, string jobCost, string jobStartDate, string jobEndDate, string jobStatus, string jobDescription)
        {
            IsDone = false;

            JobTypes inputJobType;
            JobStatus inputJobStatus;
            int inputJobLength;
            int inputJobCost;
            DateTime inputJobStartDate;
            DateTime inputJobEndDate;
            string inputJobDescription;


            //Avoid an error 
            inputJobType = JobTypes.Drill;

            if (jobType == "Drill")
            {
                inputJobType = JobTypes.Drill;
            }
            else if (jobType == "Excavator")
            {
                inputJobType = JobTypes.Excavator;
            }
            else if (jobType == "Truck")
            {
                inputJobType = JobTypes.Truck;
            }
            else if (jobType == "Crusher")
            {
                inputJobType = JobTypes.Crusher;
            }
            else if (jobType == "Reclaimer")
            {
                inputJobType = JobTypes.Reclaimer;
            }
            else if (jobType == "JobTrain")
            {
                inputJobType = JobTypes.Train;
            }

            if (targetJob.Status == JobStatus.Current)
            {
                if (jobStatus == "Completed" || jobStatus == "Cancelled")
                {
                    MessageBox.Show("NOTE: Current status cannot be changed. Please use complete or cancel function.");
                    return;
                }
            }

            if (targetJob.Status == JobStatus.Completed || targetJob.Status == JobStatus.Cancelled)
            {
                if (jobStatus == "Current")
                {
                    MessageBox.Show("NOTE: Only Completed or Cancelled is available for job status");
                    return;
                }
            }

            //Avoid an error
            inputJobStatus = JobStatus.Current;

            if (jobStatus == "Current")
            {
                inputJobStatus = JobStatus.Current;
            }
            else if (jobStatus == "Completed")
            {
                inputJobStatus = JobStatus.Completed;
            }
            else if (jobStatus == "Cancelled")
            {
                inputJobStatus = JobStatus.Cancelled;
            }



            if (string.IsNullOrEmpty(jobLength))
            {
                MessageBox.Show("NOTE: please enter the length of job (equal or more than '1' day; 1, 2, 3...)");
                return;
            }
            else if (!int.TryParse(jobLength, out inputJobLength))
            {
                MessageBox.Show("NOTE: please enter the length of job (equal or more than '1' day; 1, 2, 3...)");
                return;
            }

            if (string.IsNullOrEmpty(jobCost))
            {
                MessageBox.Show("NOTE: please enter the cost of job (equal or more than '1' dollar; 1, 2, 3...)");
                return;
            }
            else if (!int.TryParse(jobCost, out inputJobCost))
            {
                MessageBox.Show("NOTE: please enter the cost of job (equal or more than '1' dollar; 1, 2, 3...)");
                return;
            }
            else if (inputJobCost < inputJobLength)
            {
                MessageBox.Show("NOTE: job cost(dallors) must be equal or more than job length(days)");
                return;
            }



            if (!targetJob.IsAssigned)
            {
                if (!string.IsNullOrEmpty(jobStartDate))
                {
                    MessageBox.Show("NOTE: Start dete must be empty (not assigned)");
                    return;
                }
            }

            if (targetJob.Status == JobStatus.Current)
            {
                if (!string.IsNullOrEmpty(jobEndDate))
                {
                    MessageBox.Show("NOTE: End dete must be empty (not completed or not cancelled)");
                    return;
                }
            }

            inputJobStartDate = DateTime.MinValue;
            inputJobEndDate = DateTime.MinValue;

            if (targetJob.IsAssigned)
            {
                if (string.IsNullOrEmpty(jobStartDate))
                {
                    MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                    return;
                }
                else if (jobStartDate.Length != 10 || jobStartDate[2] != '/' || jobStartDate[5] != '/')
                {
                    MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                    return;
                }


                string strDay = $"{jobStartDate[0]}{jobStartDate[1]}";
                string strMonth = $"{jobStartDate[3]}{jobStartDate[4]}";
                string strYear = $"{jobStartDate[6]}{jobStartDate[7]}{jobStartDate[8]}{jobStartDate[9]}";
                string dateString = $"{strMonth}/{strDay}/{strYear}";

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeStyles styles = DateTimeStyles.None;

                if (!DateTime.TryParse(dateString, culture, styles, out inputJobStartDate))
                {
                    MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                    return;
                }
                else if (inputJobStartDate.Year < 1950 || inputJobStartDate.Year > 2050)
                {
                    MessageBox.Show("NOTE: Please enter the start date between Year 1950 and 2050");
                    return;
                }
            }



            if (inputJobStatus == JobStatus.Completed || inputJobStatus == JobStatus.Cancelled)
            {
                if (string.IsNullOrEmpty(jobEndDate))
                {
                    MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                    return;
                }
                else if (jobEndDate.Length != 10 || jobEndDate[2] != '/' || jobEndDate[5] != '/')
                {
                    MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                    return;
                }

                string strDay = $"{jobEndDate[0]}{jobEndDate[1]}";
                string strMonth = $"{jobEndDate[3]}{jobEndDate[4]}";
                string strYear = $"{jobEndDate[6]}{jobEndDate[7]}{jobEndDate[8]}{jobEndDate[9]}";
                string dateString = $"{strMonth}/{strDay}/{strYear}";

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeStyles styles = DateTimeStyles.None;

                if (!DateTime.TryParse(dateString, culture, styles, out inputJobEndDate))
                {
                    MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                    return;
                }
                else if (inputJobEndDate.Year < 1950 || inputJobEndDate.Year > 2050)
                {
                    MessageBox.Show("NOTE: Please enter the end date between Year 1950 and 2050");
                    return;
                }
            }

            if (inputJobStatus != JobStatus.Current && targetJob.IsAssigned)
            {
                if (inputJobStartDate > inputJobEndDate)
                {
                    MessageBox.Show("NOTE: End date must not be earlier than Start Date");
                    return;
                }
                else if (((inputJobEndDate - inputJobStartDate).Days + 1) < inputJobLength)
                {
                    MessageBox.Show("NOTE: Length between start date and end complete date must be longer than required job length");
                    return;
                }
            }

            inputJobDescription = jobDescription;



            MessageBox.Show($"[Job ID {targetJob.ID}; {targetJob.Type} repair] Editted");

            targetJob.Type = inputJobType;
            targetJob.Status = inputJobStatus;
            targetJob.Cost = inputJobCost;
            targetJob.Length = inputJobLength;
            targetJob.Description = inputJobDescription;

            if (targetJob.IsAssigned)
            {
                targetJob.StartDate = inputJobStartDate;
            }

            if (targetJob.Status != JobStatus.Current)
            {
                targetJob.EndDate = inputJobEndDate;
            }

            ResetJobList();

            IsDone = true;
        }

        public void DeleteJob(Job targetJob)
        {
            IsDone = false;

            if (targetJob.Status == JobStatus.Current && targetJob.IsAssigned == true)
            {
                MessageBox.Show("NOTE: Currently working job must not be deleted");
                return;
            }

            targetJob.Delete();

            ResetJobList();
            MessageBox.Show($"[Job ID {targetJob.ID}; {targetJob.Type} repair] is deleted");

            IsDone = true;
        }


        public void SearchJob(string jobStatus, string jobType, string jobMinCost, string jobMaxCost, string jobID, string jobContractorID)
        {
            IsDone = false;

            int inputJobMinCost;
            int inputJobMaxCost;
            int inputJobID;
            int inputJobContractorID;


            SearchedJobs = new List<Job>();

            if (NotDeletedJobs == null)
            {
                SearchedJobs = [];
            }
            else if (jobStatus == "All")
            {
                SearchedJobs = NotDeletedJobs;
            }
            else if (jobStatus == "CurrentAll")
            {
                foreach (Job job in NotDeletedJobs)
                {
                    if (job.Status == JobStatus.Current)
                    {
                        SearchedJobs.Add(job);
                    }
                }
            }
            else if (jobStatus == "CurrentAssigned")
            {
                foreach (Job job in NotDeletedJobs)
                {
                    if (job.Status == JobStatus.Current && job.IsAssigned == true)
                    {
                        SearchedJobs.Add(job);
                    }
                }
            }
            else if (jobStatus == "CurrentNotAssigned")
            {
                foreach (Job job in NotDeletedJobs)
                {
                    if (job.Status == JobStatus.Current && job.IsAssigned == false)
                    {
                        SearchedJobs.Add(job);
                    }
                }
            }
            else if (jobStatus == "Completed")
            {
                foreach (Job job in NotDeletedJobs)
                {
                    if (job.Status == JobStatus.Completed)
                    {
                        SearchedJobs.Add(job);
                    }
                }
            }
            else if (jobStatus == "Cancelled")
            {
                foreach (Job job in NotDeletedJobs)
                {
                    if (job.Status == JobStatus.Cancelled)
                    {
                        SearchedJobs.Add(job);
                    }
                }
            }

            List<Job> tempList = [];

            if (jobType == "All")
            {
                tempList = SearchedJobs;
            }
            else if (jobType == "Drill")
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Type == JobTypes.Drill)
                    {
                        tempList.Add(job);
                    }
                }
            }
            else if (jobType == "Excavator")
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Type == JobTypes.Excavator)
                    {
                        tempList.Add(job);
                    }
                }
            }
            else if (jobType == "Truck")
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Type == JobTypes.Truck)
                    {
                        tempList.Add(job);
                    }
                }
            }
            else if (jobType == "Crusher")
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Type == JobTypes.Crusher)
                    {
                        tempList.Add(job);
                    }
                }
            }
            else if (jobType == "Reclaimer")
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Type == JobTypes.Reclaimer)
                    {
                        tempList.Add(job);
                    }
                }
            }
            else if (jobType == "JobTrain")
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Type == JobTypes.Train)
                    {
                        tempList.Add(job);
                    }
                }
            }



            SearchedJobs = tempList;

            tempList = [];
            if (string.IsNullOrEmpty(jobMinCost))
            {
                tempList = SearchedJobs;
            }
            else if (!int.TryParse(jobMinCost, out inputJobMinCost) || inputJobMinCost < 1)
            {
                MessageBox.Show("NOTE: Minimum value in cost range must be equal or more than '1' dollar; 1, 2, 3... ");
                return;
            }
            else
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.Cost >= inputJobMinCost)
                    {
                        tempList.Add(job);
                    }
                }
            }

            SearchedJobs = tempList;

            tempList = [];
            if (string.IsNullOrEmpty(jobMaxCost))
            {
                tempList = SearchedJobs;
            }
            else if (!int.TryParse(jobMaxCost, out inputJobMaxCost) || inputJobMaxCost < 1)
            {
                MessageBox.Show("NOTE: Maximum value in cost range must be equal or more than '1' dollar; 1, 2, 3... ");
                return;
            }
            else
            {
                if (!string.IsNullOrEmpty(jobMinCost) && int.TryParse(jobMinCost, out inputJobMinCost) && inputJobMaxCost < inputJobMinCost)
                {
                    MessageBox.Show("NOTE: Maximum value in cost range must be equal or more than Minimum value");
                    return;
                }

                foreach (Job job in SearchedJobs)
                {
                    if (job.Cost <= inputJobMaxCost)
                    {
                        tempList.Add(job);
                    }
                }
            }

            SearchedJobs = tempList;

            tempList = [];

            if (string.IsNullOrEmpty(jobID))
            {
                tempList = SearchedJobs;
            }
            else if (!int.TryParse(jobID, out inputJobID) || inputJobID < 1)
            {
                MessageBox.Show("NOTE: Job ID must be positive number");
                return;
            }
            else
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.ID == inputJobID)
                    {
                        tempList.Add(job);
                    }
                }
            }

            SearchedJobs = tempList;

            tempList = [];
            if (string.IsNullOrEmpty(jobContractorID))
            {
                tempList = SearchedJobs;
            }
            else if (!int.TryParse(jobContractorID, out inputJobContractorID) || inputJobContractorID < 1)
            {
                MessageBox.Show("NOTE: Contractor ID must be positive number");
                return;
            }
            else
            {
                foreach (Job job in SearchedJobs)
                {
                    if (job.ContractorID == inputJobContractorID)
                    {
                        tempList.Add(job);
                    }
                }
            }

            SearchedJobs = tempList;
            IsDone = true;
        }

        public bool CheckValidTeminateContractor(Contractor targetContractor)
        {
            if (targetContractor.Status == ContractorStatus.ContractEnd)
            {
                MessageBox.Show("Note: Already terminated");
                return false;
            }
            else if (targetContractor.Status == ContractorStatus.Working)
            {
                MessageBox.Show("Note: Currently working contractor cannot be teminated");
                return false;
            }

            return true;
        }

        public void TerminateContractor(Contractor targetContractor, DateTime endDate)
        {
            IsDone = false;

            if (targetContractor.Status == ContractorStatus.ContractEnd)
            {
                MessageBox.Show("Note: Already terminated");
                return;
            }
            else if (targetContractor.Status == ContractorStatus.Working)
            {
                MessageBox.Show("Note: Currently working contractor cannot be teminated");
                return;
            }

            if (endDate < targetContractor.StartDate)
            {
                MessageBox.Show("NOTE: Temination date must not be easier than start date");
                return;
            }
            else
            {
                targetContractor.EndContract(endDate);
                MessageBox.Show($"[Contractor ID {targetContractor.ID}; Name {targetContractor.Name}] Terminated");
            }

            ResetContractorList();
            IsDone = true;
        }

        public bool CheckValidDeleteContractor(Contractor targetContractor)
        {
            if (targetContractor.Status != ContractorStatus.ContractEnd)
            {
                MessageBox.Show("Note: Teminating contractor is required first to delete contractor");
                return false;
            }

            return true;
        }

        public void DeleteContractor(Contractor targetContractor)
        {
            IsDone = false;

            if (targetContractor.Status != ContractorStatus.ContractEnd)
            {
                MessageBox.Show("Note: Teminating contractor is required first to delete contractor");
                return;
            }

            targetContractor.DeleteContractor();

            ResetContractorList();
            IsDone = true;

        }

        public void EditContractor(Contractor targetContractor, string name, List<JobTypes> skillTypes, string minRate, string maxRate, string startDate, string terminateDate)
        {
            string inputName;
            List<JobTypes> inputSkillTypes;
            int inputMinRate;
            int inputMaxRate;
            DateTime inputStartDate;
            DateTime inputTerminationDate;

            IsDone = false;

            if (name == string.Empty)
            {
                MessageBox.Show("NOTE: Please enter contractor name");
                return;
            }
            else
            {
                inputName = name;
            }

            if (skillTypes.Count == 0)
            {
                MessageBox.Show("NOTE: Please add at least 1 skill");
                return;
            }
            else
            {
                inputSkillTypes = skillTypes;
            }

            if (!int.TryParse(minRate, out inputMinRate))
            {
                MessageBox.Show("NOTE: Please enter daily minimum rate equal or more than 1 dollar");
                return;
            }

            if (!int.TryParse(maxRate, out inputMaxRate))
            {
                MessageBox.Show("NOTE: Please enter daily maximum rate equal or more than 1 dollar");
                return;
            }

            if (inputMinRate < 1 || inputMaxRate < 1)
            {
                MessageBox.Show("NOTE: Please enter daily minimum and maximum rates equal or more than 1 dollar");
                return;
            }
            else if (inputMaxRate < inputMinRate)
            {
                MessageBox.Show("NOTE: Maximum rate must be equal or more than minimum rate");
                return;
            }

            if (startDate.Length != 10)
            {
                MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                return;
            }
            else if (startDate[2] != '/' || startDate[5] != '/')
            {
                MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                return;
            }

            string strDay = $"{startDate[0]}{startDate[1]}";
            string strMonth = $"{startDate[3]}{startDate[4]}";
            string strYear = $"{startDate[6]}{startDate[7]}{startDate[8]}{startDate[9]}";
            string dateString = $"{strMonth}/{strDay}/{strYear}";

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeStyles styles = DateTimeStyles.None;

            if (!DateTime.TryParse(dateString, culture, styles, out inputStartDate))
            {
                MessageBox.Show("NOTE: Please enter the start date (DD/MM/YYYY)");
                return;
            }

            if (inputStartDate.Year < 1950 || inputStartDate.Year > 2050)
            {
                MessageBox.Show("NOTE: Please enter the start date between Year 1950 and 2050");
                return;
            }

            if (targetContractor.Status != ContractorStatus.ContractEnd)
            {
                if (!string.IsNullOrEmpty(terminateDate))
                {
                    MessageBox.Show("NOTE: Termination dete must be empty (not yet terminated)");
                    return;
                }
            }
            else
            {
                if (terminateDate.Length != 10)
                {
                    MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                    return;
                }
                else if (terminateDate[2] != '/' || terminateDate[5] != '/')
                {
                    MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                    return;
                }

                strDay = $"{terminateDate[0]}{terminateDate[1]}";
                strMonth = $"{terminateDate[3]}{terminateDate[4]}";
                strYear = $"{terminateDate[6]}{terminateDate[7]}{terminateDate[8]}{terminateDate[9]}";
                dateString = $"{strMonth}/{strDay}/{strYear}";

                if (!DateTime.TryParse(dateString, culture, styles, out inputTerminationDate))
                {
                    MessageBox.Show("NOTE: Please enter the end date (DD/MM/YYYY)");
                    return;
                }
                else if (inputTerminationDate.Year < 1950 || inputTerminationDate.Year > 2050)
                {
                    MessageBox.Show("NOTE: Please enter the end date between Year 1950 and 2050");
                    return;
                }
                else if (inputStartDate > inputTerminationDate)
                {
                    MessageBox.Show("NOTE: Termination date must not be earsier than start date");
                    return;
                }

                targetContractor.EndDate = inputTerminationDate;
            }

            targetContractor.Name = inputName;
            targetContractor.Skills = inputSkillTypes;
            targetContractor.MinRate = inputMinRate;
            targetContractor.MaxRate = inputMaxRate;
            targetContractor.StartDate = inputStartDate;

            ResetContractorList();

            IsDone = true;

        }


    }


}


