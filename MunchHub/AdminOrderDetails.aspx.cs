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
    public partial class WebForm17 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            } else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("AdminManageOrders.aspx");
            }
            if (!IsPostBack)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                DataTable items = getDeliveryItemList(con);
                foreach (DataRow row in items.Rows)
                {
                    double price = Convert.ToDouble(row["price"]);
                    double quantity = Convert.ToInt32(row["quantity"]);
                    double newPrice = price * quantity;
                    row["price"] = newPrice;
                }
                setOrderInformation(con);
                deliveryItemListView.DataSource = items;
                deliveryItemListView.DataBind();
            }
        }

        private void updateOrderStatus(SqlConnection con, string newStatus)
        {
            string updateOrderStatus = "UPDATE [OrderTable] SET status = @NewStatus WHERE order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(updateOrderStatus, con))
            {
                cmd.Parameters.AddWithValue("@NewStatus", newStatus);
                cmd.Parameters.AddWithValue("@OrderID", Request.QueryString["data"]);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        private void setOrderInformation(SqlConnection con)
        {
            string getOrderInformation = "SELECT * FROM [OrderTable] WHERE order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(getOrderInformation, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@OrderID", Request.QueryString["data"]);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    idLabel.Text = Request.QueryString["data"];
                    dateLabel.Text = rdr.GetDateTime(1).ToString();
                    statusLabel.Text = rdr.GetString(2);
                }
                con.Close();
            }
        }

        private DataTable getDeliveryItemList(SqlConnection con)
        {
            DataTable table = new DataTable();
            string getDeliveryItemsQuery = "SELECT DeliveryTable.quantity, ProductTable.product_name, ProductTable.price, ProductTable.img_url FROM [DeliveryTable] INNER JOIN [ProductTable] ON DeliveryTable.product_id = ProductTable.product_id WHERE order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(getDeliveryItemsQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@OrderID", Request.QueryString["data"]);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            return table;
        }

        protected void pendingButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            updateOrderStatus(con, "Pending");
            statusLabel.Text = "Pending";
            Response.Redirect("AdminManageOrders.aspx");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            updateOrderStatus(con, "Cancelled");
            statusLabel.Text = "Cancelled";
            Response.Redirect("AdminManageOrders.aspx");
        }

        protected void deliverButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            updateOrderStatus(con, "Deliver");
            statusLabel.Text = "Deliver";
            Response.Redirect("AdminManageOrders.aspx");
        }

        protected void deliveredButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            updateOrderStatus(con, "Delivered");
            statusLabel.Text = "Delivered";
            Response.Redirect("AdminManageOrders.aspx");
        }
    }
}