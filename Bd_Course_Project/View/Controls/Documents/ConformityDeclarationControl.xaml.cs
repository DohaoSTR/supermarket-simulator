using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Documents
{
    /// <summary>
    /// Interaction logic for ConformityDeclarationControl.xaml
    /// </summary>
    public partial class ConformityDeclarationControl : UserControl
    {
        private readonly ObservableCollection<ConformityDeclaration> _conformityDeclaration;

        public ConformityDeclarationControl()
        {
            InitializeComponent();

            _conformityDeclaration = new ObservableCollection<ConformityDeclaration>(DataModel.ConformityDeclarations);
            dgGrid.ItemsSource = _conformityDeclaration;
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ConformityDeclaration conformCertificate = new ConformityDeclaration(Convert.ToInt32(ListBox_RegistrationNumber.Text),
                ListBox_CodeTransportUnion.Text, ListBox_RegistrationDate.DisplayDate, ListBox_EndDate.DisplayDate);
                conformCertificate.Id = conformCertificate.Add();
                _conformityDeclaration.Add(conformCertificate);
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
            if (dgGrid.SelectedItem is ConformityDeclaration conformDeclaration)
            {
                conformDeclaration.Remove();
                _conformityDeclaration.Remove(dgGrid.SelectedItem as ConformityDeclaration);
            }
        }

        private void Update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgGrid.SelectedItem is ConformityDeclaration conformDeclaration)
                {
                    int id = DataModel.ConformityDeclarations.FindIndex(x => x.Id == conformDeclaration.Id);

                    int index = _conformityDeclaration.IndexOf(dgGrid.SelectedItem as ConformityDeclaration);
                    if (index >= 0)
                    {
                        _conformityDeclaration[index] = new ConformityDeclaration(conformDeclaration.Id, DataModel.DataAdapter, Convert.ToInt32(ListBox_RegistrationNumber.Text),
                        ListBox_CodeTransportUnion.Text, ListBox_RegistrationDate.DisplayDate, ListBox_EndDate.DisplayDate);
                    }

                    DataModel.ConformityDeclarations[id] = _conformityDeclaration[index];
                    conformDeclaration = _conformityDeclaration[index];
                    conformDeclaration.Update();
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
