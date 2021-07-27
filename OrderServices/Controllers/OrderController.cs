using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderServices.Data;
using OrderServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _db;

        public OrderController(OrderContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Order> getOrder()
        {
            return _db.Orders.Include(c => c.product);
        }

        [HttpGet("{id}")]
        public IActionResult getOrderById([FromRoute] int id)

        {
            var response = _db.Orders.Include(m => m.product).Where(m => m.orderId == id).ToList();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult createOrder([FromBody] Order order)
        {
            //Product product = new Product();
            Order ord = new Order()
            {
                productId = order.productId,
                orderDate = DateTime.Now.Date,
                Quantity=order.Quantity,
                Amount=order.Amount

            };
            // employee.EmployeeId = 14;
            _db.Orders.Add(ord);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _db.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
