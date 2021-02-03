using FacesWebMVC.Models;
using FacesWebMVC.ViewModels;
using MassTransit;
using MessagingQueue;
using MessagingQueue.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FacesWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBusControl _busControl;

        public HomeController(ILogger<HomeController> logger,IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        [HttpGet]
        public IActionResult RegisterOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterOrder(OrderViewModel model)
        {
            MemoryStream memory = new MemoryStream();
            using (var uploadedFile = model.File.OpenReadStream())
            {
                await uploadedFile.CopyToAsync(memory);

            }

            model.ImageData = memory.ToArray();
            model.PictureUrl = model.File.FileName;
            model.OrderId = Guid.NewGuid();
            var sendToUri = new Uri($"{RabbitMqMassTransitConstant.RabbitMqUri }" +

                $"{RabbitMqMassTransitConstant.RegisterOrderCommandQueue}");

            var endPoint = await _busControl.GetSendEndpoint(sendToUri);
            await endPoint.Send<IRegisterOrderCommand>(
               new
               {
                   model.OrderId,
                   model.UserEmail,
                   model.ImageData,
                   model.PictureUrl
               });
            ViewData["OrderId"] = model.OrderId;
            return View("Thanks");
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
