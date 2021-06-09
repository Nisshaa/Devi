using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Devi
{
    public partial class Devices
    {
        public Uri ImagePreview
        {
            get
            {
                var imageName = System.IO.Path.Combine(Environment.CurrentDirectory, Logo ?? "");
                return System.IO.File.Exists(imageName) ? new Uri(imageName) : new Uri("pack://application:,,,/Images/picture.png");
            }
        }
    }

}
namespace Devi.window
{
   
    /// <summary>
    /// Логика взаимодействия для order.xaml
    /// </summary>
    public partial class Add : Window, INotifyPropertyChanged
    {
        private List<DevicesOrder> _OrderList;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<DevicesOrder> OrderList
        {
            get
            {
                return _OrderList;
            }
            set
            {
                _OrderList = value;
                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
                }
            }
        }

        public Add()
        {
            InitializeComponent();
            this.DataContext = this;
            OrderList = Core.DB.DevicesOrder.ToList();
        }
        private void DelOrd_Click(object sender, RoutedEventArgs e)
        {
            var item = ProductListView.SelectedItem as DevicesOrder;
            Core.DB.DevicesOrder.Remove(item);
            Core.DB.SaveChanges();
            OrderList = Core.DB.DevicesOrder.ToList();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            var SelectedService = ProductListView.SelectedItem as DevicesOrder;
            var EditServiceWindow = new window.ServiceWindow(SelectedService);
            if ((bool)EditServiceWindow.ShowDialog())
            {
                // при успешном завершении не забываем перерисовать список услуг
                PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
                // и еще счетчики - их добавьте сами
            }
        }
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            //  создаем новую услугу
            var NewService = new DevicesOrder();

            var NewServiceWindow = new ServiceWindow(NewService);
            if ((bool)NewServiceWindow.ShowDialog())
            {
                //список услуг нужно перечитать с сервера
                OrderList = Core.DB.DevicesOrder.ToList();

                PropertyChanged(this, new PropertyChangedEventArgs("OrderList"));
            }
        }
    }
}