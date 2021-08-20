using ECommerceWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWebsite.data
{
    public static class ProductDb
    {
        // returns total amount of products as in int
        public async static Task<int> GetProductsAsync(ProductContext context)
        {
            return await(from p in context.Products
                  select p).CountAsync();
        }

        public async static Task<List<Product>> GetProductsAsync(ProductContext context, int PageSize, int PageNum, int Offset)
        {
            List<Product> products =
                await (from p in context.Products
                       orderby p.Title ascending
                       select p)
                       .Skip(PageSize * (PageNum - Offset))
                       .Take(PageSize)
                       .ToListAsync();

            return products;
        }

        public async static Task<Product> AddProductAsync(ProductContext context, Product p)
        {
            context.Products.Add(p);
            await context.SaveChangesAsync();
            return p;
        }

        public async static Task<Product> EditProductAsync(ProductContext context, Product p)
        {
            context.Entry(p).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return p;
        }

        public async static Task<Product> DeleteProductAsync(ProductContext context, int id)
        {
            Product p =
               await (from prod in context.Products
                      where prod.ProductId == id
                      select prod).SingleAsync();

            String prodTitle = p.Title;

            context.Entry(p).State = EntityState.Deleted;
            await context.SaveChangesAsync();

            return p;
        }
    }
}
