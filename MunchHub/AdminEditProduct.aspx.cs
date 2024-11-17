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
    public partial class WebForm20 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            } else if (Request.QueryString["data"] == null)
            {
                Response.Redirect("AdminManageProduct.aspx");
            }
            if (!IsPostBack)
            {
                string conStr = ConfigurationManager.ConnectionStrings["MunchHubDatabase"].ConnectionString;
                SqlConnection con = new SqlConnection(conStr);
                getAllCategories(con);
                setProductDetails(con);
            }
        }
        protected void logoutButton_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("GuestLandingPage.aspx");
        }

        private void setProductDetails(SqlConnection con)
        {
            int productID = Convert.ToInt32(Request.QueryString["data"]);
            string specificCategoryQuery = "SELECT ProductTable.product_name, ProductTable.description, ProductTable.short_description, ProductTable.price, CategoryTable.category_id FROM [ProductTable] INNER JOIN [CategoryTable] ON ProductTable.category_id = CategoryTable.category_id WHERE ProductTable.product_id = @ProductID";
            using (SqlCommand cmd = new SqlCommand(specificCategoryQuery, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    productNameInput.Text = rdr.GetString(0);
                    priceInput.Text = rdr.GetDouble(3).ToString();
                    descriptionInput.Text = rdr.GetString(1);
                    shortDescriptionInput.Text = rdr.GetString(2);
                    categoryDropdownList.SelectedValue = rdr.GetInt32(4).ToString();
                }
                con.Close();
            }
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
            string nameError = ValidationUtilities.ValidateProductName(productNameInput.Text.Trim()); ;
            string priceError = validatePriceParse();
            string categoryError = validateCategory();
            string descriptionError = ValidationUtilities.ValidateDescription(descriptionInput.Text.Trim());
            string shortDescriptionError = ValidationUtilities.ValidateShortDescription(shortDescriptionInput.Text.Trim());
            nameErrorLabel.Text = nameError;
            priceErrorLabel.Text = priceError;
            categoryErrorLabel.Text = categoryError;
            descriptionErrorLabel.Text = descriptionError;
            shortDescriptionErrorLabel.Text = shortDescriptionError;
            if (nameError == "" && priceError == "" && categoryError == "" && descriptionError == "" && shortDescriptionError == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void saveProduct(SqlConnection con)
        {
            if (fileUpload.HasFile)
            {
                string registerCustomerQuery = "UPDATE [ProductTable] SET description = @Description, img_url = @ImgUrl, price = @Price, category_id = @CategoryID, short_description = @ShortDesc, product_name = @ProductName WHERE product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(registerCustomerQuery, con))
                {
                    string filename = fileUpload.FileName;

                    string filePath = Server.MapPath("/Images/Product/") + filename;
                    fileUpload.SaveAs(filePath);

                    cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(Request.QueryString["data"]));
                    cmd.Parameters.AddWithValue("@Description", descriptionInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@ImgUrl", filePath);
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDouble(priceInput.Text.Trim()));
                    cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(categoryDropdownList.SelectedValue));
                    cmd.Parameters.AddWithValue("@ShortDesc", shortDescriptionInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductName", productNameInput.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                };
            } else
            {
                string registerCustomerQuery = "UPDATE [ProductTable] SET description = @Description, price = @Price, category_id = @CategoryID, short_description = @ShortDesc, product_name = @ProductName WHERE product_id = @ProductID";
                using (SqlCommand cmd = new SqlCommand(registerCustomerQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(Request.QueryString["data"]));
                    cmd.Parameters.AddWithValue("@Description", descriptionInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@Price", Convert.ToDouble(priceInput.Text.Trim()));
                    cmd.Parameters.AddWithValue("@CategoryID", Convert.ToInt32(categoryDropdownList.SelectedValue));
                    cmd.Parameters.AddWithValue("@ShortDesc", shortDescriptionInput.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductName", productNameInput.Text.Trim());

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                };
            }
            Response.Redirect("AdminManageProducts.aspx");
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
            }
            catch (Exception ex)
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