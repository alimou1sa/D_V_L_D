using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public enum enApplicationType { eAddNewLocalDrivingLicenseService=1, eRenewDrivingLicenseService=2
     , eReplacementforaLostDrivingLicense=3, eReplacementforaDamagedDrivingLicense=4, ReleaseDetainedDrivingLicsense=5,
            eNewInternationalLicense=6,eRetakeTest=7 };

        public int ApplicationTypeID { get; set; }

        public string ApplicationTypeTitle { get; set; }

        public double ApplicationFees { get; set; }

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = string.Empty;
            this.ApplicationFees = 0;


            Mode = enMode.AddNew;
        }

        private clsApplicationTypes(int ApplicationTypeID,string ApplicationTypeTitle,double ApplicationFees)

        {
            this.ApplicationTypeID=ApplicationTypeID;
            this.ApplicationTypeTitle=ApplicationTypeTitle;
            this.ApplicationFees=ApplicationFees;

            Mode = enMode.Update;
        }


        public static clsApplicationTypes Find(int ApplicationTypeID)
        {

            string ApplicationTypeTitle = "";
            double ApplicationFees = 0;

            if (clsApplicationTypesData.GetApplicationtypeInfoByID(ApplicationTypeID,
                ref ApplicationTypeTitle,ref ApplicationFees))

                return new clsApplicationTypes(ApplicationTypeID,ApplicationTypeTitle,ApplicationFees);
            else
                return null;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();

        }

        private bool _UpdateApplicationTypes()
        {

            return clsApplicationTypesData.UpdateApplicationTypes(this.ApplicationTypeID, this.ApplicationTypeTitle,
                this.ApplicationFees);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    return false;
                   
                case enMode.Update:

                    return _UpdateApplicationTypes();

            }

            return false;
        }




    }
}
