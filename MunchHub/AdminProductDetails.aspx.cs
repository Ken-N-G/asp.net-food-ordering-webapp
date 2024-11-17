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
    public partial class WebForm19 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("AdminManageProduct.aspx");
            }
            if (!IsPostBack)
            {
                int productID = Convert.ToInt32(Request.QueryString["data"]);
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string specificCategoryQuery = "SELECT ProductTable.product_name, ProductTable.description, ProductTable.short_description, ProductTable.img_url, ProductTable.price, CategoryTable.category_name FROM [ProductTable] INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id WHERE ProductTable.product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(specificCategoryQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        productIDLabel.Text = Request.QueryString["data"];
                        productNameLabel.Text = rdr.GetString(0);
                        priceLabel.Text = "RM " + rdr.GetDouble(4).ToString();
                        image1.ImageUrl = rdr.GetString(3);
                        image2.ImageUrl = rdr.GetString(3);
                        descriptionLabel.Text = rdr.GetString(1);
                        shortDescriptionLabel.Text = rdr.GetString(2);
                        categoryLabel.Text = rdr.GetString(5);
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

        protected void editButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminEditProduct.aspx?data=" + Request.QueryString["data"]);
        }

        protected void deleteButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            if (checkForProductRelations(con, Convert.ToInt32(Request.QueryString["data"])) > 0)
            {
                productErrorLabel.Text = "Product with ID " + Request.QueryString["data"] + " have been recorded in the Delivery or Cart tables. Remove them before trying again!";
            }
            else
            {
                productErrorLabel.Text = "";
                deleteProduct(con, Convert.ToInt32(Request.QueryString["data"]));
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
    }
}