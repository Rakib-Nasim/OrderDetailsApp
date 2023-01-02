using OrderDetailsAPI.Models;
using OrderDetailsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetailsAPI.Logic
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<IEnumerable<ItemViewModel>> GetAllItem();
        Task<IEnumerable<Order>> GetAllOrder();
        Task<int> Create(OrderViewModel model);
        Task<int> Delete(int id);
        Task<int> Update(Order model);
    }
}
