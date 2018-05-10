using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandwritingReader
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Username.Style.Remove("border-color");
            Username.Style.Remove("border-width");

            Password.Style.Remove("border-color");
            Password.Style.Remove("border-width");

            string verifyQuery = "SELECT userName FROM Account where userName = '" + Username.Value + "'";
            string verifyQuery2 = "SELECT password FROM Account where userName = '" + Username.Value + "'";
            string verifyQuery3 = "SELECT id FROM Account where userName = '" + Username.Value + "'";
            string verifyQuery4 = "SELECT permissionId FROM Account where userName = '" + Username.Value + "'";
            int UserId = 0;
            int permissionId = 0;

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\Documents\Visual Studio 2017\Projects\HandwritingReader\HandwritingReader\App_Data\HandwritingReader.mdf;Integrated Security=True");

            try
            {
                connection.Open();
                SqlCommand verifyCommand = new SqlCommand(verifyQuery, connection);
                var verifyUsername = verifyCommand.ExecuteScalar();

                SqlCommand verifyCommand2 = new SqlCommand(verifyQuery2, connection);
                var verifyPassword = verifyCommand2.ExecuteScalar();

                SqlCommand verifyCommand3 = new SqlCommand(verifyQuery3, connection);
                SqlDataReader rd = verifyCommand3.ExecuteReader();
                if (rd.HasRows)
                {
                    rd.Read();
                    UserId = rd.GetInt32(0);
                    rd.Close();
                }
                else rd.Close();

                SqlCommand verifyCommand4 = new SqlCommand(verifyQuery4, connection);
                SqlDataReader rd2 = verifyCommand4.ExecuteReader();
                if (rd2.HasRows)
                {
                    rd2.Read();
                    permissionId = rd2.GetInt32(0);
                    rd2.Close();
                }
                else rd2.Close();

                String errorMessage = String.Empty;

                if (verifyUsername != null && verifyUsername.ToString() == Username.Value && verifyPassword != null && verifyPassword.ToString() == Password.Value)
                {
                    Session["myAccount"] = Username.Value;
                    Session["myAccountId"] = UserId;
                    Session["myPermissionId"] = permissionId;
                    Session["login"] = true;

                    Response.Redirect("~/Home.aspx");
                }
                else
                {
                    RegisterError.InnerHtml = "<p style='color: red;'>The username or password is incorrect!</p>";

                    Username.Style.Add("border-color", "red");
                    Username.Style.Add("border-width", "2px");

                    Password.Style.Add("border-color", "red");
                    Password.Style.Add("border-width", "2px");
                }   
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
        }
    }
}