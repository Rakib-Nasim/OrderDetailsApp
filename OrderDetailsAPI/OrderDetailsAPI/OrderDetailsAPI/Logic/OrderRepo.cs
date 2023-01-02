using Microsoft.EntityFrameworkCore;
using OrderDetailsAPI.Data;
using OrderDetailsAPI.Models;
using OrderDetailsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetailsAPI.Logic
{
    public class OrderRepo : IOrderRepo
    {
        private readonly AppDbContext _dbContext;
        public OrderRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(OrderViewModel model)
        {
            Order newOrder = new Order();
            newOrder.CustomerId = model.CustomerId;
            newOrder.OrderAdderss = model.OrderAdderss;
            newOrder.OrderDate = model.OrderDate;
            newOrder.OrderNumber = model.OrderNumber;
            newOrder.TotalPrice = model.TotalPrice;
            newOrder.OrderId = model.OrderId;
             _dbContext.Orders.Add(newOrder);
            var number = await _dbContext.SaveChangesAsync();

            var lastColumn = _dbContext.Orders.OrderBy(x => x.OrderId).LastOrDefault();
            foreach (var item in model.Order_Items)
            {
                Order_Item orItem = new Order_Item();
                orItem.OrderId = lastColumn.OrderId;
                orItem.ItemId = item;
                _dbContext.Order_Items.Add(orItem);
                await _dbContext.SaveChangesAsync();
            }
            if (number > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public async Task<IEnumerable<ItemViewModel>> GetAllItem()
        {
            var items=await _dbContext.Items.ToListAsync();
            List<ItemViewModel> newItemlist = new List<ItemViewModel>();

            foreach (var item in items)
            {
                ItemViewModel newItem = new ItemViewModel();
                newItem.ItemId = item.ItemId;
                newItem.ItemName = item.ItemName;
                newItem.ItemPrice = item.ItemPrice;
                newItem.totalPrice = 0;
                newItem.quentity = 0;
                newItemlist.Add(newItem);
            }
            return newItemlist;
        }

        public async Task<IEnumerable<Order>> GetAllOrder()
        {
           return await _dbContext.Orders.ToListAsync();
        }

        public Task<int> Update(Order model)
        {
            throw new NotImplementedException();
        }
    }
}
