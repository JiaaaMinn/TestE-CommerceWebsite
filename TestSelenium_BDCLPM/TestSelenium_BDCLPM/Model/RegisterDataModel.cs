using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSelenium_BDCLPM.Model
{
    public struct RegisterDataModel
    {
        public string FullName;
        public string CompanyName;
        public string Email;
        public string Phone;
        public string Address;
        public string Country;
        public string City;
        public string State;
        public int ZipCode;
        public string Password;
        public string ConfirmPassword;
        public string AdminEmail;
        public string AdminPassword;
        public string XPathRegister;
        public string XPathAdmin;
        public string XPathUser;

        public RegisterDataModel(string fullName, string companyName, string email, string phone, string address, string country, string city, string state, int zipCode, string password, string confirmPassword, string adminEmail, string adminPassword, string xpathRegister, string xpathAdmin, string xpathUser)
        {
            FullName = fullName;
            CompanyName = companyName;
            Email = email;
            Phone = phone;
            Address = address;
            Country = country;
            City = city;
            State = state;
            ZipCode = zipCode;
            Password = password;
            ConfirmPassword = confirmPassword;
            AdminEmail = adminEmail;
            AdminPassword = adminPassword;
            XPathRegister = xpathRegister;
            XPathAdmin = xpathAdmin;
            XPathUser = xpathUser;
        }
    }

}
