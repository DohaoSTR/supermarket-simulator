using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls
{
    /// <summary>
    /// Interaction logic for KnowledgeBaseControl.xaml
    /// </summary>
    public partial class KnowledgeBaseControl : UserControl
    {
        private readonly ObservableCollection<WriteOffProduct> _writeOffProducts;
        private readonly ObservableCollection<StoredProduct> _soonExpiredProducts = new ObservableCollection<StoredProduct>();
        private readonly ObservableCollection<NecessaryProduct> _necessaryProductsForOrder = new ObservableCollection<NecessaryProduct>();

        public KnowledgeBaseControl()
        {
            InitializeComponent();

            _writeOffProducts = new ObservableCollection<WriteOffProduct>(DataModel.WriteOffProducts);
            foreach (StoredProduct storedProduct in DataModel.StoredProducts.ToList())
            {
                if (storedProduct.Product.ExpirationDate <= DateTime.Now)
                {
                    storedProduct.Remove();

                    WriteOffProduct writeOffProduct = new WriteOffProduct(storedProduct.Product, storedProduct.DateArrival, storedProduct.NumberOfProducts, DateTime.Now);
                    writeOffProduct.Id = writeOffProduct.Add();
                    _writeOffProducts.Add(writeOffProduct);
                }
            }

            dgGrid.ItemsSource = _writeOffProducts;
            GetSoonExpiredProducts();
            dgGrid1.ItemsSource = _soonExpiredProducts;
            GetNecessaryProductsForOrder();
            dgGrid2.ItemsSource = _necessaryProductsForOrder;
        }

        private void GetSoonExpiredProducts()
        {
            List<StoredProduct> storedProducts = DataModel.StoredProducts;

            foreach(StoredProduct storedProduct in storedProducts)
            {
                if (storedProduct.Product.ExpirationDate <= DateTime.Now.AddDays(10))
                {
                    _soonExpiredProducts.Add(storedProduct);
                }
            }
        }

        private void GetNecessaryProductsForOrder()
        {
            List<StoredProduct> storedProducts = DataModel.StoredProducts;
            List<Product> products = DataModel.Products;
            
            foreach(Product product in products)
            {
                StoredProduct necessaryStoredProduct = new StoredProduct();
                foreach(StoredProduct storedProduct in storedProducts)
                {
                    if(product.Id == storedProduct.Product.Id)
                    {
                        necessaryStoredProduct = storedProduct;
                    }
                }

                if(product.ExpirationDate <= DateTime.Now.AddDays(10) || necessaryStoredProduct.NumberOfProducts <= 5)
                {
                    _necessaryProductsForOrder.Add(new NecessaryProduct(product));
                }
            }
        }
    }
}
