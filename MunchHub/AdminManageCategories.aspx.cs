using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace MunchHub
{
    public partial class WebForm13 : System.Web.UI.Page
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
                categoryView.DataSource = getAllCategories(con);
                categoryView.DataBind();
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            setSpecificCategories(con);
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            categoryErrorLabel.Text = ValidationUtilities.ValidateCustomerName(addInput.Text.Trim());
            validateCategoryName(con, addInput.Text.Trim());
            if (categoryErrorLabel.Text == "")
            {
                addCategory(con);
                categoryView.DataSource = getAllCategories(con);
                categoryView.DataBind();
            }
        }

        private void addCategory(SqlConnection con)
        {
            string registerCustomerQuery = "INSERT INTO [CategoryTable] (category_id, category_name) VALUES (@CategoryID, @CategoryName)";
            using (SqlCommand cmd = new SqlCommand(registerCustomerQuery, con))
            {
                string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
                timestamp = "3" + timestamp;
                int userID = Convert.ToInt32(timestamp);
                cmd.Parameters.AddWithValue("@CategoryID", userID);
                cmd.Parameters.AddWithValue("@CategoryName", addInput.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            };
        }

        private void validateCategoryName(SqlConnection con, string name)
        {
            if (checkForSimilarCategoryNames(con, name) > 0)
            {
                categoryErrorLabel.Text = "There is a category with this name that already exists! Try another name";
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

        protected void categoryView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("edit"))
            {
                Response.Redirect("AdminCategoryDetails.aspx?data=" + e.CommandArgument.ToString());
            } else if (e.CommandName.Equals("deleteItem"))
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                int relationsCount = checkForCategoryRelations(con, Convert.ToInt32(e.CommandArgument));
                if (relationsCount > 0 )
                {
                    categoryErrorLabel.Text = "There some products that are related to this category with id = " + e.CommandArgument + ". Delete them before deleting this category!";
                } else
                {
                    categoryErrorLabel.Text = "";
                    deleteCategory(con, Convert.ToInt32(e.CommandArgument));
                    categoryView.DataSource = getAllCategories(con);
                    categoryView.DataBind();
                }
            }
        }

        private void setSpecificCategories(SqlConnection con)
        {
            DataTable table = new DataTable();
            string specificOrderQuery = "SELECT * FROM [CategoryTable] WHERE category_id LIKE '%' + @SearchQuery + '%'";
            using (SqlCommand cmd = new SqlCommand(specificOrderQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchQuery", searchInput.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            categoryView.DataSource = table;
            categoryView.DataBind();
        }

        private void deleteCategory(SqlConnection con, int categoryID)
        {
            string deleteItemQuery = "DELETE FROM [CategoryTable] WHERE category_id = @CategoryID";
            using (SqlCommand cmd = new SqlCommand(deleteItemQuery, con))
            {
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private int checkForCategoryRelations(SqlConnection con, int categoryID)
        {
            int relationCount = 0;
            string categoryQuery = "SELECT COUNT(*) FROM [ProductTable] WHERE category_id = @CategoryID";
            using (SqlCommand cmd = new SqlCommand(categoryQuery, con))
            {
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                con.Open();
                relationCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return relationCount;
        }

        private DataTable getAllCategories(SqlConnection con)
        {
            DataTable table = new DataTable();
            string specificOrderQuery = "SELECT * FROM [CategoryTable]";
            using (SqlCommand cmd = new SqlCommand(specificOrderQuery, con))
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            return table;
        }
    }
}