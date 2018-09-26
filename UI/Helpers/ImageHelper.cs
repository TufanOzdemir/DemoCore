using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace UI.Helpers
{
    public class ImageHelper
    {
        IHostingEnvironment _hostingEnvironment;
        public ImageHelper(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> ImageUploaderAsync(IFormFile pic)
        {
            string result = string.Empty;
            if (pic != null && pic.Length > 0)
            {
                var file = pic;
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "Content\\Product");

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse
                        (file.ContentDisposition).FileName.Trim('"');

                    System.Console.WriteLine(fileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        result = file.FileName;
                    }
                }
            }
            return result;
        }
    }
}
