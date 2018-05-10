using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HandwritingReader
{
    public partial class Doodle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string SaveCanvasImage(string imgBytes, string username)
        {
            string result = string.Empty;

            try
            {
                string filePath = "d:/Desktop/TestPython/test3.png";
                string x = imgBytes.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(x);
                MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {

            }

            try
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = @"C:/Users/Dany/AppData/Local/Programs/Python/Python36-32/python.exe";
                p.StartInfo.Arguments = @"D:/Desktop/TestPython/test.py";
                p.StartInfo.WorkingDirectory = @"D:/Desktop/TestPython";
                p.Start();
                p.WaitForExit();

                try
                {   
                    using (StreamReader sr = new StreamReader("d:/Desktop/TestPython/result.txt"))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
            catch (Exception ex)
            {

            }

            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=d:\Documents\Visual Studio 2017\Projects\HandwritingReader\HandwritingReader\App_Data\HandwritingReader.mdf;Integrated Security=True");

            try
            {
                string insertQuery = "INSERT INTO Handwriting(title,createdDate,image,createdBy,result) OUTPUT INSERTED.Id "
                           + " VALUES (@title, @createdDate, @image, @createdBy, @result)";

                string x = imgBytes.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(x);

                connection.Open();

                DateTime localDate = DateTime.Now;

                string sGenName = "Result " + localDate.ToString() + ".txt";

                SqlCommand command1 = new SqlCommand(insertQuery, connection);
                command1.Parameters.AddWithValue("title", sGenName);
                command1.Parameters.AddWithValue("createdDate", localDate);
                command1.Parameters.AddWithValue("image", bytes);
                command1.Parameters.AddWithValue("createdBy", int.Parse(username));
                command1.Parameters.AddWithValue("result", result);
                command1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sGenName = "Result.txt";

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
    }
}