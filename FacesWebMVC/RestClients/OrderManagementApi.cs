using FacesWebMVC.ViewModels;
using Microsoft.Extensions.Configuration;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FacesWebMVC.RestClients
{
    public class OrderManagementApi : IOrderManagementApi
    {
        private IOrderManagementApi _restClient;
        public OrderManagementApi(IConfiguration config,HttpClient httpClient)
        {
            string apiHostEndpoint = config.GetSection("ApiServiceLocations")
                .GetValue<string>("OrdersApiLocation");
            httpClient.BaseAddress = new Uri($"http://{apiHostEndpoint}/api");
            _restClient = RestService.For<IOrderManagementApi>(httpClient);
        }
        public async Task<OrderViewModel> GetOrderById(Guid OrderId)
        {          
            try
            {
                return await _restClient.GetOrderById(OrderId);
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            return await _restClient.GetOrders();
        }
    }
}
