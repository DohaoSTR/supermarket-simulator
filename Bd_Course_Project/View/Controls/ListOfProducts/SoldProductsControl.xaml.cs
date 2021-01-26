using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.ListOfProducts
{
    /// <summary>
    /// Interaction logic for SoldProductsControl.xaml
    /// </summary>
    public partial class SoldProductsControl : UserControl
    {
        private readonly ObservableCollection<SoldProduct> _soldProducts;

        public SoldProductsControl()
        {
            InitializeComponent();

            _soldProducts = new ObservableCollection<SoldProduct>(DataModel.SoldProducts);
            dgGrid.ItemsSource = _soldProducts;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                Product product = new Product(Convert.ToInt32(ListBox_IdOfProduct.Text));

                Discount discount = new Discount();
                if (ListBox_Discount.Text != "")
                {
                    discount = new Discount(Convert.ToInt32(ListBox_Discount.Text));
                }

                SpecialOffer specialOffer = new SpecialOffer();
                if (ListBox_SpecialOffer.Text != "")
                {
                    specialOffer = new SpecialOffer(Convert.ToInt32(ListBox_SpecialOffer.Text));
                }

                DiscountCard discountCard = new DiscountCard();
                if (ListBox_SpecialOffer.Text != "")
                {
                    discountCard = new DiscountCard(Convert.ToInt32(ListBox_DiscountCard.Text));
                }

                GiftCard giftCard = new GiftCard();
                if (ListBox_SpecialOffer.Text != "")
                {
                    giftCard = new GiftCard(Convert.ToInt32(ListBox_GiftCard.Text));
                }


                SoldProduct arrivalProduct = new SoldProduct(product, Convert.ToDateTime(ListBox_SoldDate.Text), Convert.ToInt32(ListBox_NumberOfProducts.Text),
                    discount, specialOffer, discountCard, giftCard);
                arrivalProduct.Id = arrivalProduct.Add();
                _soldProducts.Add(arrivalProduct);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.ParamName);
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }

        private void Remove_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dgGrid.SelectedItem is SoldProduct soldProduct)
            {
                soldProduct.Remove();
                _soldProducts.Remove(dgGrid.SelectedItem as SoldProduct);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is SoldProduct soldProduct)
                {
                    int id = DataModel.SoldProducts.FindIndex(x => x.Id == soldProduct.Id);

                    int index = _soldProducts.IndexOf(dgGrid.SelectedItem as SoldProduct);
                    if (index >= 0)
                    {
                        _soldProducts[index] = new SoldProduct(soldProduct.Id, DataModel.DataAdapter, new Product(Convert.ToInt32(ListBox_IdOfProduct.Text)), Convert.ToDateTime(ListBox_SoldDate.Text), Convert.ToInt32(ListBox_NumberOfProducts.Text),
                        new Discount(Convert.ToInt32(ListBox_Discount.Text)), new SpecialOffer(Convert.ToInt32(ListBox_SpecialOffer.Text)), new DiscountCard(Convert.ToInt32(ListBox_DiscountCard.Text)),
                        new GiftCard(Convert.ToInt32(ListBox_GiftCard.Text)));
                    }

                    DataModel.SoldProducts[id] = _soldProducts[index];
                    soldProduct = _soldProducts[index];
                    soldProduct.Update();
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.ParamName);
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }
    }
}
