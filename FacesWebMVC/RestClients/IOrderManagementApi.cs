using FacesWebMVC.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacesWebMVC.RestClients
{
    public interface IOrderManagementApi
    {
        [Get("/orders")]
        Task<List<OrderViewModel>> GetOrders();
        [Get("orders/{id}")]
        Task<OrderViewModel> GetOrderById(Guid OrderId);    
    }
}
