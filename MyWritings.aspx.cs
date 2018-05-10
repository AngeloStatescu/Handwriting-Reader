using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandwritingReader
{
    public partial class MyWritings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\Documents\Visual Studio 2017\Projects\HandwritingReader\HandwritingReader\App_Data\HandwritingReader.mdf;Integrated Security=True");

            string getWritings = "SELECT id, title, result FROM Handwriting WHERE createdBy = " + Session["myAccountId"].ToString();

            try
            {
                connection.Open();
                SqlCommand command2 = new SqlCommand(getWritings, connection);
                using (SqlDataReader myReader = command2.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        string result = myReader["result"].ToString();

                        string title = myReader["title"].ToString();

                        string id = myReader["id"].ToString();

                        string Template =

                           "<div class='col-md-12 col-sm-12 col-xs-12 col-lg-12'> " +
                           "<div class='panel panel-default'>" +
                              "<div class='panel-body'> " +
                                   "<button type = 'button' onclick=' downloadResult(this) ' class='btn btn-default' style='font-size: 18px; float: right;'>Download Result</button> " +
                                   "<p style='display:none;'>" + result + "</p>" +
                                   "<h3 style = 'margin-top: 8px !important;'>" + title + "</h3> " +
                                 "</div> " +
                              "</div> " +
                          "</div> ";

                        writingsContainer.InnerHtml += Template;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}