using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTests
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string  Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTests()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult=false;
            this.Notes="";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsTests(int TestID, int TestAppointmentID,
            int CreatedByUserID, string Notes, bool TestResult)
        {
            this.TestID = TestID;
            this.TestAppointmentID =TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {

            this.TestID = clsTestsData.AddNewTest
                (this.TestAppointmentID, this.CreatedByUserID,
               this.Notes, this.TestResult);

            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {

            return clsTestsData.UpdateTest(this.TestID,this.TestAppointmentID, this.CreatedByUserID,
                this.Notes,this.TestResult);
        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }


        public static bool IsTestFail(int LocalDrivingLicenseApplicationID, byte TestTypeID, bool IsLocked, bool TestResult)
        {
            return clsTestsData.IsTestFail(LocalDrivingLicenseApplicationID,TestTypeID, IsLocked, TestResult);  
        }
    }
}
