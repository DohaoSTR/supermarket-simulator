using Bd_Course_Project.View;
using Bd_Course_Project.View.Controls;
using Bd_Course_Project.View.Menu;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Bd_Course_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<SubItem> menuProducts = new List<SubItem>
            {
                new SubItem("Прибывшие продукты"),
                new SubItem("Продукты на складе"),
                new SubItem("Проданные продукты"),
                new SubItem("Список всех продуктов")
            };
            ItemMenu item0 = new ItemMenu("Продукты", menuProducts, PackIconKind.Food);

            List<SubItem> menuDocuments = new List<SubItem>
            {
                new SubItem("Ветеринарные сертификаты"),
                new SubItem("Свидетельства о госрегистрации"),
                new SubItem("Сертификаты о соответствии"),
                new SubItem("Декларации о соответствии"),
                new SubItem("Товарно-транспортные накладные")
            };
            ItemMenu item1 = new ItemMenu("Документы", menuDocuments, PackIconKind.FileDocument);

            List<SubItem> menuCards = new List<SubItem>
            {
                new SubItem("Подарочные карты"),
                new SubItem("Скидочные карты")
            };
            ItemMenu item2 = new ItemMenu("Карты магазина", menuCards, PackIconKind.CreditCard);

            List<SubItem> menuStocks = new List<SubItem>
            {
                new SubItem("Скидки"),
                new SubItem("Специальные предложения")
            };
            ItemMenu item3 = new ItemMenu("Акции", menuStocks, PackIconKind.Percent);

            Menu.Children.Add(new MenuItemControl(item0, this));
            Menu.Children.Add(new MenuItemControl(item1, this));
            Menu.Children.Add(new MenuItemControl(item2, this));
            Menu.Children.Add(new MenuItemControl(item3, this));

            GridMain.Children.Add(new ProductsControl());
        }

        private void Turn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void KnowledgeBase_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Add(new KnowledgeBaseControl());
        }
    }
}
