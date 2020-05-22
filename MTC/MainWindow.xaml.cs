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
using System.IO;

namespace MTC
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Lewy.ListBoxSciezki.SelectedIndex != -1)
            {

                string plikL = Path.Combine(Lewy.AktualnaSciezka, Lewy.ZawartoscSciezki[Lewy.ListBoxSciezki.SelectedIndex]);
                string plikR = Path.Combine(Prawy.AktualnaSciezka, Lewy.ZawartoscSciezki[Lewy.ListBoxSciezki.SelectedIndex]);

                try
                {
                    if (Prawy.ZawartoscSciezki.Contains(Lewy.ZawartoscSciezki[Lewy.ListBoxSciezki.SelectedIndex]))
                    {
                        MessageBoxResult overwrite = MessageBox.Show($"Czy chcesz nadpisać plik ?", "OVERWRITE", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (overwrite == MessageBoxResult.Yes)
                        {
                            File.Copy(plikL, plikR, true);
                            Prawy.ListBoxSciezki.ItemsSource = Prawy.GetContents();
                        }
                    }
                    else
                    {
                        File.Copy(plikL, plikR, true);
                        Prawy.ListBoxSciezki.ItemsSource = Prawy.GetContents();
                    }
                }
                catch (Exception b)
                {
                    MessageBox.Show(b.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    Lewy.ListBoxSciezki.ItemsSource = Lewy.GetContents();
                    Prawy.ListBoxSciezki.ItemsSource = Prawy.GetContents();
                }

            }
        }
            
    }
}

