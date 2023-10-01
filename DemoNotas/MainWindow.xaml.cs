using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DemoNotas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class Nota
    {
        public int Num { get; set; }
        public string? Texto { get; set; }

        public override string ToString()
        {
            return Texto;
        }

        public Nota(int num, string texto)
        {
            Num = num;
            Texto = texto;
        }


    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void ButtonSave(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await RestConsumer.Save(textblock.Text, (combobox.Items.Count + 1));

            }
            catch (Exception err)
            {
                MessageBox.Show($"Error, {err.Message}");
            }
        }

        private async void ButtonDelete(object sender, RoutedEventArgs e)
        {
            try
            {

                Nota cbp = (Nota)combobox.SelectedItem;
                var response = await RestConsumer.Delete(cbp.Num);
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error, {err.Message}");
            }

        }

        private async void ButtonGetAll(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await RestConsumer.GetAll();
                List<Nota> cbp = new List<Nota>();
                for (int i = 0; i < response.Count; i++)
                {
                    var nt = new Nota(response[i].Num, response[i].Texto);
                    cbp.Add(nt);

                }
                combobox.DisplayMemberPath = "Texto";
                combobox.SelectedValuePath = "Num";
                Console.WriteLine(cbp);
                combobox.ItemsSource = cbp;
            }
            catch (Exception err)
            {
                MessageBox.Show($"Error, {err.Message}");
            }
        }


    }
}
