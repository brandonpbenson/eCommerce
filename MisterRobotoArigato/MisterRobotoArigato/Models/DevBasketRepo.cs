﻿using Microsoft.EntityFrameworkCore;
using MisterRobotoArigato.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MisterRobotoArigato.Models
{
    public class DevBasketRepo : IBasketRepo
    {
        private RobotoDbContext _context;

        public DevBasketRepo(RobotoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// creates a basket for a user if the user tries to add an item to a basket and 
        /// doesn't already have a basket
        /// </summary>
        /// <param name="basket"></param>
        /// <returns></returns>
        public async Task<HttpStatusCode> CreateBasket(Basket basket)
        {
            try
            {
                _context.Add(basket);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }

        public async Task<HttpStatusCode> AddProductToBasket(string email, Product product)
        {
            try
            {
                Basket datBasket = GetUserBasketByEmail(email).Result;
                BasketItem datBasketItem = datBasket.BasketItems.FirstOrDefault(i => i.ProductID == product.ID);

                if (datBasketItem != null)
                {
                    datBasketItem.Quantity++;
                    await UpdateBasket(email, datBasketItem);
                    return HttpStatusCode.Created;
                }

                datBasketItem = new BasketItem
                {
                    ProductID = product.ID,
                    ProductName = product.Name,
                    CustomerEmail = email,
                    Quantity = 1,
                    UnitPrice = product.Price,
                    ImgUrl = product.ImgUrl
                };

                await _context.AddAsync(datBasketItem);
                await _context.SaveChangesAsync();
                return HttpStatusCode.Created;
            }

            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<HttpStatusCode> DeleteProductFromBasket(string email, BasketItem basketItem)
        {
            try
            {
                _context.Remove(basketItem);
                await _context.SaveChangesAsync();
                return HttpStatusCode.Created;
            }

            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<Basket> GetUserBasketByEmail(string email)
        {
            var prodInts = _context.BasketDetails.Where(d => d.CustomerEmail == email).Select(p => p.ProductID);
            List<BasketItem> demItems = _context.BasketDetails.Where(d => d.CustomerEmail == email).ToList();
            Basket datBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.CustomerEmail == email);
            if (datBasket == null)
            {
                datBasket = new Basket();
            }
            else
            {
                datBasket.BasketItems = demItems;
            }
            return datBasket;
        }

        public async Task<HttpStatusCode> UpdateBasket(string email, BasketItem basketItem)
        {
            try
            {
                _context.Update(basketItem);
                await _context.SaveChangesAsync();
                return HttpStatusCode.Created;
            }

            catch
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
