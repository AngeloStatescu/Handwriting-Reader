using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandwritingReader
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        static bool ValidatePassword(string password)
        {
            const int minLength = 8;

            if (password == null) return false;

            bool hasLength = password.Length >= minLength;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            bool hasDecimalDigit = false;

            if (hasLength)
            {
                foreach (char c in password)
                {
                    if (char.IsUpper(c)) hasUpperCaseLetter = true;
                    else if (char.IsLower(c)) hasLowerCaseLetter = true;
                    else if (char.IsDigit(c)) hasDecimalDigit = true;
                }
            }

            bool isValid = hasLength && hasUpperCaseLetter && hasLowerCaseLetter && hasDecimalDigit;

            return isValid;
        }

        static bool ValidateEmail(string email)
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            Regex r = new Regex(validEmailPattern, RegexOptions.IgnoreCase);

            bool isValid = r.IsMatch(email);

            return isValid;
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {

            Username.Style.Remove("border-color");
            Username.Style.Remove("border-width");

            Email.Style.Remove("border-color");
            Email.Style.Remove("border-width");

            Password.Style.Remove("border-color");
            Password.Style.Remove("border-width");

            bool validPassword;
            bool validUsername;
            bool validEmail;

            String passwordError = String.Empty;
            String usernameError = String.Empty;
            String emailError = String.Empty;

            String errorMessage = String.Empty;

            string insertQuery = "INSERT INTO Account(userName,password,email,permissionId) OUTPUT INSERTED.id "
                   + " VALUES (@username, @password, @email, @permissionId)";
            
            string verifyQuery = "SELECT id FROM Account where userName = '" + Username.Value + "'";
            string verifyQuery2 = "SELECT id FROM Account where email = '" + Email.Value + "'";

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\Documents\Visual Studio 2017\Projects\HandwritingReader\HandwritingReader\App_Data\HandwritingReader.mdf;Integrated Security=True");

            if (ValidatePassword(Password.Value))
                validPassword = true;
            else
            {
                validPassword = false;
                errorMessage += "<p style='color: red;'>The password must be of at least 8 characters and must contain lowercase letters, uppercase letters and numbers!</p>";
            }


            if (Username.Value.Length > 5)
                validUsername = true;
            else
            {
                validUsername = false;
                errorMessage += "<p style='color: red;'>The username must be of at least 6 characters!</p>";
            }


            if (ValidateEmail(Email.Value))
                validEmail = true;
            else
            {
                validEmail = false;
                errorMessage += "<p style='color: red;'>The email is not valid!</p>";
            }


            if (!validUsername || !validPassword || !validEmail)
            {
                RegisterError.InnerHtml = errorMessage;
                if (!validUsername)
                {
                    Username.Style.Add("border-color", "red");
                    Username.Style.Add("border-width", "2px");
                }

                if (!validEmail)
                {
                    Email.Style.Add("border-color", "red");
                    Email.Style.Add("border-width", "2px");
                }

                if (!validPassword)
                {
                    Password.Style.Add("border-color", "red");
                    Password.Style.Add("border-width", "2px");
                }
            }
            else
            {

                try
                {
                    connection.Open();

                    SqlCommand verifyCommand = new SqlCommand(verifyQuery, connection);
                    var verifyUsername = verifyCommand.ExecuteScalar();

                    SqlCommand verifyCommand2 = new SqlCommand(verifyQuery2, connection);
                    var verifyEmail = verifyCommand2.ExecuteScalar();

                    errorMessage = String.Empty;
                    bool existentUsername;
                    bool existentEmail;

                    if (verifyUsername != null)
                    {
                        existentUsername = true;
                        errorMessage += "<p style='color: red;'>That username is already registered!</p>";
                    }
                    else
                    {
                        existentUsername = false;
                    }

                    if (verifyEmail != null)
                    {
                        existentEmail = true;
                        errorMessage += "<p style='color: red;'>That email is already registered!</p>";
                    }
                    else
                    {
                        existentEmail = false;
                    }

                    if (existentUsername || existentEmail)
                    {
                        RegisterError.InnerHtml = errorMessage;
                        if (existentUsername)
                        {
                            Username.Style.Add("border-color", "red");
                            Username.Style.Add("border-width", "2px");
                        }

                        if (existentEmail)
                        {
                            Email.Style.Add("border-color", "red");
                            Email.Style.Add("border-width", "2px");
                        }
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@username", Username.Value);
                        command.Parameters.AddWithValue("@password", Password.Value);
                        command.Parameters.AddWithValue("@email", Email.Value);
                        command.Parameters.AddWithValue("@permissionId", 3);
                        var id = command.ExecuteScalar();
                        Response.Redirect("~/Login.aspx");
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
}