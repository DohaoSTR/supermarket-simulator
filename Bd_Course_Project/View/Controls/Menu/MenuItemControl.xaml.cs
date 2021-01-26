using Bd_Course_Project.View.Controls.Cards;
using Bd_Course_Project.View.Controls.ListOfProducts;
using Bd_Course_Project.View.Controls.Stocks;
using Bd_Course_Project.View.Documents;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Bd_Course_Project.View.Menu
{
    /// <summary>
    /// Interaction logic for MenuItemControl.xaml
    /// </summary>
    public partial class MenuItemControl : UserControl
    {
        private readonly MainWindow _mainWindow;

        public MenuItemControl(ItemMenu itemMenu, MainWindow mainWindow)
        {
            InitializeComponent();

            _mainWindow = mainWindow;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            DataContext = itemMenu;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            Dictionary<string, UserControl> controls = new Dictionary<string, UserControl>
            {
                { "Прибывшие продукты", new  ArrivalProductsControl() },
                { "Продукты на складе", new StoredProductsControl() },
                { "Проданные продукты", new SoldProductsControl() },
                { "Список всех продуктов", new ProductsControl() },
                { "Ветеринарные сертификаты", new VeterinaryCertificateControl() },
                { "Свидетельства о госрегистрации", new  StateRegistrationCertificateControl() },
                { "Сертификаты о соответствии", new  ConformityCertificateControl() },
                { "Декларации о соответствии", new  ConformityDeclarationControl() },
                { "Товарно-транспортные накладные", new BillsOfLadingControl() },
                { "Подарочные карты", new GiftCardsControl() },
                { "Скидочные карты", new DiscountCardsControl() },
                { "Скидки", new DiscountsControl() },
                { "Специальные предложения", new SpecialOffersControl() }
            };

            _mainWindow.GridMain.Children.Clear();
            _mainWindow.GridMain.Children.Add(controls[button.Content.ToString()]);
        }
    }
}
