using System;
using System.Windows;

namespace Theme_18_HomeWork
{
    /// <summary>
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window, INewClientView
    {
        NewClientPresenter presenter;
        public string FirstName => tbx_FirstName.Text;

        public string LastName => tbx_LastName.Text;

        public string MidleName => tbx_MidleName.Text;

        public string Documents => tbx_Documents.Text;

        public bool GoodHistory => (bool)cb_GoodHistory.IsChecked;

        public event Action<string, string, string, string, bool> newClientEvent;


        public NewClient()
        {
            InitializeComponent();
            presenter = new NewClientPresenter(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            presenter.AddClient();
        }






    }


}
