using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.Cards
{
    /// <summary>
    /// Interaction logic for GiftCardsControl.xaml
    /// </summary>
    public partial class GiftCardsControl : UserControl
    {
        private readonly ObservableCollection<GiftCard> _giftCards;

        public GiftCardsControl()
        {
            InitializeComponent();

            _giftCards = new ObservableCollection<GiftCard>(DataModel.GiftCards);
            dgGrid.ItemsSource = _giftCards;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GiftCard giftCard = new GiftCard(Convert.ToInt32(ListBox_RegistrationNumber.Text), Convert.ToInt32(ListBox_FaceValue.Text));
                giftCard.Id = giftCard.Add();
                _giftCards.Add(giftCard);
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
            if (dgGrid.SelectedItem is GiftCard giftCard)
            {
                giftCard.Remove();
                _giftCards.Remove(dgGrid.SelectedItem as GiftCard);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is GiftCard giftCard)
                {
                    int id = DataModel.GiftCards.FindIndex(x => x.Id == giftCard.Id);
                    int index = _giftCards.IndexOf(dgGrid.SelectedItem as GiftCard);
                    if (index >= 0)
                    {
                        _giftCards[index] = new GiftCard(giftCard.Id, DataModel.DataAdapter, Convert.ToInt32(ListBox_RegistrationNumber.Text), Convert.ToInt32(ListBox_FaceValue.Text));
                    }

                    DataModel.GiftCards[id] = _giftCards[index];
                    giftCard = _giftCards[index];
                    giftCard.Update();
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
