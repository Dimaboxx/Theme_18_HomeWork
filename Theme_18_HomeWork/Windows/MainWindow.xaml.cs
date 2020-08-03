using LogCenterNameSpace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Theme_18_HomeWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewMain
    {

        Presenter presenter;

        public List<ClientType> ClientTypes { set => cbbx_ClientType.ItemsSource = value; }
        public List<AccauntForView> accauntForViews { set => dtg_Accaunts.ItemsSource = value; }
        public List<ClientsforView> clientsforViews { set => dtg_Clients.ItemsSource = value; }
        public List<OrganisationforView> organisationforViews { set => dtg_Clients.ItemsSource = value; }
        public object ClientTypeSelection => cbbx_ClientType.SelectedItem;
        public object ClientSelection => dtg_Clients.SelectedItem;
        public object OrganisationsSelection => dtg_Clients.SelectedItem;
        public object AccauntSelection => dtg_Accaunts.SelectedItem;
        public int ClientTypeSelectionIndex { get => cbbx_ClientType.SelectedIndex; set => cbbx_ClientType.SelectedIndex = value; }
        public int ClienSelectionIndex { get => dtg_Clients.SelectedIndex; set => dtg_Clients.SelectedIndex = value; }
        public int OrganisationsSelectionIndex { get => dtg_Clients.SelectedIndex; set => dtg_Clients.SelectedIndex = value; }
        public int AccauntSelectionIndex { get => dtg_Accaunts.SelectedIndex; set => dtg_Accaunts.SelectedIndex = value; }
        public string CacheBox => txtbx_CashValue.Text;
        public ObservableCollection<LogRecord> LogRecords { set => lst_log.ItemsSource = value; }
        public LogRecord LogViewRow { set => lst_log.ScrollIntoView(value); }
        public XmlLanguage xmlLanguage { set => this.Language = value; }
        public MainWindow()
        {
            InitializeComponent();
            presenter = new Presenter(this);
            presenter.Init();
        }
        private void dtg_Clients_SelectionChanged(object sender, SelectionChangedEventArgs e) => presenter.ClientsSelectionChange();
        private void cbbx_ClientType_SelectionChanged(object sender, SelectionChangedEventArgs e) =>  presenter.ClietTypeSelectionChanged();
        private void Btn_addClient_Click(object sender, RoutedEventArgs e) => presenter.AddClient();
        private void Btn_addacc_Click(object sender, RoutedEventArgs e) => presenter.AddAccaunt();
        private void Btn_AddCache_Click(object sender, RoutedEventArgs e) => presenter.AddCache();
        private void Btn_PopCache_Click(object sender, RoutedEventArgs e) => presenter.PopCache();

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    cbx_ClientType.SelectedIndex = -1;
        //}

        private void Btn_CloseAcc_Click(object sender, RoutedEventArgs e) => presenter.CloseAcc();
        private void Btn_DellClient_Click(object sender, RoutedEventArgs e) => presenter.DelClient();

    }
}
