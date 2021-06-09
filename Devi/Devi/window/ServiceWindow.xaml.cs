using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace Devi.window
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window, INotifyPropertyChanged
    {

        public List<Devices> UsermanList { get; set; }

        public ServiceWindow(DevicesOrder devicesOrder)
        {
            InitializeComponent();
            this.DataContext = this;
            CurrentOrder = devicesOrder;
            UsermanList = Core.DB.Devices.ToList();

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public DevicesOrder CurrentOrder { get; set; }
        public string WindowName
        {
            get
            {
                return CurrentOrder.Id == 0 ? "Новая услуга" : "Редоктирование улсгуи";
            }
        }

        

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {



            {
                if (CurrentOrder.Id == 0)
                    Core.DB.DevicesOrder.Add(CurrentOrder);


                try
                {
                    Core.DB.SaveChanges();
                }
                catch
                {
                }
                DialogResult = true;
            }



        }

        public string NewProduct
        {
            get
            {
                if (CurrentOrder.Id == 0) return "collapsed";
                return "visible";



            }
        }
    }
}