using ExamJanvier2023.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication1.ViewModels;

namespace ExamJanvier2023.ViewModels
{
    public class ProductVM
    {
        private NorthwindContext dc = new NorthwindContext();


        private ProductModel _selectedProduct;
        private ObservableCollection<ProductModel> _ProductsList;
        private ObservableCollection<CountryProductStats> _CountryStats;

        private DelegateCommand _abandonnateCommand;



        public ObservableCollection<CountryProductStats> CountryStats
        {
            get
            {
                if (_CountryStats == null)
                {
                    _CountryStats = loadCountryProductStats();
                }

                return _CountryStats;

            }
        }

        private ObservableCollection<CountryProductStats> loadCountryProductStats()
        {
            var stats = dc.Orders
                .Join(dc.OrderDetails,
                      order => order.OrderId,
                      orderDetail => orderDetail.OrderId,
                      (order, orderDetail) => new { order, orderDetail })
                .Join(dc.Products,
                      joined => joined.orderDetail.ProductId,
                      product => product.ProductId,
                      (joined, product) => new { joined.order, product })
                .GroupBy(group => group.order.ShipCountry)
                .Select(group => new CountryProductStats
                {
                    Country = group.Key,
                    ProductCount = group.Select(g => g.product.ProductId).Distinct().Count()
                })
                .OrderByDescending(stat => stat.ProductCount)
                .ToList();

            return new ObservableCollection<CountryProductStats>(stats);
        }




        public ObservableCollection<ProductModel> ProductsList
        {
            get
            {
                if (_ProductsList == null)
                {
                    _ProductsList = loadProduct();
                }

                return _ProductsList;

            }

        }


        private ObservableCollection<ProductModel> loadProduct()
        {
            ObservableCollection<ProductModel> localCollection = new ObservableCollection<ProductModel>();
            foreach (var item in dc.Products)
            {
                if (item.Discontinued) continue;
                localCollection.Add(new ProductModel(item));

            }

            return localCollection;

        }


        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }

        }


        public DelegateCommand AbandonnateCommand
        {
            get
            {
                return _abandonnateCommand = _abandonnateCommand ?? new DelegateCommand(AbandonnateProduct);
            }

        }

        private void AbandonnateProduct()
        {
            // Vérifiez si un produit est sélectionné
            if (SelectedProduct == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit.");
                return;
            }


            Product verif = dc.Products.Where(e => e.ProductId == SelectedProduct.MonProduct.ProductId).SingleOrDefault();
            if (verif != null)
            {
                // Marquer le produit comme abandonné
                verif.Discontinued = true;
                dc.SaveChanges();
                ProductsList.Remove(SelectedProduct);
            }
            MessageBox.Show("Enregistrement en base de données fait");

        }

    }
}
