using FacesWebMVC.RestClients;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacesWebMVC.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly IOrderManagementApi _orderManagementApi;

        public OrderManagementController(IOrderManagementApi orderManagementApi)
        {
            _orderManagementApi = orderManagementApi;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderManagementApi.GetOrders();
            foreach(var order in orders)
            {
                order.ImageString = ConvertAndFormatToString(order.ImageData);
            }
            return View(orders);
        }
        
        [Route("/Details/{OrderId}")]
        public async Task<IActionResult> Details(string OrderId)
        {
            var order = await _orderManagementApi.GetOrderById(Guid.Parse(OrderId));
            order.ImageString = ConvertAndFormatToString(order.ImageData);

            foreach (var detail in order.OrderDetails)
            {
                detail.ImageString = ConvertAndFormatToString(detail.FaceData);
            }

            return View(order);
        }

        private string ConvertAndFormatToString(byte[] imageData)
        {
            string imageBase64Data = Convert.ToBase64String(imageData);
            return string.Format("data:/image/png;base64,{0}", imageBase64Data);
        }
    }
}
