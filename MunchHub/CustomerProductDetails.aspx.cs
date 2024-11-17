using Microsoft.Ajax.Utilities;
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
    public partial class WebForm10 : System.Web.UI.Page
    {
        protected int quantity
        {
            get
            {
                if (ViewState["Quantity"] != null)
                    return (int)ViewState["Quantity"];
                else
                    return 1;
            }
            set
            {
                ViewState["Quantity"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            } else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("CustomerMenu.aspx");
            }
            if (!IsPostBack)
            {
                int productID = Convert.ToInt32(Request.QueryString["data"]);
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string specificCategoryQuery = "SELECT ProductTable.product_name, ProductTable.description, ProductTable.img_url, ProductTable.price, CategoryTable.category_name FROM [ProductTable] INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id WHERE ProductTable.product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(specificCategoryQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        productNameLabel.Text = rdr.GetString(0);
                        priceLabel.Text = "RM " + rdr.GetDouble(3).ToString();
                        image1.ImageUrl = rdr.GetString(2);
                        image2.ImageUrl = rdr.GetString(2);
                        descriptionLabel.Text = rdr.GetString(1);
                        categoryLabel.Text = rdr.GetString(4);
                        quantityLabel.Text = quantity.ToString(); 
                    }
                    con.Close();
                }
            }
        }

        protected void removeItemButton_Click(object sender, EventArgs e)
        {
            if (quantity == 1)
            {
                return;
            } else
            {
                quantity--;
                quantityLabel.Text = quantity.ToString();
            }
        }

        protected void addItemButton_Click(object sender, EventArgs e)
        {
            quantity++;
            quantityLabel.Text = quantity.ToString();
        }

        protected void addToCartButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            addToCart(con, Convert.ToInt32(Request.QueryString["data"]), Convert.ToInt32(Session["user"]));
        }

        private void addToCart(SqlConnection con, int productID, int userID)
        {
            if (checkForExistingItems(con, productID, userID) > 0)
            {
                updateCartItem(con, productID, userID);
            } else
            {
                string registerCustomerQuery = "INSERT INTO [CartTable] (item_id, quantity, user_id, product_id) VALUES (@ItemID, @Quantity, @UserID, @ProductID)";
                using (SqlCommand cmd = new SqlCommand(registerCustomerQuery, con))
                {
                    string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
                    timestamp = "5" + timestamp;
                    int itemID = Convert.ToInt32(timestamp);
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(Request.QueryString["data"]));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                };
            }
            Response.Redirect("CustomerMenu.aspx");
        }

        private void updateCartItem(SqlConnection con, int productID, int userID)
        {
            string itemQuantityQuery = "SELECT quantity FROM [CartTable] WHERE product_id = @ProductID and user_id = @UserID";
            int itemQuantity = 0;
            using (SqlCommand cmd = new SqlCommand(itemQuantityQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    itemQuantity = rdr1.GetInt32(0);
                }
                con.Close();
            }
            itemQuantity = itemQuantity + quantity;
            string itemUpdateQuery = "UPDATE [CartTable] SET quantity = @Quantity WHERE product_id = @ProductID and user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(itemUpdateQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Quantity", itemQuantity);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private int checkForExistingItems(SqlConnection con, int productID, int userID)
        {
            string itemCountQuery = "SELECT COUNT(*) FROM [CartTable] WHERE product_id = @ProductID and user_id = @UserID";
            int itemCount = 0;
            using (SqlCommand cmd = new SqlCommand(itemCountQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                itemCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            con.Close();
            return itemCount;
        }
    }
}