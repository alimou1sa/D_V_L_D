using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationStatus{New=1,Canceled=2,Completed=3}

        public int ApplicationID {  get; set; }
        public int ApplicantPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }

        public byte ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }
        public double PaidFees { get; set; }

        public int CreatedByUserID { get; set; }


        public clsApplication()
        {
            this .ApplicationID = -1;
            this .ApplicationDate = DateTime.Now;
            this .ApplicationTypeID = -1;
            this.ApplicationStatus = 0;
            this .LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this .CreatedByUserID = -1;
            this.ApplicantPersonID = -1;

            Mode = enMode.AddNew;
        }

        private clsApplication(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate
            ,int ApplicationTypeID, byte ApplicationStatus,DateTime LastStatusDate,double PaidFees,
            int CreatedByUserID)
        {

            this.ApplicationID = ApplicationID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicantPersonID = ApplicantPersonID;

            Mode = enMode.Update;
        }


        private bool _AddNewApplication()
        {

            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, 
                this.ApplicationTypeID,
               this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {

            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID,
                this.ApplicationDate,this.ApplicationTypeID,
               this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }

        public static clsApplication Find(int ApplicationID)
        {
        
            DateTime LastStatusDate = DateTime.Now, ApplicationDate=DateTime.Now;
            byte ApplicationStatus = 0;
            int ApplicantPersonID = -1, ApplicationTypeID=-1, CreatedByUserID=-1;
            double PaidFees = 0;

            if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate
            , ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees,
            ref CreatedByUserID))

                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate
            , ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees,
             CreatedByUserID);
            else
                return null;

        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();

        }



        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public static bool isApplicationExistbyID(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExistbyID(ApplicationID);
        }



    }
}
