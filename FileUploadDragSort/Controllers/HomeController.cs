using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUploadDragSort.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        [HttpPost]
        public ActionResult Formpost(List<imageModel> model)
        {
            //var temp = Request.Form["aa"];

            var one = 1;
            var two = "122";


            model.ForEach(delegate (imageModel aaclass) {
                one = aaclass.sortID;
                two = aaclass.name;
            });


            return RedirectToAction("Index", "Home", new { data = "" });
        }

        [HttpPost]
        public JsonResult UploadImage()
        {
            int status = 500;
            string strMessage = "fail";

            string ImageFolder = "C:\\Users\\raylin\\Documents\\Visual Studio 2015\\Projects\\FileUploadDragSort\\FileUploadDragSort\\Content\\ImageFolder\\";
            Guid ImageId = Guid.NewGuid();
            string fileName = string.Empty;
            List<string> imgUrl = new List<string>();
            List<string> imgName = new List<string>();


            foreach (string upload in Request.Files)
            {
                if (!(Request.Files[upload] != null && Request.Files[upload].ContentLength > 0)) continue;

                HttpPostedFileBase file = Request.Files[upload];

                if (ModelState.IsValid)
                {
                    ImageId = Guid.NewGuid();
                    fileName = string.Format("{0}.jpg", ImageId.ToString("N"));

                    if (file == null)
                    {
                        strMessage = "Please Upload Your file";
                    }
                    else if (file.ContentLength > 0)
                    {
                        string[] AllowedFileExtensions = { ".jpg", ".png" };

                        if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower()))
                        {
                            strMessage = string.Format("Please file of type:  {0} ", string.Join(", ", AllowedFileExtensions));
                        }
                        else
                        {
                            if (!Directory.Exists(ImageFolder))
                            {
                                Directory.CreateDirectory(ImageFolder);
                            }

                            file.SaveAs(ImageFolder + fileName);

                            //System.IO.File.Copy(file.FileName, ImageFolder + fileName, true);


                            //byte[] data = new Byte[file.ContentLength];
                            //file.InputStream.Read(data, 0, file.ContentLength);
                            //Image photo = data.ConvertByteArrayToImage();

                            //photo.Save(ImageFolder + fileName, System.Drawing.Imaging.ImageFormat.Png);
                            //photo.Dispose();

                            imgName.Add(file.FileName);
                            imgUrl.Add("/Content/ImageFolder/" + fileName);
                            strMessage = "File uploaded successfully";
                            status = 200;
                        }
                    }
                }

            }


            return Json(new
            {
                status = status,
                message = strMessage,
                img = imgUrl,
                imgName = imgName
            });
        }

    }

    public class imageModel
    {
        public int sortID { get; set; }
        public string name { get; set; }
    }
}