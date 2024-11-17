using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MunchHub
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("CustomerDashboard.aspx");
            }
            if (!IsPostBack && Request.QueryString["data"] != null)
            {
                int orderID = Convert.ToInt32(Request.QueryString["data"]);
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                getOrderDetails(con, orderID);
            }
        }

        private void getOrderDetails(SqlConnection con, int orderID)
        {
            string orderDetailsQuery = "SELECT OrderTable.datetime_ordered, OrderTable.status FROM [OrderTable] WHERE order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(orderDetailsQuery, con))
            {
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    orderIDLabel.Text = "Order - " + orderID.ToString();
                    dateOrderedLabel.Text = rdr.GetDateTime(0).ToString();
                    statusLabel.Text = rdr.GetString(1);
                }
                con.Close();
                totalPriceLabel.Text = "RM " + getOrderTotalPrice(con, orderID);
            }
        }

        private double getOrderTotalPrice(SqlConnection con, int orderID)
        {
            DataTable table = new DataTable();
            string getCartItemsQuery = "SELECT DeliveryTable.quantity, ProductTable.price FROM [DeliveryTable] INNER JOIN [ProductTable] ON DeliveryTable.product_id = ProductTable.product_id WHERE DeliveryTable.order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(getCartItemsQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            double totalPrice = 0.0;
            foreach (DataRow row in table.Rows)
            {
                double price = Convert.ToDouble(row["price"]);
                double quantity = Convert.ToInt32(row["quantity"]);
                double newPrice = price * quantity;
                totalPrice = totalPrice + newPrice;
            }
            return  totalPrice;
        }

        protected void reviewOrderButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerOrders.aspx?data=" + Request.QueryString["data"]);
        }
    }
}