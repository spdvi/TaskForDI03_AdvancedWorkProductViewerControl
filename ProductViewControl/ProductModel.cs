using System;
using System.Collections.Generic;

namespace ProductViewControl
{
    class ProductModel
    {
        public int ProductModelID { get; set; }
        public string Name { get; set; }
        public decimal ListPrice { get; set; }
        public byte[] LargePhoto { get; set; }
        public List<Product> ProductSizes { get; set; }
    }
}
