using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicense_Applications
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID {  get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID {  get; set; }
        public clsApplication application { get; set; }



        public clsLocalDrivingLicense_Applications()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicense_Applications(int LocalDrivingLicenseApplicationID, 
            int ApplicationID,int LicenseClassID)
        {

            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;

            Mode = enMode.Update;
        }

      public  struct stLocalDrivingLicenseApp
        {
            public int LocalDrivingLicenseApplicationID;
            public string LicenseName;
            public string NationalNo;
            public string ApplicationStatus;
            public byte PassedTestCount;
        }

        public static DataTable GetAllLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicense_ApplicationsData.GetAllLocalDrivingLicense_Applications();
        }


        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID =
                clsLocalDrivingLicense_ApplicationsData.AddNewLocalDrivingLicenseApplicationI
                (this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {

            return clsLocalDrivingLicense_ApplicationsData.UpdateLocalDrivingLicenseApplication
                (this.LocalDrivingLicenseApplicationID,this.ApplicationID,this.LicenseClassID);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }
            return false;
        }

        public static bool IsLocalDrivinLicensAppExistbyApplicantPersonIDAndStatus(int ApplicantPersonID,
            byte ApplicationStatus,int LicenseClassID)
        {
            return clsLocalDrivingLicense_ApplicationsData.IsLocalDrivinLicensAppExistbyApplicantPersonIDAndStatus
                (ApplicantPersonID, ApplicationStatus, LicenseClassID);
        }


        public static clsLocalDrivingLicense_Applications FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID=-1;
            int  LicenseClassID=-1;

            if (clsLocalDrivingLicense_ApplicationsData.GetLocalDrivinLicensAppExistbyApplicantInfoByApplicationID
                ( ApplicationID,ref LocalDrivingLicenseApplicationID, ref LicenseClassID))

                return new clsLocalDrivingLicense_Applications(LocalDrivingLicenseApplicationID, ApplicationID
                    , LicenseClassID);
            else
                return null;
        }

        public static clsLocalDrivingLicense_Applications FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1;
            int LicenseClassID = -1;

            if (clsLocalDrivingLicense_ApplicationsData.GetLocalDrivinLicensAppExistbyApplicantInfoByL_D_L_AppID
                ( LocalDrivingLicenseApplicationID,ref ApplicationID,  ref LicenseClassID))

                return new clsLocalDrivingLicense_Applications(LocalDrivingLicenseApplicationID, ApplicationID
                    , LicenseClassID);
            else
                return null;
        }


        public static byte PassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicense_ApplicationsData.GetPassedTestCount(LocalDrivingLicenseApplicationID);

        }

    }
}
