using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Image
{
    public interface ImageService
    {
        public Tuple<int, string> SaveImage(IFormFile imagefile);

        public bool DeleteImage(string imagefilename);
    }
}
