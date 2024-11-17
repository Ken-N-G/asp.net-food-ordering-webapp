using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MunchHub
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string customerSearchQuery = "SELECT * FROM [CustomerTable] WHERE user_id = @UserID";
                using (SqlCommand cmd = new SqlCommand(customerSearchQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["user"]));
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        nameInput.Text = rdr.GetString(1);
                        emailInput.Text = rdr.GetString(2);
                        homeAddressInput.Text = rdr.GetString(3);
                        changePasswordInput.Text = rdr.GetString(4);
                        confirmPasswordInput.Text = rdr.GetString(4);
                    }
                }
                con.Close();
            }
        }

        private bool ValidateUserAccountChange()
        {
            string nameError = ValidationUtilities.ValidateCustomerName(nameInput.Text.Trim());
            string emailError = ValidationUtilities.ValidateEmail(emailInput.Text.Trim());
            string homeAddressError = ValidationUtilities.ValidateHomeAddress(homeAddressInput.Text.Trim());
            string passwordError = ValidationUtilities.ValidateRegisterPassword(changePasswordInput.Text.Trim(), confirmPasswordInput.Text.Trim());
            string confirmPasswordError = ValidationUtilities.ValidateRegisterPassword(confirmPasswordInput.Text.Trim(), changePasswordInput.Text.Trim());
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
            string adminCountQuery = "SELECT COUNT(*) FROM [CustomerTable] WHERE NOT user_id = @UserID AND email = @Email";
            int userCount = 0;
            using (SqlCommand cmd = new SqlCommand(adminCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["user"]));
                con.Open();
                userCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return userCount;
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (countCustomer() > 0)
            {
                emailErrorLabel.Text = "This email is used by another account.";
            } else
            {
                if (ValidateUserAccountChange())
                {
                    string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                    SqlConnection con = new SqlConnection(conStr);
                    string customerUpdateQuery = "UPDATE CustomerTable SET name = @Name, email = @Email, home_address = @HomeAddress, password = @Password WHERE user_id = @UserID";
                    using (SqlCommand cmd = new SqlCommand(customerUpdateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["user"]));
                        cmd.Parameters.AddWithValue("@Name", nameInput.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                        cmd.Parameters.AddWithValue("@HomeAddress", homeAddressInput.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", changePasswordInput.Text.Trim());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }
    }
}