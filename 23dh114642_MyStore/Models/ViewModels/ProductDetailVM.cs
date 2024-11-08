using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace _23dh114642_MyStore.Models.ViewModels
{
    public class ProductDetailVM
    {
        public Product Product { get; set; }
        public int quantity { get; set; } = 1;
        public decimal estimatedValue => quantity * Product.ProductPrice;
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;
        public PagedList.IPagedList<Product> RelatedProducts { get; set; }
        //public List<Product> RelatedProducts { get; set; }
        public PagedList.IPagedList<Product> TopProducts { get; set; }

    }
}