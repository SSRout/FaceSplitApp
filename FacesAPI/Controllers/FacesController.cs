using Microsoft.AspNetCore.Mvc;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FacesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacesController : ControllerBase
    {
        [HttpPost]
        public async Task<List<byte[]>>  ReadFaces()
        {
           using(var ms=new MemoryStream(2048)){
                await Request.Body.CopyToAsync(ms);
                var faces = GetFaces(ms.ToArray());
                return faces;
          }
        }

        private List<byte[]> GetFaces(byte[] image)
        {
            Mat src = Cv2.ImDecode(image, ImreadModes.Color);
            // Convert the byte array into jpeg image and Save the image coming from the source
            //in the root directory for testing purposes. 
            src.SaveImage(@"Images\image.jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
            var file = Path.Combine(Directory.GetCurrentDirectory(), "CascadeFile", "haarcascade_frontalface_default.xml");
            var faceCascade = new CascadeClassifier();
            faceCascade.Load(file);
            var faces = faceCascade.DetectMultiScale(src, 1.1, 6, HaarDetectionType.DoRoughSearch, new Size(60, 60));
            var faceList = new List<byte[]>();
            int j = 0;
            foreach (var rect in faces)
            {
                var faceImage = new Mat(src, rect);
                faceList.Add(faceImage.ToBytes(".jpg"));
                faceImage.SaveImage(@"Images\face" + j + ".jpg", new ImageEncodingParam(ImwriteFlags.JpegProgressive, 255));
                j++;
            }
            return faceList;

        }
    }
}
