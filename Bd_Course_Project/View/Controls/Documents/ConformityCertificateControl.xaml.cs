using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Documents
{
    /// <summary>
    /// Interaction logic for ConformityCertificateControl.xaml
    /// </summary>
    public partial class ConformityCertificateControl : UserControl
    {
        private readonly ObservableCollection<ConformityCertificate> _conformityCertificates;

        public ConformityCertificateControl()
        {
            InitializeComponent();

            _conformityCertificates = new ObservableCollection<ConformityCertificate>(DataModel.ConformityCertificates);
            dgGrid.ItemsSource = _conformityCertificates;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ConformityCertificate conformCertificate = new ConformityCertificate(Convert.ToInt32(ListBox_RegistrationNumber.Text),
                ListBox_CodeTransportUnion.Text, ListBox_RegistrationDate.DisplayDate, ListBox_EndDate.DisplayDate);
                conformCertificate.Id = conformCertificate.Add();
                _conformityCertificates.Add(conformCertificate);
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
            if (dgGrid.SelectedItem is ConformityCertificate conformityCertificate)
            {
                conformityCertificate.Remove();
                _conformityCertificates.Remove(dgGrid.SelectedItem as ConformityCertificate);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is ConformityCertificate conformityCertificate)
                {
                    int id = DataModel.ConformityCertificates.FindIndex(x => x.Id == conformityCertificate.Id);
                    int index = _conformityCertificates.IndexOf(dgGrid.SelectedItem as ConformityCertificate);
                    if (index >= 0)
                    {
                        _conformityCertificates[index] = new ConformityCertificate(conformityCertificate.Id, DataModel.DataAdapter, Convert.ToInt32(ListBox_RegistrationNumber.Text),
                        ListBox_CodeTransportUnion.Text, ListBox_RegistrationDate.DisplayDate, ListBox_EndDate.DisplayDate);
                    }

                    DataModel.ConformityCertificates[id] = _conformityCertificates[index];
                    conformityCertificate = _conformityCertificates[index];
                    conformityCertificate.Update();
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
