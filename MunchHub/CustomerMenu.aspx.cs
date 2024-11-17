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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                getAllProducts(con);
                getAllCategories(con);
            }
        }

        private void getAllCategories(SqlConnection con)
        {
            string allCategoriesQuery = "SELECT * FROM [CategoryTable]";
            using (SqlCommand cmd = new SqlCommand(allCategoriesQuery, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ListItem defaultItem = new ListItem("all categories", "default");
                categoryDropdownList.DataSource = rdr;
                categoryDropdownList.DataTextField = "category_name";
                categoryDropdownList.DataValueField = "category_id";
                categoryDropdownList.DataBind();
                categoryDropdownList.Items.Insert(0, defaultItem);
                con.Close();
            }
        }

        private void getProductsFromSpecificSearchAndCategory(SqlConnection con, string categoryName, string searchQuery)
        {
            string specificProductQuery = "";
            if (categoryName != "all categories")
            {
                specificProductQuery = "SELECT * FROM [ProductTable] INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id WHERE CategoryTable.category_name = @CategoryName AND ProductTable.product_name LIKE '%' + @SearchQuery + '%'";
                using (SqlCommand cmd = new SqlCommand(specificProductQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    menuListView.DataSource = rdr;
                    menuListView.DataBind();
                    con.Close();
                }
            } else
            {
                specificProductQuery = "SELECT * FROM [ProductTable] INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id WHERE ProductTable.product_name LIKE '%' + @SearchQuery + '%'";
                using (SqlCommand cmd = new SqlCommand(specificProductQuery, con))
                {
                    cmd.Parameters.AddWithValue("@SearchQuery", searchQuery);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    menuListView.DataSource = rdr;
                    menuListView.DataBind();
                    con.Close();
                }
            }
        }


        private void getAllProducts(SqlConnection con)
        {
            string allProductsQuery = "SELECT * FROM [ProductTable] INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id";
            using (SqlCommand cmd = new SqlCommand(allProductsQuery, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                menuListView.DataSource = rdr;
                menuListView.DataBind();
                con.Close();
            }
        }

        protected void menuListView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("viewDetails"))
            {
                Response.Redirect("CustomerProductDetails.aspx?data="+ e.CommandArgument.ToString());
            }
        }

        protected void categoryDropdownList_selectionChange(object sender, EventArgs e)
        {
            runSearchAndCategoryQuery();
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            runSearchAndCategoryQuery();
        }

        private void runSearchAndCategoryQuery()
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            string selectedCategory = categoryDropdownList.SelectedItem.Text;
            string searchQuery = searchInput.Text.Trim();
            getProductsFromSpecificSearchAndCategory(con, selectedCategory, searchQuery);
        }
    }
}