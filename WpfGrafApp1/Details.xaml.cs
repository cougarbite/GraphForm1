using GrafLib;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfGrafApp1
{
    /// <summary>
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        Graf selectedGraf;
        //public Details()
        //{
        //    InitializeComponent();
        //}

        public Details(Graf graf)
        {
            selectedGraf = graf;
            InitializeComponent();
            PopulateForm();
        }

        private void PopulateForm()
        {
            grafNameLabel.Content = selectedGraf.Name;
            grafImage.Source = new BitmapImage(new Uri($@"C:\Users\darks\source\repos\GraphForm1\WpfGrafApp1\Grafuri\{selectedGraf.Name}.png"));
        }
    }
}
