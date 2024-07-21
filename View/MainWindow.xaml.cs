using HospitalRecord.Model;
using HospitalRecord.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HospitalRecord.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static ListView list;

        public MainWindow()
        {
            InitializeComponent();
            // Get ListView object
            list = PeopleList;

            // Set Context's path to the corresponding ViewModel
            MainWindowViewModel mainViewModel = new MainWindowViewModel();
            this.DataContext = mainViewModel;
            
        }

        // When the searchbox is used, filter out patients according to the typed text
        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PeopleList.Items.Filter = FilterMethod;
        }

        private bool FilterMethod(object obj)
        {
            Patient patient = (Patient)obj;

            return patient.Name.ToLower().Contains(FilterBox.Text.ToLower());
        }

        // Close application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
