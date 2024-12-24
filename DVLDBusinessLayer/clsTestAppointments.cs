using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int TestAppointmentID {  get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public byte PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID {  get; set; }


        public clsTestAppointments()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this .LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate=DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = true;
            this.RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsTestAppointments(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,byte PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {

            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate =AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID= RetakeTestApplicationID;

            Mode = enMode.Update;
        }

        public static DataTable GetAllTestAppointments(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            return clsTestAppointmentsData.GetAllTestAppointments(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool isTestAppointmentsExist(int LocalDrivingLicenseApplicationID,int TestTypeID,bool IsLocked)
        {
            return clsTestAppointmentsData.IsTestAppointmentsExist(LocalDrivingLicenseApplicationID, TestTypeID, IsLocked);
        }

        private bool _AddNewTestAppointment()
        {

            this.TestAppointmentID =clsTestAppointmentsData.AddNewTestAppointment
                (this.TestTypeID, this.LocalDrivingLicenseApplicationID,this.AppointmentDate,
               this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {

            return clsTestAppointmentsData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
               this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);

        }

        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }

        public static bool isTestAppointmentHasTest(int TestAppointmentID)
        {
            return clsTestAppointmentsData.IsTestAppointmentshasTest(TestAppointmentID);
        }


        public static clsTestAppointments Find(int TestAppointmentID)
        {

            DateTime  ApplicationDate = DateTime.Now;
            bool IsLocked = false;
            int TestTypeID = -1, LocalDrivingLicenseApplicationID = -1, CreatedByUserID = -1, RetakeTestApplicationID=-1;
            byte PaidFees = 0;

            if (clsTestAppointmentsData.GetAppointmentshasTestInfoByID
                (TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID
            , ref ApplicationDate, ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestApplicationID))

                return new clsTestAppointments(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID
            , ApplicationDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        public static bool IsTestAppointmentFail (int TestAppointmentID)
        {
            return clsTestAppointmentsData.IsTestAppointmentFail(TestAppointmentID);
        }


    }
}
