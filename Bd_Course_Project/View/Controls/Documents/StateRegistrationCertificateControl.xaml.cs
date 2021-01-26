using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Documents
{
    /// <summary>
    /// Interaction logic for StateRegistrationCertificateControl.xaml
    /// </summary>
    public partial class StateRegistrationCertificateControl : UserControl
    {
        private readonly ObservableCollection<StateRegistrationCertificate> _stateRegistrationCertificates;

        public StateRegistrationCertificateControl()
        {
            InitializeComponent();

            _stateRegistrationCertificates = new ObservableCollection<StateRegistrationCertificate>(DataModel.StateRegistrationCertificates);
            dgGrid.ItemsSource = _stateRegistrationCertificates;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try           
            {
                StateRegistrationCertificate conformCertificate = new StateRegistrationCertificate(Convert.ToInt32(ListBox_RegistrationNumber.Text), ListBox_CodeTransportUnion.Text, ListBox_RegistrationDate.DisplayDate);
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
            if (dgGrid.SelectedItem is StateRegistrationCertificate stateRegistrationCertificate)
            {
                stateRegistrationCertificate.Remove();
                _stateRegistrationCertificates.Remove(dgGrid.SelectedItem as StateRegistrationCertificate);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is StateRegistrationCertificate stateRegistrationCertificate)
                {
                    int id = DataModel.StateRegistrationCertificates.FindIndex(x => x.Id == stateRegistrationCertificate.Id);

                    int index = _stateRegistrationCertificates.IndexOf(dgGrid.SelectedItem as StateRegistrationCertificate);
                    if (index >= 0)
                    {
                        _stateRegistrationCertificates[index] = new StateRegistrationCertificate(Convert.ToInt32(ListBox_RegistrationNumber.Text), ListBox_CodeTransportUnion.Text, ListBox_RegistrationDate.DisplayDate);
                    }

                    DataModel.StateRegistrationCertificates[id] = _stateRegistrationCertificates[index];
                    stateRegistrationCertificate = _stateRegistrationCertificates[index];
                    stateRegistrationCertificate.Update();
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
