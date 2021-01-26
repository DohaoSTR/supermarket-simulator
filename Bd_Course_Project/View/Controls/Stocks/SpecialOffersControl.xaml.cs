using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Controls.Stocks
{
    /// <summary>
    /// Interaction logic for SpecialOffersControl.xaml
    /// </summary>
    public partial class SpecialOffersControl : UserControl
    {
        private readonly ObservableCollection<SpecialOffer> _specialOffers;

        public SpecialOffersControl()
        {
            InitializeComponent();

            _specialOffers = new ObservableCollection<SpecialOffer>(DataModel.SpecialOffers);
            dgGrid.ItemsSource = _specialOffers;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                SpecialOffer specialOffer = new SpecialOffer(ListBox_Name.Text, ListBox_DateStart.DisplayDate, ListBox_DateEnd.DisplayDate);
                specialOffer.Id = specialOffer.Add();
                _specialOffers.Add(specialOffer);
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
            if (dgGrid.SelectedItem is SpecialOffer specialOffer)
            {
                specialOffer.Remove();
                _specialOffers.Remove(dgGrid.SelectedItem as SpecialOffer);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is SpecialOffer specialOffer)
                {
                    int id = DataModel.SpecialOffers.FindIndex(x => x.Id == specialOffer.Id);

                    int index = _specialOffers.IndexOf(dgGrid.SelectedItem as SpecialOffer);
                    if (index >= 0)
                    {
                        _specialOffers[index] = new SpecialOffer(specialOffer.Id, DataModel.DataAdapter, ListBox_Name.Text, ListBox_DateStart.DisplayDate, ListBox_DateEnd.DisplayDate);
                    }

                    DataModel.SpecialOffers[id] = _specialOffers[index];
                    specialOffer = _specialOffers[index];
                    specialOffer.Update();
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
