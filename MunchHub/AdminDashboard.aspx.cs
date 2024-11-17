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
    public partial class WebForm11 : System.Web.UI.Page
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
                DataTable orders = getRecentOrders(con);
                recentOrderView.DataSource = evaluateTotalQuantityPerOrder(con, orders);
                recentOrderView.DataBind();
            }
        }
        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        private DataTable evaluateTotalQuantityPerOrder(SqlConnection con, DataTable table)
        {
            table.Columns.Add("quantity", typeof(Int32));
            string evaluateTotalQuantityPerOrderQuery = "SELECT quantity FROM [DeliveryTable] WHERE order_id = @OrderID";
            using (SqlCommand cmd = new SqlCommand(evaluateTotalQuantityPerOrderQuery, con))
            {
                foreach (DataRow row in table.Rows)
                {
                    int totalQuantity = 0;
                    con.Open();
                    DataTable quantityTable = new DataTable();
                    cmd.Parameters.AddWithValue("@OrderID", row["order_id"].ToString());
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(quantityTable);
                    con.Close();
                    foreach (DataRow innerRow in quantityTable.Rows)
                    {
                        totalQuantity = totalQuantity + Convert.ToInt32(innerRow["quantity"]);
                    }
                    row["quantity"] = totalQuantity;
                    cmd.Parameters.Clear();
                }
            }
            return table;
        }

        private DataTable getRecentOrders(SqlConnection con)
        {
            DataTable orders = new DataTable();
            string recentOrdersQuery = "SELECT TOP 4 order_id, datetime_ordered FROM [OrderTable] ORDER BY OrderTable.datetime_ordered DESC";
            using (SqlCommand cmd = new SqlCommand(recentOrdersQuery, con))
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(orders);
                con.Close();
            }
            return orders;
        }

        protected void recentOrderView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("manageOrder"))
            {
                Response.Redirect("AdminManageOrders.aspx?data=" + e.CommandArgument.ToString());
            }
        }
    }
}