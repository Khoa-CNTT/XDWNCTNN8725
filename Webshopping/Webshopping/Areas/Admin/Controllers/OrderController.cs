﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshopping.Models;
using Webshopping.Repository;

namespace Webshopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        public OrderController(DataContext context)
        {
            _dataContext = context;
        }
        [HttpGet("Order")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Orders.OrderByDescending(p => p.Id).ToListAsync());
        }
        [HttpGet("Order/View")]
        public async Task<IActionResult> ViewOrder(string ordercode)
        {
            var DetailsOrder = await _dataContext.OrderDetails.Include(od => od.Product).Where(od => od.OrderCode == ordercode).ToListAsync();
            var totalAmount = DetailsOrder.Sum(o => o.Price * o.Quantity);  // Tính tổng tiền

            ViewBag.TotalAmount = totalAmount;

            return View(DetailsOrder);
        }
    }
}
