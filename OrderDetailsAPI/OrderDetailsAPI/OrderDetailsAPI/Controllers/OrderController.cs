using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderDetailsAPI.Logic;
using OrderDetailsAPI.Models;
using OrderDetailsAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetailsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;
        public OrderController(IOrderRepo  orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var customers= await _orderRepo.GetAllCustomer();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllItem")]
        public async Task<IActionResult> GetAllItem()
        {
            try
            {
                var Items = await _orderRepo.GetAllItem();
                return Ok(Items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreatOrder")]
        public async Task<IActionResult> CreatOrder([FromForm] OrderViewModel model )
        {
            try
            {
                var val = await _orderRepo.Create(model);
                if (val == 1)
                {
                    return Ok(1);
                }
                return Ok("Not Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
