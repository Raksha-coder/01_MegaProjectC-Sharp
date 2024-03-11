using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Image
{
    public class mageService:ImageService
    {
        private readonly IHostingEnvironment _environment;
        public mageService(IHostingEnvironment env)
        {
            _environment = env;
        }

        public Tuple<int, string> SaveImage(IFormFile imagefile)
        {
            try
            {

                //creating folder with name "Upload"
                var contentPath = this._environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //end



                //check the allowed extension
                var ext = Path.GetExtension(imagefile.FileName);
                var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtension.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtension));
                    return new Tuple<int, string>(0, msg);
                }
                //end




                //giving name to image in Upload Folder => guid.extension 
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithpath = Path.Combine(path, newFileName);
                //end




                /*
                 FileMode.Create: Specifies that the operating system should create a new file. 
                If the file already exists, 
                it will be overwritten. If the file does not exist, it will be created.
                 */
                var stream = new FileStream(fileWithpath, FileMode.Create);
                imagefile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
                //end




            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has Occured" + ex.Message);
            }
        }


        public bool DeleteImage(string imagefilename)
        {
            try
            {
                var wwwPath = this._environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\", imagefilename);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
