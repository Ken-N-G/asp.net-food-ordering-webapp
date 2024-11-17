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

namespace MunchHub
{
    public partial class WebForm14 : System.Web.UI.Page
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
                productView.DataSource = getAllProducts(con);
                productView.DataBind();
            }
        }

        private void setSpecificProducts(SqlConnection con)
        {
            DataTable table = new DataTable();
            string specificOrderQuery = "SELECT * FROM [ProductTable] WHERE product_id LIKE '%' + @SearchQuery + '%'";
            using (SqlCommand cmd = new SqlCommand(specificOrderQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchQuery", searchInput.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            productView.DataSource = table;
            productView.DataBind();
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        protected void productView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("viewDetails"))
            {
                Response.Redirect("AdminProductDetails.aspx?data=" + e.CommandArgument.ToString());
            } else if (e.CommandName.Equals("delete"))
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                if (checkForProductRelations(con, Convert.ToInt32(e.CommandArgument)) > 0)
                {
                    productErrorLabel.Text = "Product with ID " + e.CommandArgument + " have been recorded in the Delivery or Cart tables. Remove them before trying again!";
                } else
                {
                    productErrorLabel.Text = "";
                    deleteProduct(con, Convert.ToInt32(e.CommandArgument));
                }
            }
        }

        private void deleteProduct(SqlConnection con, int productID)
        {
            string deleteItemQuery = "DELETE FROM [ProductTable] WHERE product_id = @ProductID";
            using (SqlCommand cmd = new SqlCommand(deleteItemQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private int checkForProductRelations(SqlConnection con, int productID)
        {
            int relationCount = 0;
            string cartCountQuery = "SELECT COUNT(*) FROM [CartTable] WHERE product_id = @ProductID";
            using (SqlCommand cmd = new SqlCommand(cartCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                con.Open();
                relationCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            string deliveryCountQuery = "SELECT COUNT(*) FROM [DeliveryTable] WHERE product_id = @ProductID";
            using (SqlCommand cmd = new SqlCommand(deliveryCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                con.Open();
                relationCount = relationCount + Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return relationCount;
        }

        private DataTable getAllProducts(SqlConnection con)
        {
            DataTable products = new DataTable();
            string allProductsQuery = "SELECT * FROM [ProductTable]";
            using (SqlCommand cmd = new SqlCommand(allProductsQuery, con))
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(products);
                con.Close();
            }
            return products;
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            setSpecificProducts(con);
        }

        protected void addButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAddProduct.aspx");
        }
    }
}