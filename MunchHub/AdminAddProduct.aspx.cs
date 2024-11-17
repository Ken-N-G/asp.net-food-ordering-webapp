using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MunchHub
{
    public partial class WebForm21 : System.Web.UI.Page
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
                getAllCategories(con);
            }
        }
        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        protected void saveChanges_Click(object sender, EventArgs e)
        {
            if (validateProductDetails())
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                saveProduct(con);
            }
        }

        private bool validateProductDetails()
        {
            string imageError = validateFileUpload();
            string nameError = validateProductName();
            string priceError = validatePriceParse();
            string categoryError = validateCategory();
            string descriptionError = ValidationUtilities.ValidateDescription(descriptionInput.Text.Trim());
            string shortDescriptionError = ValidationUtilities.ValidateShortDescription(shortDescriptionInput.Text.Trim());
            imageErrorLabel.Text = imageError;
            nameErrorLabel.Text = nameError;
            priceErrorLabel.Text = priceError;
            categoryErrorLabel.Text = categoryError;
            descriptionErrorLabel.Text = descriptionError;
            shortDescriptionErrorLabel.Text = shortDescriptionError;
            if (imageError == "" && nameError == "" && priceError == "" && categoryError == "" && descriptionError == "" && shortDescriptionError == "")
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void saveProduct(SqlConnection con)
        {
            string registerCustomerQuery = "INSERT INTO [ProductTable] (product_id, description, img_url, price, category_id, short_description, product_name) VALUES (@ProductID, @Description, @ImgUrl, @Price, @CategoryID, @ShortDesc, @ProductName)";
            using (SqlCommand cmd = new SqlCommand(registerCustomerQuery, con))
            {
                string timestamp = new DateTimeOffset(DateTime.Now).Ticks.ToString().Remove(0, 12);
                timestamp = "4" + timestamp;
                int productID = Convert.ToInt32(timestamp);

                string filename = fileUpload.FileName;
                string filePath = Server.MapPath("/Images/Product/") + filename;
                fileUpload.SaveAs(filePath);

                cmd.Parameters.AddWithValue("@ProductID", productID);
                cmd.Parameters.AddWithValue("@Description", descriptionInput.Text.Trim());
                cmd.Parameters.AddWithValue("@ImgUrl", "/Images/Product/" + filename);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDouble(priceInput.Text.Trim()));
                cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(categoryDropdownList.SelectedValue));
                cmd.Parameters.AddWithValue("@ShortDesc", shortDescriptionInput.Text.Trim());
                cmd.Parameters.AddWithValue("@ProductName", productNameInput.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            };
            Response.Redirect("AdminManageProducts.aspx");
        }

        private string validateProductName()
        {
            int amount = 0;
            string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            string allCategoriesQuery = "SELECT COUNT(*) FROM [ProductTable] WHERE product_name = @ProductName";
            using (SqlCommand cmd = new SqlCommand(allCategoriesQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@ProductName", productNameInput.Text.Trim());
                amount = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            if (amount > 0)
            {
                return "A product already exists with this name.";
            } else
            {
                return ValidationUtilities.ValidateProductName(productNameInput.Text.Trim());
            }
        }

        private string validateFileUpload()
        {
            if (!fileUpload.HasFile)
            {
                return "You need to upload an image file of the product.";
            }
            return "";
        }

        private string validateCategory()
        {
            if (categoryDropdownList.SelectedItem.Text == "select category")
            {
                return "You need to select a category for the product.";
            }
            return "";
        }

        private string validatePriceParse()
        {
            try
            {
                double price = Convert.ToDouble(priceInput.Text.Trim());
                return ValidationUtilities.ValidatePrice(price);
            } catch (Exception ex)
            {
                return "Enter a number in the form of 14.99.";
            }
        }

        private void getAllCategories(SqlConnection con)
        {
            string allCategoriesQuery = "SELECT * FROM [CategoryTable]";
            using (SqlCommand cmd = new SqlCommand(allCategoriesQuery, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ListItem defaultItem = new ListItem("select category", "default");
                categoryDropdownList.DataSource = rdr;
                categoryDropdownList.DataTextField = "category_name";
                categoryDropdownList.DataValueField = "category_id";
                categoryDropdownList.DataBind();
                categoryDropdownList.Items.Insert(0, defaultItem);
                con.Close();
            }
        }
    }
}