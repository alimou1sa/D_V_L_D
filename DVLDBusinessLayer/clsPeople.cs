using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsPeople
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string Name()
        {
            return this.FirstName+ " "+this.SecondName+" "+this.ThirdName+" "+ this.LastName;
        }
        public string Email { set; get; }
        public string Phone { set; get; }
        public byte Gendor { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ImagePath { set; get; }
        public int NationalityCountryID { set; get; }
        
        public  clsPeople()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.ImagePath = "";
            this.Address = "";
            this.DateOfBirth = new DateTime(1, 1, 1);
            this.Email = "";
            this.NationalityCountryID = -1;
            this.NationalNo = "";
            this.Gendor = 0 ;
            this.Phone = "";
            this.LastName = "";

            Mode = enMode.AddNew;
        }


        private  clsPeople(int PersonID, string FirstName, string LastName, string SecondName, string ThirdName,
            string Email, string Phone, string Address, DateTime DateOfBirth,
            int NationalityCountryID, string ImagePath, byte Gender,string NationalNo)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.ImagePath = ImagePath;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.NationalNo = NationalNo;
            this.Gendor = Gender;
            this.Phone = Phone;
            this.LastName = LastName;

            Mode = enMode.Update;

        }


        public enum enGendor { eMale=0,eFemale=1};

        private bool _AddNewPerson()
        {

            this.PersonID = clsPeopleData.AddNewPerson(this.FirstName, this.LastName,this.SecondName,
               this.ThirdName, this.Email, this.Phone,this.Address, this.DateOfBirth,
               this.NationalityCountryID, this.ImagePath,this.Gendor,this.NationalNo);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {

            return clsPeopleData.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.SecondName,
               this.ThirdName, this.Email, this.Phone, this.Address, this.DateOfBirth,
               this.NationalityCountryID, this.ImagePath, this.Gendor, this.NationalNo);

        }

        public static clsPeople Find(int PersonID)
        {

            string FirstName = "", LastName = "", SecondName="", ThirdName="", Email = "", Phone = "", 
                Address = "", ImagePath = "", NationalNo="";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalityCountryID = -1;

            if (clsPeopleData.GetPersonInfoByID(PersonID,ref FirstName, ref LastName
                  , ref SecondName, ref ThirdName, ref Email, ref Phone, ref Address, 
                  ref DateOfBirth, ref NationalityCountryID, ref ImagePath,ref Gendor,ref NationalNo))

                return new clsPeople(PersonID, FirstName, LastName, SecondName, ThirdName,
                           Email, Phone, Address, DateOfBirth, NationalityCountryID, ImagePath,Gendor,NationalNo);
            else
                return null;
        }

        public static clsPeople FindByNationalNo(string NationalNo)
        {

            string FirstName = "", LastName = "", SecondName = "", ThirdName = "", Email = "", Phone = "",
                Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalityCountryID = -1, PersonID=-1;

            if (clsPeopleData.GetPersonInfoByNationalNo( NationalNo,ref PersonID, ref FirstName, ref LastName
                  , ref SecondName, ref ThirdName, ref Email, ref Phone, ref Address,
                  ref DateOfBirth, ref NationalityCountryID, ref ImagePath, ref Gendor))

                return new clsPeople(PersonID, FirstName, LastName, SecondName, ThirdName,
                           Email, Phone, Address, DateOfBirth, NationalityCountryID, ImagePath, Gendor, NationalNo);
            else
                return null;
        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleData.GetAllPeople();

        }

        public static bool DeletPerson(int PersonID)
        {
            return clsPeopleData.DeletePeople(PersonID);
        }

        public static bool isPesonExistbyPersonID(int PersonID)
        {
            return clsPeopleData.IsPeopleExistbyPersonID(PersonID);
        }
        public static bool isPesonExistbyNationalNo(string NationalNo)
        {
            return clsPeopleData.IsPeopleExistbyNationalNo(NationalNo);
        }


        public static DataTable GetAllPeopleByFrstName( char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByFirstName(StartWith);

        }

        public static DataTable GetAllPeopleByLastName(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByLastName(StartWith);

        }

        public static DataTable GetAllPeopleBySecondName(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleBySecondName(StartWith);

        }

        public static DataTable GetAllPeopleByThirdName(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByThirdName(StartWith);

        }

        public static DataTable GetAllPeopleByNationalNo(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByNationalNo(StartWith);

        }

        public static DataTable GetAllPeopleByPersonID(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByPersonID(StartWith);
        }

        public static DataTable GetAllPeopleByEmail(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByEmail(StartWith);

        }

        public static DataTable GetAllPeopleByPhone(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByPhone(StartWith);

        }

        public static DataTable GetAllPeopleByNationalty(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByNationalty(StartWith);

        }

        public static DataTable GetAllPeopleByAddress(char[] StartWith)
        {
            return clsPeopleData.GetAllPeopleByAddress(StartWith);

        }

        public static DataTable GetAllPeopleByGendor(byte Gendor)
        {
            return clsPeopleData.GetAllPeopleByGendor(Gendor);

        }

        public static clsPeople FirstPersonInfo()
        {
            int PersonID=-1;
            string FirstName = "", LastName = "", SecondName = "", ThirdName = "", Email = "", Phone = "",
                Address = "", ImagePath = "", NationalNo = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            int NationalityCountryID = -1;

            if (clsPeopleData.GetFirstPersonInfo(ref PersonID, ref FirstName, ref LastName
                  , ref SecondName, ref ThirdName, ref Email, ref Phone, ref Address,
                  ref DateOfBirth, ref NationalityCountryID, ref ImagePath, ref Gendor, ref NationalNo))

                return new clsPeople(PersonID, FirstName, LastName, SecondName, ThirdName,
                           Email, Phone, Address, DateOfBirth, NationalityCountryID, ImagePath, Gendor, NationalNo);
            else
                return null;

        }



    }
}
