using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandwritingReader
{
    public partial class AddImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                Session["Image"] = null;
            }
        }

        public static string filename = string.Empty;
        public static byte[] uploadedBytes;

        protected void UploadButton_Click(object sender, EventArgs e)
        {

            if (FileUploadControl.HasFile)
            {
                try
                {
                    filename = Path.GetFileName(FileUploadControl.FileName);
                    Session["Image"] = FileUploadControl.PostedFile;
                    Stream fs = FileUploadControl.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    uploadedBytes = bytes;
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    Image1.ImageUrl = "data:image/png;base64," + base64String;

                    string filePath = "d:/Desktop/TestPython/test3.png";
                    string x = Image1.ImageUrl.Replace("data:image/png;base64,", "");
                    byte[] imageBytes = Convert.FromBase64String(x);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            try {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = @"C:/Users/Dany/AppData/Local/Programs/Python/Python36-32/python.exe";
                p.StartInfo.Arguments = @"D:/Desktop/TestPython/test.py";
                p.StartInfo.WorkingDirectory = @"D:/Desktop/TestPython";
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {

            }

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\Documents\Visual Studio 2017\Projects\HandwritingReader\HandwritingReader\App_Data\HandwritingReader.mdf;Integrated Security=True");

            try
            {
                string insertQuery = "INSERT INTO Handwriting(title,createdDate,image,createdBy,result) OUTPUT INSERTED.Id "
                           + " VALUES (@title, @createdDate, @image, @createdBy, @result)";


                using (StreamReader sr = new StreamReader("d:/Desktop/TestPython/result.txt"))
                {
                    result = sr.ReadToEnd();
                }

                byte[] bytes = uploadedBytes;

                connection.Open();

                DateTime localDate = DateTime.Now;

                string sGenName = Path.GetFileName(FileUploadControl.FileName);

                SqlCommand command1 = new SqlCommand(insertQuery, connection);
                command1.Parameters.AddWithValue("title", filename.ToString());
                command1.Parameters.AddWithValue("createdDate", localDate);
                command1.Parameters.AddWithValue("image", bytes);
                command1.Parameters.AddWithValue("createdBy", int.Parse(Session["myAccountId"].ToString()));
                command1.Parameters.AddWithValue("result", result);
                command1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }


            try
            {
                DateTime localDate = DateTime.Now;

                string sGenName = "Result " + localDate.ToString() + ".txt";

                System.IO.FileStream fs = null;
                fs = System.IO.File.Open("d:/Desktop/TestPython/result.txt", System.IO.FileMode.Open);

                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" +
                                   sGenName);
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}