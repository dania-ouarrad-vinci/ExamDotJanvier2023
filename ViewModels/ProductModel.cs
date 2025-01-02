using ExamJanvier2023.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamJanvier2023.ViewModels
{
    public class ProductModel
    {

        private readonly Product _monProduct;

        public Product MonProduct
        {
            get { return _monProduct; }
        }




        public ProductModel(Product current)
        {
            this._monProduct = current;
        }


        public int ProductId
        {
            get { return _monProduct.ProductId; }

        }

        public String ProductName
        {
            get
            {
                return _monProduct.ProductName;
            }
        }

        public string CategoryName
        {
            get { return _monProduct.Category?.CategoryName ?? "Unknown"; }
        }

        public string ContactName
        {
            get { return _monProduct.Supplier?.ContactName ?? "Unknown"; }

        }

    }
}
