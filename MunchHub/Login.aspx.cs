using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace MunchHub
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected int countAdmin(SqlConnection con)
        {
            string adminCountQuery = "SELECT COUNT(*) FROM [AdminTable] WHERE email = @Email and password = @Password";
            int userCount = 0;
            using(SqlCommand cmd = new SqlCommand(adminCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", passwordInput.Text.Trim());
                con.Open();
                userCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return userCount;
        }

        protected string getCustomers(SqlConnection con)
        {
            string customerSearchQuery = "SELECT user_id FROM [CustomerTable] WHERE email = @Email and password = @Password";
            string customerId = "";
            using (SqlCommand cmd = new SqlCommand(customerSearchQuery, con))
            {

                cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", passwordInput.Text.Trim());
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    customerId = rdr.GetInt32(0).ToString();
                }
            }
            con.Close();
            return customerId;
        }

        protected string getAdmins(SqlConnection con)
        {
            string adminSearchQuery = "SELECT admin_id FROM [AdminTable] WHERE email = @Email and password = @Password";
            string adminId = "";
            using (SqlCommand cmd = new SqlCommand(adminSearchQuery, con))
            {

                cmd.Parameters.AddWithValue("@Email", emailInput.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", passwordInput.Text.Trim());
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    adminId = rdr.GetInt32(0).ToString();
                }
            }
            con.Close();
            return adminId;
        }
        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateLogin())
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                int adminCount = countAdmin(con);
                if (adminCount == 0)
                {
                    string customerId = getCustomers(con);
                    if (customerId == "")
                    {
                        loginErrorLabel.Text = "Login failed. Your credentials maybe incorrect!";
                    } 
                    else if (checkForSuspension(con, Convert.ToInt32(customerId)))
                    {
                        loginErrorLabel.Text = "";
                        Session["user"] = customerId;

                        Response.Redirect("CustomerDashboard.aspx");
                    } else
                    {
                        loginErrorLabel.Text = "Login Failed. The account associated with the login details has been suspended!";
                    }
                }
                else if (adminCount >= 1)
                {
                    string adminId = getAdmins(con);
                    if (adminId == "")
                    {
                        loginErrorLabel.Text = "Login failed. Your credentials maybe incorrect!";
                    } 
                    else
                    {
                        loginErrorLabel.Text = "";
                        Session["user"] = adminId;
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    loginErrorLabel.Text = "Login failed. Your credentials maybe incorrect!";
                }
            }
     
        }

        private bool checkForSuspension(SqlConnection con, int userID)
        {
            string suspensionCountQuery = "SELECT COUNT(*) FROM [ModerationTable] WHERE user_id = @UserID";
            int userCount = 0;
            using (SqlCommand cmd = new SqlCommand(suspensionCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                userCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            if (userCount > 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        private bool ValidateLogin()
        {
            string emailError = ValidationUtilities.ValidateEmail(emailInput.Text.Trim());
            string passwordError = ValidationUtilities.ValidateLoginPassword(passwordInput.Text.Trim());
            emailErrorLabel.Text = emailError;
            passwordErrorLabel.Text = passwordError;
            if (emailError =="" && passwordError =="")
            {
                return true;
            }
            return false;
        }
    }
}