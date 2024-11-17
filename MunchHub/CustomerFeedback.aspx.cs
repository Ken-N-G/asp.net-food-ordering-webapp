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
    public partial class WebForm9 : System.Web.UI.Page
    {

        protected int rating
        {
            get
            {
                if (ViewState["Quantity"] != null)
                    return (int)ViewState["Quantity"];
                else
                    return 0;
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
                Response.Redirect("CustomerOrder.aspx");
            }
            if (!IsPostBack && Request.QueryString["data"] != null)
            {
                ratingCountLabel.Text = rating.ToString();
            }
            orderLabel.Text = "Order - " + Request.QueryString["data"].ToString();
        }

        protected void removeRatingButton_Click(object sender, EventArgs e)
        {
            if (rating > 0)
            {
                rating--;
            }
            setRatingStars();
        }

        protected void addRatingButton_Click(object sender, EventArgs e)
        {
            if (rating < 5)
            {
                rating++;
            }
            setRatingStars();
        }

        private void setRatingStars()
        {
            ratingRow.Controls.Clear();
            for (int i = 0;  i < rating; i++)
            {
                LiteralControl element = new LiteralControl("<div class=\"col-2 d-flex justify-content-center\"><i class=\"bi bi-star-fill\" style=\"font-size:36px;color:#ffae30\"></i></div>");
                ratingRow.Controls.Add(element);
            }
            ratingCountLabel.Text = rating.ToString();
        }

        protected void sendFeedbackButton_Click(object sender, EventArgs e)
        {
            string comment = commentInput.Text.Trim();
            string feedbackErrorText = ValidationUtilities.ValidateFeedback(comment);
            feedbackErrorLabel.Text = feedbackErrorText;
            if (feedbackErrorText == "")
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                string createFeedbackQuery = "INSERT INTO [FeedbackTable] (feedback_id, rating, feedback, order_id) VALUES (@FeedbackID, @Rating, @Feedback, @OrderID)";
                string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
                timestamp = "8" + timestamp;
                using (SqlCommand cmd = new SqlCommand(createFeedbackQuery, con))
                {
                    int feedbackID = Convert.ToInt32(timestamp);
                    DateTime dateOrdered = DateTime.Now;
                    cmd.Parameters.AddWithValue("@FeedbackID", feedbackID);
                    cmd.Parameters.AddWithValue("@Rating", rating);
                    cmd.Parameters.AddWithValue("@Feedback", comment);
                    cmd.Parameters.AddWithValue("@OrderID", Request.QueryString["data"]);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("CustomerOrders.aspx");
                }
            }
        }
    }
}