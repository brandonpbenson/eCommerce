﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MisterRobotoArigato.Models;
using MisterRobotoArigato.Models.ViewModel;

namespace MisterRobotoArigato.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IRobotoRepo _robotoRepo;
        private readonly IBasketRepo _basketRepo;
        private readonly ICheckoutRepo _checkoutRepo;
        private readonly IConfiguration Configuration;
        private readonly IEmailSender _emailSender;
        private UserManager<ApplicationUser> _userManager;

        public CheckoutController(IRobotoRepo robotoRepo, IConfiguration configuration, 
            IBasketRepo basketRepo, ICheckoutRepo checkoutRepo, 
            IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _robotoRepo = robotoRepo;
            _basketRepo = basketRepo;
            _checkoutRepo = checkoutRepo;
            _userManager = userManager;
            _emailSender = emailSender;
            Configuration = configuration;
        }

        public async Task<IActionResult> Index(string discountCoupon)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            Basket datBasket = _basketRepo.GetUserBasketByEmail(user.Email).Result;

            if (datBasket.BasketItems.Count == 0)
            {
                return RedirectToAction("MyBasket", "Shop");
            }

            CheckoutViewModel datCheckoutVM = new CheckoutViewModel
            {
                Basket = datBasket,
            };

            if (!String.IsNullOrEmpty(discountCoupon))
            {
                string upperCaseCoupon = discountCoupon.ToUpper();

                if (upperCaseCoupon == "IAMDOGE" || upperCaseCoupon == "ILIKEFATCATS")
                {
                    datCheckoutVM.DiscountPercent = 20;
                }

                if (upperCaseCoupon == "CODEFELLOWS")
                {
                    datCheckoutVM.DiscountPercent = 10;
                }

                datCheckoutVM.DiscountName = upperCaseCoupon;
            }

            return View(datCheckoutVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(CheckoutViewModel cvm)
        {

            if (cvm == null || cvm.Address == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Basket datBasket = _basketRepo.GetUserBasketByEmail(user.Email).Result;

            cvm.Basket = datBasket;
            return View("Shipping", cvm);
        }

        public async Task<IActionResult> Shipping(CheckoutViewModel cvm)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Basket datBasket = _basketRepo.GetUserBasketByEmail(user.Email).Result;

            cvm.Basket = datBasket;

            if (ModelState.IsValid)
            {
                return View(cvm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Confirm(CheckoutViewModel cvm)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            Basket datBasket = _basketRepo.GetUserBasketByEmail(user.Email).Result;
            cvm.Basket = datBasket;

            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Shipping));
            // add address to database
            await _checkoutRepo.CreateAddress(cvm.Address);

            // create an new order object and load the order items onto it
            Order datOrder = new Order
            {
                UserID = user.Id,
                AddressID = cvm.Address.ID,
                Address = cvm.Address,
                Shipping = cvm.Shipping,
                DiscountName = cvm.DiscountName,
                DiscountPercent = cvm.DiscountPercent,
                DiscountAmt = cvm.DiscountAmt,
                TotalItemQty = cvm.TotalItemQty,
                Subtotal = cvm.Subtotal,
                Total = cvm.Total,
            };

            // add order to the database table
            // I'm doing this first in hopes that the order generates an ID that
            // I can add to the order items. Here's hoping...
            await _checkoutRepo.AddOrderAsync(datOrder);

            // turn basket items into order items
            List<OrderItem> demOrderItems = new List<OrderItem>();
            foreach(var item in datBasket.BasketItems)
            {
                OrderItem tempOrderItem = new OrderItem
                {
                    ProductID = item.ProductID,
                    OrderID = datOrder.ID,
                    UserID = user.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    ImgUrl = item.ImgUrl,
                    UnitPrice = item.UnitPrice
                };

                // add order item to the database table
                await _checkoutRepo.AddOrderItemToOrderAsync(tempOrderItem);
                demOrderItems.Add(tempOrderItem);
            }

            // attach orderitems to order
            datOrder.OrderItems = demOrderItems;

            string htmlMessage = "Thank you for shopping with us!  You ordered: </br>"; 
            foreach (var item in datOrder.OrderItems)
            {
                htmlMessage += $"Item: {item.ProductName}, Quantity: {item.Quantity}</br>";
            };

            await _emailSender.SendEmailAsync(user.Email, "Order Information",
                        htmlMessage);
            // empty out basket
            await _basketRepo.ClearOutBasket(cvm.Basket.BasketItems);


            return View("Confirmed", datOrder);
        }

        public IActionResult Confirmed(Order order)
        {
            return View(order);
        }
    }
}