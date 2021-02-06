using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacesWebMVC.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }
        public byte[] FaceData { get; set; }
        public string ImageString { get; set; }

    }
}
