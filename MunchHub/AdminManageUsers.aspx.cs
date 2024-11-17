using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MunchHub
{
    public partial class WebForm12 : System.Web.UI.Page
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
                userView.DataSource = getAllCustomers(con);
                userView.DataBind();
            }
        }

        private DataTable getAllCustomers(SqlConnection con)
        {
            DataTable users = new DataTable();
            string allCategoriesQuery = "SELECT * FROM [CustomerTable]";
            using (SqlCommand cmd = new SqlCommand(allCategoriesQuery, con))
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(users);
                con.Close();
            }
            users = evaluateUserStatus(con, users);
            return users;
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
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
                } else
                {
                    return false;
                }
            }
        }

        private DataTable evaluateUserStatus(SqlConnection con, DataTable table)
        {
            table.Columns.Add("status", typeof(string));
            foreach (DataRow row in table.Rows)
            {
                bool isSuspended = isUserSuspended(con, Convert.ToInt32(row["user_id"]));
                if (isSuspended)
                {
                    row["status"] = "Suspended";
                }
                else
                {
                    row["status"] = "Active";
                }
            }
            return table;
        }

        protected void userView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                DataTable users = (DataTable)userView.DataSource;
                foreach (DataRow user in users.Rows)
                {
                    if (user["status"].ToString() == "Suspended")
                    {
                        Button suspendButton = (Button)e.Item.FindControl("suspendButton");
                        suspendButton.Text = "Reactivate";
                    } else
                    {
                        Button suspendButton = (Button)e.Item.FindControl("suspendButton");
                        suspendButton.Text = "Suspended";
                    }
                }
            }
        }

        protected void userView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("viewDetails"))
            {
                Response.Redirect("AdminUserDetails.aspx?data=" + e.CommandArgument.ToString());
            } else if (e.CommandName.Equals("suspendUser"))
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                if (isUserSuspended(con, Convert.ToInt32(e.CommandArgument)))
                {
                    reactivateUser(con, Convert.ToInt32(e.CommandArgument));
                    Button button = (Button)e.Item.FindControl("suspendButton");
                    button.Text = "Suspend";
                } else
                {
                    suspendUser(con, Convert.ToInt32(e.CommandArgument));
                    Button button = (Button)e.Item.FindControl("suspendButton");
                    button.Text = "Reactivate";
                }
                getAllCustomers(con);
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

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            setSpecificUsers(con);
        }

        private void setSpecificUsers(SqlConnection con)
        {
            DataTable table = new DataTable();
            string specificOrderQuery = "SELECT * FROM [CustomerTable] WHERE user_id LIKE '%' + @SearchQuery + '%'";
            using (SqlCommand cmd = new SqlCommand(specificOrderQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchQuery", searchInput.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            table = evaluateUserStatus(con, table);
            userView.DataSource = table;
            userView.DataBind();

        }
    }
}