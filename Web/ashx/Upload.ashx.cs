using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.ashx
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpPostedFile file = context.Request.Files["FileData"];
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string fileExt = Path.GetExtension(fileName);
                if (fileExt == ".jpg")
                {
                    string dir = "/ImageUpload/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" +
                                 DateTime.Now.Day + "/";
                    if (!Directory.Exists(context.Request.MapPath(dir)))
                    {
                        Directory.CreateDirectory(context.Request.MapPath(dir));
                    }

                    string newFileName = Guid.NewGuid().ToString();
                    string fullDir = dir + fileName + fileExt;
                    file.SaveAs(context.Request.MapPath(fullDir));
                    context.Response.Write(fullDir);
                    //file.SaveAs(context.Request.MapPath("/ImageUpload/"+fileName));
                }
            }
        }

        private void ProcessCutPhoto(HttpContext context)
        {
            int x = Convert.ToInt32(context.Request["x"]);
            int y = Convert.ToInt32(context.Request["y"]);
            int width = Convert.ToInt32(context.Request["width"]);
            int height = Convert.ToInt32(context.Request["height"]);
            string imgSrc = context.Request["imgSrc"];
            using (Bitmap map = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(map))
                {
                    using (Image img = Image.FromFile(context.Request.MapPath(imgSrc)))
                    {
                        g.DrawImage(img, new Rectangle(0,0,width,height), new Rectangle(x,y,width,height),GraphicsUnit.Pixel);
                        string newFileName = Guid.NewGuid().ToString();
                        string fullDir = "/ImageUpload/" + newFileName + ".jpg";
                        map.Save(context.Request.MapPath(fullDir),System.Drawing.Imaging.ImageFormat.Jpeg);
                        context.Response.Write(fullDir);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}