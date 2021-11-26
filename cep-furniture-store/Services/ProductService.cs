using cep_furniture_store.Data;
using cep_furniture_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Services
{
    
    public class ProductService
    {
        //private readonly ApplicationDbContext _context;

        //public ProductService(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        //public DbSet<Product> getAll()
        //{
        //    return _context.products;
        //}

        //public object getCurrentPage(int? queryPage, int limit, int categoryId)
        //{
        //    var query = (from products in _context.products select products);
        //    if (categoryId != 0)
        //    {
        //        query = query.Where(a => a.categoryId == categoryId);
        //    }
        //    int page = queryPage.GetValueOrDefault(1) == 0 ? 1 : queryPage.GetValueOrDefault(1);
        //    return new
        //    {
        //        products = query.Skip((page - 1) * limit).Take(limit)
        //    };
        //}
    }
}
