using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandwritingReader
{
    public partial class HandwritingReader : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["login"] != null)
                if ((bool)Session["login"])
                {
                    RegisterNavButton.Style.Add("display", "none");
                    LoginNavButton.Style.Add("display", "none");
                    LogoutLi.Style.Remove("display");
                    AddImage.Style.Remove("display");
                    Doodle.Style.Remove("display");
                    MyWritings.Style.Remove("display");
                    AccountUser.InnerText = Session["myAccount"].ToString();
                    AccountUserId.InnerText = Session["myAccountId"].ToString();
                    ProfileDiv.Style.Remove("display");
                }
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session["login"] = false;
            Response.Redirect("~/Home.aspx");
        }
    }
}