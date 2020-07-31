using System;
using System.Windows;

namespace Theme_18_HomeWork
{
    /// <summary>
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewOrgs : Window
    {
        //       public SqlConnection con { get; set; }
        //public DataTable accTypes
        //{
        //    get { return null; }
        //    set
        //    {
        //        cbx_ClientType.DataContext = value.DefaultView;
        //    }
        //}







        public event Action<string, string, string, bool> newOrganisationEvent;
        public NewOrgs()
        {

            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //MainWindow.wnd_newClient = null;
            //this.DialogResult = false;
        }



        private void Button_AddOrganisationClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (!String.IsNullOrWhiteSpace(tbx_OrganistionName.Text) && !String.IsNullOrWhiteSpace(tbx_BankDetails.Text) && !String.IsNullOrWhiteSpace(tbx_Adress.Text))
            {
                newOrganisationEvent?.Invoke(
                    tbx_OrganistionName.Text,
                    tbx_BankDetails.Text,
                    tbx_Adress.Text,
                    (bool)(cb_GoodHistory.IsChecked));
                this.Close();
            }
            else
                MessageBox.Show("Имя организации,Реквизиты и адресс не могут быть пустыми!", "Обнаружено пустое поле", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


}
