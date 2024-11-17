using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace MunchHub
{
    public partial class WebForm18 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            } else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("AdminManageCategory.aspx");
            }
            if (!IsPostBack)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string getOrderInformation = "SELECT * FROM [CategoryTable] WHERE category_id = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(getOrderInformation, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@CategoryID", Request.QueryString["data"]);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        categoryLabel.Text = "Category - " + rdr.GetInt32(0).ToString();
                        nameInput.Text = rdr.GetString(1);
                    }
                    con.Close();
                }
            }
        }
        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        protected void saveChangesButton_Click(object sender, EventArgs e)
        {
            string errorText = ValidationUtilities.ValidateCustomerName(nameInput.Text.Trim());
            nameErrorLabel.Text = errorText;
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            validateCategoryName(con, nameInput.Text.Trim());
            if (errorText == "")
            {
                string updateOrderStatus = "UPDATE [CategoryTable] SET category_name = @NewName WHERE category_id = @CategoryID";
                using (SqlCommand cmd = new SqlCommand(updateOrderStatus, con))
                {
                    cmd.Parameters.AddWithValue("@NewName", nameInput.Text);
                    cmd.Parameters.AddWithValue("@CategoryID", Request.QueryString["data"]);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                Response.Redirect("AdminManageCategories.aspx");
            }
        }

        private void validateCategoryName(SqlConnection con, string name)
        {
            if (checkForSimilarCategoryNames(con, name) > 0)
            {
                nameErrorLabel.Text = "There is a category with this name that already exists! Try another name";
            }
        }

        private int checkForSimilarCategoryNames(SqlConnection con, string name)
        {
            int similarCount = 0;
            string adminCountQuery = "SELECT COUNT(*) FROM [CategoryTable] WHERE LOWER(category_name) = LOWER(@category_name)";
            using (SqlCommand cmd = new SqlCommand(adminCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@category_name", name);
                con.Open();
                similarCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return similarCount;
        }
    }
}