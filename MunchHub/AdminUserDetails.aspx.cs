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
    public partial class WebForm16 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            } else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("AdminManageUsers.aspx");
            }
            if (!IsPostBack)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string customerSearchQuery = "SELECT * FROM [CustomerTable] WHERE user_id = @UserID";
                using (SqlCommand cmd = new SqlCommand(customerSearchQuery, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Request.QueryString["data"]));
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        userIDLabel.Text = "Customer - " + Request.QueryString["data"];
                        nameInput.Text = rdr.GetString(1);
                        emailInput.Text = rdr.GetString(2);
                        homeAddressInput.Text = rdr.GetString(3);
                    }
                }
                con.Close();
                if (isUserSuspended(con, Convert.ToInt32(Request.QueryString["data"])))
                {
                    suspendButton.Text = "Reactivate";
                } else
                {
                    suspendButton.Text = "Suspend";
                }
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

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (countCustomer() > 0)
            {
                emailErrorLabel.Text = "This email is used by another account.";
            }
            else
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


        protected void suspendButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            if (isUserSuspended(con, Convert.ToInt32(Request.QueryString["data"])))
            {
                reactivateUser(con, Convert.ToInt32(Request.QueryString["data"]));
                suspendButton.Text = "Suspend";
            } else
            {
                suspendUser(con, Convert.ToInt32(Request.QueryString["data"]));
                suspendButton.Text = "Reactivate";
            }
            Response.Redirect("AdminManageUsers.aspx");
        }

        private bool isUserSuspended(SqlConnection con, int userID)
        {
            string evaluateStatusQuery = "SELECT COUNT(*) FROM [ModerationTable] WHERE user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(evaluateStatusQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                int userCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                if (userCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void reactivateUser(SqlConnection con, int userID)
        {
            string createOrderQuery = "DELETE FROM [ModerationTable] WHERE user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(createOrderQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void suspendUser(SqlConnection con, int userID)
        {
            string createOrderQuery = "INSERT INTO [ModerationTable] (ticket_id, user_id, admin_id) VALUES (@TicketID, @UserID, @AdminID)";
            string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
            string ticketID = "9" + timestamp;
            using (SqlCommand cmd = new SqlCommand(createOrderQuery, con))
            {
                cmd.Parameters.AddWithValue("@TicketID", Convert.ToInt32(ticketID));
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@AdminID", Convert.ToInt32(Session["user"]));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}