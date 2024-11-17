using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MunchHub
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateRegister() && countCustomer() < 1)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string registerCustomerQuery = "INSERT INTO [CustomerTable] (user_id, name, email, home_address, password) VALUES (@UserID, @Name, @Email, @HomeAddress, @Password)";
                using (SqlCommand cmd = new SqlCommand(registerCustomerQuery, con))
                {
                    string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
                    timestamp = "1" + timestamp;
                    int userID = Convert.ToInt32(timestamp);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Name", nameInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@HomeAddress", homeAddressInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", passwordInput.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Session["user"] = userID.ToString();
                };
                Response.Redirect("CustomerDashboard.aspx");
            } else
            {
                emailErrorLabel.Text = "There is an account that already uses this email";
            }
        }

        private bool ValidateRegister()
        {
            string nameError = ValidationUtilities.ValidateCustomerName(nameInput.Text.Trim());
            string emailError = ValidationUtilities.ValidateEmail(emailInput.Text.Trim());
            string homeAddressError = ValidationUtilities.ValidateHomeAddress(homeAddressInput.Text.Trim());
            string passwordError = ValidationUtilities.ValidateRegisterPassword(passwordInput.Text.Trim(), conPasswordInput.Text.Trim());
            string confirmPasswordError = ValidationUtilities.ValidateRegisterPassword(conPasswordInput.Text.Trim(), passwordInput.Text.Trim());
            nameErrorLabel.Text = nameError;
            emailErrorLabel.Text = emailError;
            addressErrorLabel.Text = homeAddressError;
            passwordErrorLabel.Text = passwordError;
            confirmPasswordErrorLabel.Text = confirmPasswordError;
            if (nameError == "" && emailError == "" && homeAddressError == "" && passwordError == "" && confirmPasswordError == "")
            {
                return true;
            }
            return false;
        }

        private int countCustomer()
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            string adminCountQuery = "SELECT COUNT(*) FROM [CustomerTable] WHERE email = @Email";
            int userCount = 0;
            using (SqlCommand cmd = new SqlCommand(adminCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                con.Open();
                userCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return userCount;
        }
    }
}