using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ApplicationInfo : UserControl
    {
        
        public ApplicationInfo()
        {
            InitializeComponent();
        }



        public clsLocalDrivingLicense_Applications _LocalDrivingLicense_Applications { set; get; }
        public clsApplication application {  set; get; }
        public clsLicenseClass licenseClass {  set; get; }
        public clsPeople people {  set; get; }

        public clsUser user {  set; get; }
        public clsApplicationTypes applicationTypes {  set; get; }

        private bool FindLocalDrivingLicenseApplications(int ApplicationID)
        {
            _LocalDrivingLicense_Applications=clsLocalDrivingLicense_Applications.FindByApplicationID(ApplicationID);
          
            if(_LocalDrivingLicense_Applications!=null)
            {
                return true;

            }

            return false;
        }

        private bool FindApplication(int ApplicationID)
        {
            application=clsApplication.Find(ApplicationID);

            if (application != null)
            {
                return true;

            }

            return false;
        }

        private bool FindPerson(int PersonID)
        {

            people =clsPeople.Find(PersonID);
            if (people != null)
            {
                return true;

            }

            return false;

        }

        private bool FindUser(int UserID)
        {
          user=clsUser.FindByUserID(UserID);

            if (user != null)
            {
                return true;

            }

            return false;
        }

        private bool FindLicenseClass(int LicenseClassID)
        {
            licenseClass = clsLicenseClass.FindByID(LicenseClassID);

            if (licenseClass != null)
            { 
                return true;
            }
            return false;
        }

        private bool FindapplicationTypes(int ApplicationTypeID)
        {
            applicationTypes = clsApplicationTypes.Find(ApplicationTypeID);

            if (applicationTypes != null)
            {
                return true;
            }
            return false;
        }

        private bool  Find(int ApplicationID)
        {    
            if(!FindApplication(ApplicationID)) 
                return false;

            if(!FindLocalDrivingLicenseApplications(ApplicationID))
            return false;

            if (!FindLicenseClass(_LocalDrivingLicense_Applications.LicenseClassID))
                return false;


            if (!FindPerson(application.ApplicantPersonID))
                return false;

            if(!FindUser(application.CreatedByUserID))
                return false;
            if(!FindapplicationTypes(application.ApplicationTypeID))
                return false;
            

            return true;
        }

        public void LoadDrivingLicensApplicationInfo(int ApplicationID)
        {
            Find(ApplicationID);
            byte PassedTestCount = clsLocalDrivingLicense_Applications.PassedTestCount(_LocalDrivingLicense_Applications.LocalDrivingLicenseApplicationID);
          
            lblTestCount.Text = PassedTestCount.ToString() + "/3";
            lbl_D_L_AppID.Text = _LocalDrivingLicense_Applications.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text = licenseClass.ClassName;
            lblID.Text = ApplicationID.ToString();

            lblApplicant.Text = people.FirstName + " " + people.SecondName + " " + people.ThirdName + " " + people.LastName;

            lblStatus.Text = ((clsApplication.enApplicationStatus)application.ApplicationStatus).ToString();

            lblFees.Text = applicationTypes.ApplicationFees.ToString();
            lblType.Text=applicationTypes.ApplicationTypeTitle;
             lblDate.Text=application.ApplicationDate.ToString("yyyy/mm/dd");
            lblStatusDate.Text=application.LastStatusDate.ToString("yyyy/mm/dd");
            lblCreatedBy.Text = user.UserName;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetsils frmPersonDetsils = new frmPersonDetsils(people.PersonID);
                frmPersonDetsils.ShowDialog();
        }
 
    
    }
}
