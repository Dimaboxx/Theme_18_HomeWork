using System;
using System.Windows;

namespace Theme_18_HomeWork
{
    /// <summary>
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {

        public event Action<string, string, string, string, bool> newClientEvent;
        public NewClient()
        {

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //SqlCommand c = new SqlCommand();
            if (!String.IsNullOrWhiteSpace(tbx_FirstName.Text) && !String.IsNullOrWhiteSpace(tbx_LastName.Text) && !String.IsNullOrWhiteSpace(tbx_Documents.Text))
            {
                newClientEvent?.Invoke(
                    tbx_FirstName.Text,
                    tbx_MidleName.Text,
                    tbx_LastName.Text,
                    tbx_Documents.Text,
                    (bool)(cb_GoodHistory.IsChecked));
                this.Close();
            }
            else
            {
                MessageBox.Show("FirstName и LastName не могут быть пустым!", "Обнаружено пустое поле", MessageBoxButton.OK, MessageBoxImage.Error);


            }


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MainWindow.wnd_newClient = null;
            //this.DialogResult = false;
        }




    }


}
