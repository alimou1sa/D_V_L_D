using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public double ClassFees { get; set; }

        public clsLicenseClass()
        {
            this.LicenseClassID = -1;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.DefaultValidityLength = 18;
            this.MinimumAllowedAge = 18;
            this.ClassFees = 0;

            Mode = enMode.AddNew;

        }

        private clsLicenseClass(int LicenseClassID,string ClassName,string ClassDescription,
            byte DefaultValidityLength, byte MinimumAllowedAge ,double ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.DefaultValidityLength = DefaultValidityLength;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.ClassFees = ClassFees;

            Mode = enMode.Update;

        }


        public static DataTable GetAllLicenseClass()
        {
            return clsLicenseClassData.GetAllLicenseClasses();

        }


        public static clsLicenseClass FindByClassName(string ClassName)
        {

            int LicenseClassID = -1;
            string ClassDescription="";
            byte DefaultValidityLength=18, MinimumAllowedAge= 18;
            double ClassFees = 0;


            if (clsLicenseClassData.GetLicenseClassInfoByName(ClassName, ref LicenseClassID, ref ClassDescription,
             ref DefaultValidityLength, ref MinimumAllowedAge,ref ClassFees))

                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription
                    ,DefaultValidityLength,MinimumAllowedAge,ClassFees);
            else
                return null;

        }



        private bool _AddNewLicenseClass()
        {
            //call DataAccess Layer 

            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(this.ClassName,this.ClassDescription
                ,this.MinimumAllowedAge,this.DefaultValidityLength,this.ClassFees);

            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            //call DataAccess Layer 

            return clsLicenseClassData.UpdateLicenseClass(this.LicenseClassID,this.ClassName, this.ClassDescription
                , this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }
            return false;
        }



        public static clsLicenseClass FindByID( int LicenseClassID)
        {
            string ClassName="";

           
            string ClassDescription = "";
            byte DefaultValidityLength = 18, MinimumAllowedAge = 18;
            double ClassFees = 0;


            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID,ref ClassName, ref ClassDescription,
             ref DefaultValidityLength, ref MinimumAllowedAge, ref ClassFees))

                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription
                    , DefaultValidityLength, MinimumAllowedAge, ClassFees);
            else
                return null;

        }


    }
}
