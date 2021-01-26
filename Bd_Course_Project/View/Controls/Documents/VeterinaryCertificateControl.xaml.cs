using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Documents
{
    /// <summary>
    /// Interaction logic for VeterinaryCertificateControl.xaml
    /// </summary>
    public partial class VeterinaryCertificateControl : UserControl
    {
        private readonly ObservableCollection<VeterinaryCertificate> _stateRegistrationCertificates;

        public VeterinaryCertificateControl()
        {
            InitializeComponent();

            _stateRegistrationCertificates = new ObservableCollection<VeterinaryCertificate>(DataModel.VeterinaryCertificates);
            dgGrid.ItemsSource = _stateRegistrationCertificates;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                VeterinaryCertificate conformCertificate = new VeterinaryCertificate(Convert.ToInt32(ListBox_RegistrationNumber.Text),
                ListBox_Form.Text, ListBox_RegistrationDate.DisplayDate);
                conformCertificate.Id = conformCertificate.Add();
                _stateRegistrationCertificates.Add(conformCertificate);
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
            if (dgGrid.SelectedItem is VeterinaryCertificate veterinaryCertificate)
            {
                veterinaryCertificate.Remove();
                _stateRegistrationCertificates.Remove(dgGrid.SelectedItem as VeterinaryCertificate);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is VeterinaryCertificate veterinaryCertificate)
                {
                    int id = DataModel.VeterinaryCertificates.FindIndex(x => x.Id == veterinaryCertificate.Id);

                    int index = _stateRegistrationCertificates.IndexOf(dgGrid.SelectedItem as VeterinaryCertificate);
                    if (index >= 0)
                    {
                        _stateRegistrationCertificates[index] = new VeterinaryCertificate(veterinaryCertificate.Id, DataModel.DataAdapter, Convert.ToInt32(ListBox_RegistrationNumber.Text),
                        ListBox_Form.Text, ListBox_RegistrationDate.DisplayDate);
                    }

                    DataModel.VeterinaryCertificates[id] = _stateRegistrationCertificates[index];
                    veterinaryCertificate = _stateRegistrationCertificates[index];
                    veterinaryCertificate.Update();
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
