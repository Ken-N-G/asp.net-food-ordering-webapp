using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MunchHub
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["data"] != null)
                {
                    searchInput.Text = Request.QueryString["data"];
                    string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                    SqlConnection con = new SqlConnection(conStr);
                    setSpecificOrders(con, Convert.ToInt32(Session["user"]));
                } else
                {
                    string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                    SqlConnection con = new SqlConnection(conStr);
                    setAllOrderList(con);
                }
            }
        }

        private DataTable getOrderList(SqlConnection con, int userID)
        {
            DataTable table = new DataTable();
            string getOrdersQuery = "SELECT * FROM [OrderTable] WHERE user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(getOrdersQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            return table;
        }

        private DataTable getDeliveryItemList(SqlConnection con, int orderID)
        {
            DataTable table = new DataTable();
            string getDeliveryItemsQuery = "SELECT DeliveryTable.quantity, ProductTable.product_name, ProductTable.price, ProductTable.img_url FROM [DeliveryTable] INNER JOIN [ProductTable] ON DeliveryTable.product_id = ProductTable.product_id WHERE order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(getDeliveryItemsQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            return table;
        }

        protected void orderListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                double totalPrice = 0.0;
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                ListView deliveryItemListView = (ListView)e.Item.FindControl("deliveryItemListView");
                DataRowView order = (DataRowView)e.Item.DataItem;
                DataTable allDeliveryItems = getDeliveryItemList(con, Convert.ToInt32(order["order_id"]));
                foreach (DataRow row in allDeliveryItems.Rows)
                {
                    double price = Convert.ToDouble(row["price"]);
                    double quantity = Convert.ToInt32(row["quantity"]);
                    double newPrice = price * quantity;
                    totalPrice = totalPrice + newPrice;
                    row["price"] = newPrice;
                }
                System.Web.UI.WebControls.Label totalPriceLabel = (System.Web.UI.WebControls.Label)e.Item.FindControl("totalPriceLabel");
                totalPriceLabel.Text = "RM " + totalPrice.ToString();
                if (order["status"].ToString() != "Delivered")
                {
                    Panel buttonPanel = (Panel)e.Item.FindControl("buttonPanel");
                    buttonPanel.Visible = false;
                } else
                {
                    Panel buttonPanel = (Panel)e.Item.FindControl("buttonPanel");
                    buttonPanel.Visible = true;
                }
                deliveryItemListView.DataSource = allDeliveryItems;
                deliveryItemListView.DataBind();
            }
        }

        protected void orderListView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "reviewOrder")
            {
                Response.Redirect("CustomerFeedback.aspx?data=" + e.CommandArgument.ToString());
            }
        }

        private void setAllOrderList(SqlConnection con)
        {
            DataTable allOrders = getOrderList(con, Convert.ToInt32(Session["user"]));
            orderListView.DataSource = allOrders;
            orderListView.DataBind();
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            setSpecificOrders(con, Convert.ToInt32(Session["user"]));
        }

        private void setSpecificOrders(SqlConnection con, int userID)
        {
            DataTable table = new DataTable();
            string specificOrderQuery = "SELECT * FROM [OrderTable] WHERE user_id = @UserID AND OrderTable.order_id LIKE '%' + @SearchQuery + '%'";
            using (SqlCommand cmd = new SqlCommand(specificOrderQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@SearchQuery", searchInput.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();

                orderListView.DataSource = table;
                orderListView.DataBind();
            }
        }
    }
}