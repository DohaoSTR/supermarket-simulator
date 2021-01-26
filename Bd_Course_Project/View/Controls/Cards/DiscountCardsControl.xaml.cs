using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.Cards
{
    /// <summary>
    /// Interaction logic for DiscountCardsControl.xaml
    /// </summary>
    public partial class DiscountCardsControl : UserControl
    {
        private readonly ObservableCollection<DiscountCard> _discountCards;

        public DiscountCardsControl()
        {
            InitializeComponent();

            _discountCards = new ObservableCollection<DiscountCard>(DataModel.DiscountCards);
            dgGrid.ItemsSource = _discountCards;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DiscountCard discountCard = new DiscountCard(Convert.ToInt32(ListBox_RegistrationNumber.Text), Convert.ToInt32(ListBox_BonusValue.Text));
                discountCard.Id = discountCard.Add();
                _discountCards.Add(discountCard);
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

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (dgGrid.SelectedItem is DiscountCard discountCard)
            {
                discountCard.Remove();
                _discountCards.Remove(dgGrid.SelectedItem as DiscountCard);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is DiscountCard discountCard)
                {
                    int id = DataModel.DiscountCards.FindIndex(x => x.Id == discountCard.Id);
                    int index = _discountCards.IndexOf(dgGrid.SelectedItem as DiscountCard);

                    if (index >= 0)
                    {
                        _discountCards[index] = new DiscountCard(discountCard.Id, DataModel.DataAdapter, Convert.ToInt32(ListBox_RegistrationNumber.Text), Convert.ToInt32(ListBox_BonusValue.Text));
                    }

                    DataModel.DiscountCards[id] = _discountCards[index];
                    discountCard = _discountCards[index];
                    discountCard.Update();
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
