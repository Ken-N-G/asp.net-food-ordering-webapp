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
    public partial class WebForm5 : System.Web.UI.Page
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
                setCartItems(con);
            }
        }

        private void setCartItems(SqlConnection con)
        {
            double totalPrice = 0.0;
            DataTable table = getCartItems(con, Convert.ToInt32(Session["user"]));
            foreach (DataRow row in table.Rows)
            {
                double price = Convert.ToDouble(row["price"]);
                double quantity = Convert.ToInt32(row["quantity"]);
                double newPrice = price * quantity;
                totalPrice = totalPrice + newPrice;
                row["price"] = newPrice;
            }
            cartListView.DataSource = table;
            cartListView.DataBind();
            totalPriceLabel.Text = "Total: RM " + totalPrice.ToString();
        }

        private DataTable getCartItems(SqlConnection con, int userID)
        {
            DataTable table = new DataTable();
            string getCartItemsQuery = "SELECT CartTable.item_id, CartTable.quantity, ProductTable.product_name, ProductTable.img_url, ProductTable.price, ProductTable.product_id FROM [CartTable] INNER JOIN [ProductTable] ON CartTable.product_id = ProductTable.product_id WHERE CartTable.user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(getCartItemsQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(table);
                con.Close();
            }
            return table;
        }

        protected void cartListView_itemCommand(object sender, ListViewCommandEventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            switch (e.CommandName)
            {
                case "addQuantity":
                    addItemQuantity(con, Convert.ToInt32(e.CommandArgument));
                    break;
                case "removeQuantity":
                    removeItemQuantity(con, Convert.ToInt32(e.CommandArgument));
                    break;
            }
            setCartItems(con);
        }

        private void addItemQuantity(SqlConnection con, int itemID)
        {
            int itemQuantity = getItemQuantity(con, itemID);
            itemQuantity++;
            updateItemQuantity(con, itemID, itemQuantity);
        }

        private void removeItemQuantity(SqlConnection con, int itemID)
        {
            int itemQuantity = getItemQuantity(con, itemID);
            itemQuantity--;
            if (itemQuantity >= 1)
            {
                updateItemQuantity(con, itemID, itemQuantity);
            } else
            {
                removeCartItem(con, itemID);
            }
        }

        private void removeCartItem(SqlConnection con, int itemID)
        {
            string deleteItemQuery = "DELETE FROM [CartTable] WHERE item_id = @ItemID";
            using (SqlCommand cmd = new SqlCommand(deleteItemQuery, con))
            {
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private int getItemQuantity(SqlConnection con, int itemID)
        {
            string itemQuantityQuery = "SELECT quantity FROM [CartTable] WHERE item_id = @ItemID";
            int itemQuantity = 0;
            using (SqlCommand cmd = new SqlCommand(itemQuantityQuery, con))
            {
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                con.Open();
                SqlDataReader rdr1 = cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    itemQuantity = rdr1.GetInt32(0);
                }
                con.Close();
            }
            return itemQuantity;
        }

        private void updateItemQuantity(SqlConnection con, int itemID, int quantity)
        {
            string itemUpdateQuery = "UPDATE [CartTable] SET quantity = @Quantity WHERE item_id = @ItemID";
            using (SqlCommand cmd = new SqlCommand(itemUpdateQuery, con))
            {
                cmd.Parameters.AddWithValue("@ItemID", itemID);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void paymentButton_Click(object sender, EventArgs e)
        {
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            string orderID = createOrder(con, Convert.ToInt32(Session["user"]));
            addDeliveryItems(con, Convert.ToInt32(orderID), Convert.ToInt32(Session["user"]));
            Response.Redirect("CustomerPaymentConfirmation.aspx?data=" + orderID);
        }

        private string createOrder(SqlConnection con, int userID)
        {
            string createOrderQuery = "INSERT INTO [OrderTable] (order_id, datetime_ordered, status, user_id) VALUES (@OrderID, @DateTime, @Status, @UserID)";
            string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
            timestamp = "6" + timestamp;
            using (SqlCommand cmd = new SqlCommand(createOrderQuery, con))
            {
                int orderID = Convert.ToInt32(timestamp);
                DateTime dateOrdered = DateTime.Now;
                string status = "Pending";
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                cmd.Parameters.AddWithValue("@DateTime", dateOrdered);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@UserID", userID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return timestamp;
        }

        private void addDeliveryItems(SqlConnection con, int orderID, int userID)
        {
            DataTable table = getCartItems(con, userID);
            string createDeliveryItemQuery = "INSERT INTO [DeliveryTable] (delivery_id, quantity, product_id, order_id) VALUES (@DeliveryID, @Quantity, @ProductID, @OrderID)";
            using (SqlCommand cmd = new SqlCommand(createDeliveryItemQuery, con))
            {
                con.Open();
                long timestamp = new DateTimeOffset(DateTime.Now).Ticks;
                foreach (DataRow row in table.Rows)
                {
                    {
                        string id = "7" + timestamp.ToString().Remove(0, 12);
                        int deliveryItemID = Convert.ToInt32(id);
                        cmd.Parameters.AddWithValue("@DeliveryID", deliveryItemID);
                        cmd.Parameters.AddWithValue("@Quantity", row["quantity"]);
                        cmd.Parameters.AddWithValue("@ProductID", row["product_id"]);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        timestamp++;
                    }
                }
                con.Close();
            }
            string deleteCartItemsQuery = "DELETE FROM [CartTable] WHERE user_id = @UserID";
            using (SqlCommand cmd = new SqlCommand(deleteCartItemsQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserID", userID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}