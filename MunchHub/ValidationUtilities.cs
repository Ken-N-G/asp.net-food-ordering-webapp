using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace MunchHub
{
    public static class ValidationUtilities
    {

        private static bool ContainsSpecialCharacter(string input) 
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var ch in specialChar)
            {
                if (input.Contains(ch))
                {
                    return true;
                }
            }
            return false;
        }
        
        private static bool ContainsNumber(string input)
        {
            string numbers = "0123456789";
            foreach(var num in numbers)
            {
                if (input.Contains(num))
                {
                    return true;
                }
            }
            return false;
        }

        private static char GetSpecialCharacter(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var ch in specialChar)
            {
                if (input.Contains(ch))
                {
                    return ch;
                }
            }
            return 'a';
        }

        public static string ValidateProductName(string name)
        {
            if (name.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter the product name.";
            }
            else if (ContainsSpecialCharacter(name))
            {
                char illegalChar = GetSpecialCharacter(name);
                return "Special characters like " + illegalChar + " cannot be in the product name.";
            }
            else if (ContainsNumber(name))
            {
                return "Numbers cannot be in the name.";
            }
            else if (name.Length > 50)
            {
                return "The product's name cannot be more than 50 characters long.";
            }
            return "";
        }

        public static string ValidateDescription(string description)
        {
            if (description.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter the product description.";
            }
            else if (description.Length > 200)
            {
                return "The product's description cannot be more than 200 characters long.";
            }
            return "";
        }

        public static string ValidateShortDescription(string description)
        {
            if (description.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter the short product description.";
            }
            else if (description.Length > 50)
            {
                return "The product's short description cannot be more than 50 characters long.";
            }
            return "";
        }

        public static string ValidatePrice(double price)
        {
            if (price <= 0)
            {
                return "The price cannot be less than or equal to 0.";
            }
            return "";
        }

        public static string ValidateCustomerName(string name)
        {
            if (name.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter your name.";
            } else if (ContainsSpecialCharacter(name))
            {
                char illegalChar = GetSpecialCharacter(name);
                return "Special characters like " + illegalChar + " cannot be in your name.";
            } else if (ContainsNumber(name))
            {
                return "Numbers cannot be in your name.";
            }
            return "";
        }

        public static string ValidateEmail(string email)
        {
            if (email.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter your email.";
            }
            try
            {
                MailAddress add = new MailAddress(email);
                return "";
            } catch (Exception e)
            {
                return "Enter an email with in the format someone@example.com.";
            }
        }

        public static string ValidateRegisterPassword(string password, string confirmPassword)
        {
            if (password.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter your password.";
            } else if (password.Length < 9)
            {
                return "Your password must be at least 8 characters long.";
            } 
            else if (!ContainsSpecialCharacter(password))
            {
                return "Add at least one special character like @!#$";
            }
            else if (!password.Equals(confirmPassword))
            {
                return "Your passwords do not match.";
            }
            return "";
        }
        
        public static string ValidateHomeAddress(string address)
        {
            if (address.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter your home address.";
            }
            return "";
        }

        public static string ValidateLoginPassword(string password)
        {
            if (password.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter your password.";
            }
            return "";
        }

        public static string ValidateFeedback(string feedback)
        {
            if (feedback.IsNullOrWhiteSpace())
            {
                return "You left this empty. Enter a comment on your order.";
            }
            return "";
        }
    }
}