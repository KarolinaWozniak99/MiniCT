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
    public partial class Panel : UserControl, IPanel
    {
        string aktualnaSciezka = null;
        string[] dyski = null;
        string[] zawartoscSciezki = null;

        public Panel()
        {
            InitializeComponent();
            dyski = Directory.GetLogicalDrives();
            aktualnaSciezka = Path.GetPathRoot(Environment.SystemDirectory);
            zawartoscSciezki = GetContents();

            Path_tb.Text = AktualnaSciezka;
            CBDyski.ItemsSource = Dyski;
            CBDyski.SelectedIndex = -1;
            ListBoxSciezki.ItemsSource = ZawartoscSciezki;
        }

        public string AktualnaSciezka
        {
            get => aktualnaSciezka;
            set
            {
                aktualnaSciezka = value;
            }
        }
        public string[] Dyski
        {
            get => dyski;
            set { dyski = value; }
            
        }
        public string[] ZawartoscSciezki
        {
            get => zawartoscSciezki;
            set
            {
                zawartoscSciezki = value;
            }
        }

        public string[] GetContents()
        {
            string[] temp1;
            string[] temp2;
            string[] temp3;
            try
            {
                temp1 = Directory.GetDirectories(aktualnaSciezka);
                for (int i = 0; i < temp1.Length; i++)
                {
                    temp1[i] = Path.GetFileName(temp1[i]);
                    temp1[i] = $"<D>{temp1[i]}";
                }

                temp2 = Directory.GetFiles(aktualnaSciezka);
                for (int i = 0; i < temp2.Length; i++)
                    temp2[i] = Path.GetFileName(temp2[i]);

                int l = temp1.Length + temp2.Length;
                temp3 = null;
                if (!dyski.Contains(aktualnaSciezka))
                {
                    temp3 = new string[l + 1];
                    temp3[0] = "..";
                    Array.Copy(temp1, 0, temp3, 1, temp1.Length);
                    Array.Copy(temp2, 0, temp3, temp1.Length + 1, temp2.Length);
                }
                else
                {
                    temp3 = new string[l];
                    Array.Copy(temp1, temp3, temp1.Length);
                    Array.Copy(temp2, 0, temp3, temp1.Length, temp2.Length);
                }

            }
            catch (Exception b)
            {
                MessageBox.Show(b.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                aktualnaSciezka = aktualnaSciezka.Substring(0, aktualnaSciezka.Length - 1);
                int index = aktualnaSciezka.LastIndexOf("\\");
                aktualnaSciezka = aktualnaSciezka.Substring(0, index + 1);
                Path_tb.Text = aktualnaSciezka;
                return GetContents();
            }
            return temp3;
        }

        private void CBDyski_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (CBDyski.SelectedIndex != -1)
            {
                aktualnaSciezka = dyski[CBDyski.SelectedIndex];
                Path_tb.Text = aktualnaSciezka;
                zawartoscSciezki = GetContents();
                ListBoxSciezki.ItemsSource = zawartoscSciezki;
            }
        }

        private void ListBoxSciezki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxSciezki.SelectedIndex != -1)
            {
                string item = zawartoscSciezki[ListBoxSciezki.SelectedIndex];

                if (item.StartsWith("<D>"))
                {
                    aktualnaSciezka += $"{item.Substring(3)}\\";
                    Path_tb.Text = aktualnaSciezka;
                    zawartoscSciezki = GetContents();
                    ListBoxSciezki.ItemsSource = zawartoscSciezki;
                    
                }
                else if (item == "..")
                {
                    aktualnaSciezka = aktualnaSciezka.Substring(0, aktualnaSciezka.Length - 1);
                    int index = aktualnaSciezka.LastIndexOf("\\");
                    aktualnaSciezka = aktualnaSciezka.Substring(0, index + 1);
                    Path_tb.Text = aktualnaSciezka;
                    zawartoscSciezki = GetContents();
                    ListBoxSciezki.ItemsSource = zawartoscSciezki;
                   
                }
            }
        }




        
        
    }


}

