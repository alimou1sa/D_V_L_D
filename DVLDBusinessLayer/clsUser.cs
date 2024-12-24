using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public clsUser()
        {
            this.PersonID = -1;
            this.UserID = -1;
            this.UserName = "";
            this.IsActive = true;
            this.Password = "";

            Mode = enMode.AddNew;
        }

        private clsUser(int PersonID, int UserID, string UserName, string Password,bool IsActive)
        {
            this.PersonID = PersonID;

            this.UserID = UserID;
            this.UserName = UserName;
            this.IsActive = IsActive;
            this.Password = Password;


            Mode = enMode.Update;

        }


        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1, PersonID = -1;
            bool IsActive = false;
            if (clsUserData.GetUsersInfoByPasswordandUserName(Password, UserName, ref UserID,
                ref IsActive, ref PersonID))

                return new clsUser(PersonID,UserID, UserName, Password, IsActive);
            else

                return null;
        }


        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.UserName, this.Password, this.IsActive, this.PersonID);

            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID,this.UserName, this.Password, this.IsActive, this.PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:

                    return _UpdateUser();
            }

            return false;
        }

        public static bool IsPersonConnectWithUser(int PersonID)
        {
            return clsUserData.IsPersonConnectWithUser(PersonID);
        }

        public static clsUser FindByUserID(int UserID)
        {
            int  PersonID = -1;
            string UserName="", Password="";
            bool IsActive = false;
            if (clsUserData.GetUsersInfoByUserID(UserID,ref Password, ref UserName,
                ref IsActive, ref PersonID))

                return new clsUser(PersonID, UserID, UserName, Password, IsActive);
            else

                return null;
        }

        public static DataTable GetAllUsersByPersonID(char[] StartWith)
        {
            return clsUserData.GetAllUsersByPersonID(StartWith);

        }
        public static DataTable GetAllUsersByUserID(char[] StartWith)
        {
            return clsUserData.GetAllUsersByUserID(StartWith);

        }

        public static DataTable GetAllUsersByUserName(char[] StartWith)
        {
            return clsUserData.GetAllUsersByUserName(StartWith);

        }
        public static DataTable GetAllUsersByFullName(char[] StartWith)
        {
            return clsUserData.GetAllUsersByFullName(StartWith);

        }
        public static DataTable GetAllUsersByIsActive_True_()
        {
            return clsUserData.GetAllUsersByIsActive_True_();
        }

        public static DataTable GetAllUsersByIsActive_False_()
        {
            return clsUserData.GetAllUsersByIsActive_False_();
        }
        public static bool DeletUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
    }
}
