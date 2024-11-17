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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                int userID = Convert.ToInt32(Session["user"]);
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                int itemAmount = checkRecentItems(userID, con);
                int orderAmount = checkRecentOrdered(userID, con);
                if (itemAmount > 0)
                {
                    recentsPanel.Visible = true;
                    noRecentsPanel.Visible = false;
                    getRecentOrders(userID, con);
                }
                else
                {
                    recentsPanel.Visible = false;
                    noRecentsPanel.Visible = true;
                }
                if (orderAmount > 0)
                {
                    orderPanel.Visible = true;
                    noOrderPanel.Visible = false;
                    getCurrentOrders(userID, con);
                } else
                {
                    orderPanel.Visible = false;
                    noOrderPanel.Visible = true;
                }
                getItemOfTheDay(con);
            }
        }

        private int checkRecentOrdered(int userID, SqlConnection con)
        {
            int orderAmount = 0;
            string recentOrdersQuery = "SELECT COUNT(*) FROM [OrderTable] WHERE NOT status = 'Delivered' AND user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(recentOrdersQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                orderAmount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            return orderAmount;
        }

        private int checkRecentItems(int userID, SqlConnection con)
        {
            int itemAmount = 0;
            string recentOrdersQuery = "SELECT COUNT(*) FROM [OrderTable] WHERE user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(recentOrdersQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                itemAmount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
            }
            return itemAmount;
        }

        private void getRecentOrders(int userID, SqlConnection con)
        {
            string recentOrdersQuery = "SELECT TOP 4 ProductTable.product_id, ProductTable.product_name, ProductTable.img_url, ProductTable.short_description, ProductTable.price, CategoryTable.category_name FROM [OrderTable] INNER JOIN [DeliveryTable] ON OrderTable.order_id = DeliveryTable.order_id INNER JOIN [ProductTable] ON DeliveryTable.product_id = ProductTable.product_id INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id WHERE user_id = @UserID ORDER BY OrderTable.datetime_ordered DESC";
            using (SqlCommand cmd = new SqlCommand(recentOrdersQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                recentsListView.DataSource = rdr;
                recentsListView.DataBind();
                con.Close();
            }
        }

        private void getCurrentOrders(int userID, SqlConnection con)
        {
            string recentOrdersQuery = "SELECT TOP 7 OrderTable.order_id, ProductTable.product_name, DeliveryTable.quantity FROM [OrderTable] INNER JOIN [DeliveryTable] ON OrderTable.order_id = DeliveryTable.order_id INNER JOIN [ProductTable] ON DeliveryTable.product_id = ProductTable.product_id WHERE NOT OrderTable.status = 'Delivered' AND OrderTable.user_id = @UserID ORDER BY OrderTable.datetime_ordered DESC";
            using (SqlCommand cmd = new SqlCommand(recentOrdersQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                deliveryListView.DataSource = rdr;
                deliveryListView.DataBind();
                con.Close();
            }
        }

        protected void recentsListView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "viewDetails")
            {
                Response.Redirect("CustomerProductDetails.aspx?data=" + e.CommandArgument.ToString());
            }
        }

        protected void deliveryListView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "viewDetails")
            {
                Response.Redirect("CustomerOrders.aspx?data=" + e.CommandArgument.ToString());
            }
        }

        private void getItemOfTheDay(SqlConnection con)
        {
            string itemOfTheDayQuery = "SELECT TOP 1 * FROM [ProductTable] ORDER BY NEWID()";
            using (SqlCommand cmd = new SqlCommand(itemOfTheDayQuery, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    itemOfTheDayImg.ImageUrl = rdr["img_url"].ToString();
                    itemOfTheDayLabel.Text = rdr["product_name"].ToString();
                }
                con.Close();
            }
        }
    }
}