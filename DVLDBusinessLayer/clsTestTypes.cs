using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestTypes
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public byte TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public double TestTypeFees { get; set; }

        public clsTestTypes()
        {
            this.TestTypeID = 0;
            this.TestTypeTitle = string.Empty;
            this.TestTypeDescription = string.Empty;
            this.TestTypeFees = 0;


            Mode = enMode.AddNew;
        }

        private clsTestTypes(byte TestTypeID, string TestTypeTitle, string TestTypeDescription, double TestTypeFees)

        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;

            Mode = enMode.Update;
        }

        public enum enTestTypes {visionTest=1,WrittenTest=2,PracticalTest=3};


        public static clsTestTypes Find(byte TestTypeID)
        {

            string TestTypeTitle = "", TestTypeDescription="";
            double TestTypeFees = 0;

            if (clsTestTypesData.GetTestTypesInfoByID(TestTypeID,
                ref TestTypeTitle, ref TestTypeDescription,ref TestTypeFees))

                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            else
                return null;

        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();

        }

        private bool _UpdateTestTypes()
        {

            return clsTestTypesData.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle,this.TestTypeDescription,
                this.TestTypeFees);

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    return false;

                case enMode.Update:

                    return _UpdateTestTypes();

            }

            return false;
        }


    }
}
