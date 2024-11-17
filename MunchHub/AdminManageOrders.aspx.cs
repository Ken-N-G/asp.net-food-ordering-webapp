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
    public partial class WebForm15 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            } else if (Request.QueryString["data"] != null)
            {
                searchInput.Text = Request.QueryString["data"];
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                setSpecificOrders(con);
            }
            if (!IsPostBack)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                orderView.DataSource = getAllOrders(con);
                orderView.DataBind();
            }
        }

        private DataTable getAllOrders(SqlConnection con)
        {
            DataTable orders = new DataTable();
            string allCategoriesQuery = "SELECT * FROM [OrderTable]";
            using (SqlCommand cmd = new SqlCommand(allCategoriesQuery, con))
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(orders);
                con.Close();
            }
            return orders;
        }

        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        private void setSpecificOrders(SqlConnection con)
        {
            DataTable table = new DataTable();
            string specificOrderQuery = "SELECT * FROM [OrderTable] WHERE order_id LIKE '%' + @SearchQuery + '%'";
            using (SqlCommand cmd = new SqlCommand(specificOrderQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@SearchQuery", searchInput.Text.Trim());
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            orderView.DataSource = table;
            orderView.DataBind();
        }

        protected void orderView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("viewDetails"))
            {
                Response.Redirect("AdminOrderDetails.aspx?data=" + e.CommandArgument.ToString());
            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            setSpecificOrders(con);
        }
    }
}