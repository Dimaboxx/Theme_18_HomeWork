using LogCenterNameSpace;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Theme_18_HomeWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public static string DateTimeformat;
        public static string Timeformat;
        public static string Dateformat;
        public static NewClient wnd_newClient;
        public static NewOrgs wnd_newOrgs;
        public static w_newAccaunt wnd_w_newAccaunt;
        LogCenter logCenter;
        DBEntity edb;
        selecteditems curselecteditems;
        Presenter presenter;

        public List<AccauntForView> accauntForViews { set => dtg_Accaunts.ItemsSource = value; }
        public List<ClientsforView> clientsforViews { set => dtg_Clients.ItemsSource = value; }
        public List<OrganisationforView> organisationforViews { set => dtg_Clients.ItemsSource = value; }
        public int ClientTypeSelectionIndex { get => cbbx_ClientType.SelectedIndex; set => cbbx_ClientType.SelectedIndex = value; }
        public int ClienSelectionIndex { get => dtg_Clients.SelectedIndex; set => dtg_Clients.SelectedIndex = value; }
        public int OrganisationsSelectionIndex { get => dtg_Clients.SelectedIndex; set => dtg_Clients.SelectedIndex = value; }
        public int AccauntSelectionIndex { get => dtg_Accaunts.SelectedIndex; set => dtg_Accaunts.SelectedIndex = value; }

        static MainWindow()
        {
            Timeformat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.LongTimePattern;
            Dateformat = System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern;
            DateTimeformat = $"{Dateformat} {Timeformat}";
        }


        public MainWindow()
        {

            InitializeComponent();
            init_0();

            presenter = new Presenter(this);

        }



        void init_0 ()
        {
            logCenter  = new LogCenter();
            logCenter.AddMessage($"Репозитарий создан ");
            edb = new DBEntity();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
            edb.newLogMessage += scrollLogView;
            edb.newLogMessage += logCenter.AddMessage;
            lst_log.ItemsSource = logCenter.records;
            cbbx_ClientType.ItemsSource = edb.enticontext.ClientTypes.Local;
            cbbx_ClientType.SelectedIndex = 0;
            load_items();
        }



        void load_items()
        {
            if (((ClientType)cbbx_ClientType.SelectedItem).id == 0)
                dtg_Clients.ItemsSource = edb.ClientsforViews;
            else
                dtg_Clients.ItemsSource = edb.OrganisationforViews;
                
            dtg_Accaunts.ItemsSource = edb.AccauntForViews;
        }

        void saveselection(bool SaveClientType,bool SaveClient,  bool SaveAccaunt)
        {
            if(SaveClientType)
                curselecteditems.ClientType = cbbx_ClientType.SelectedIndex;
            if (SaveClient)
            {
                if(((ClientType)cbbx_ClientType.SelectedItem).id == 0)
                    curselecteditems.ClientsforView = dtg_Clients.SelectedIndex;
                else
                    curselecteditems.OrganisationforView = dtg_Clients.SelectedIndex;
            }
            if(SaveAccaunt)
                curselecteditems.AccauntForView = dtg_Accaunts.SelectedIndex;

        }
        void restoreselection(bool restoreClientType, bool restoreClient, bool restoreAccaunt)
        {
            if (restoreClientType)
                cbbx_ClientType.SelectedIndex = curselecteditems.ClientType ;

            if (restoreClient)
            {
                if (((ClientType)cbbx_ClientType.SelectedItem).id == 0)
                    dtg_Clients.SelectedIndex = curselecteditems.ClientsforView; 
                else
                    dtg_Clients.SelectedIndex = curselecteditems.OrganisationforView;
            }
            if (restoreAccaunt)
                dtg_Accaunts.SelectedIndex = curselecteditems.AccauntForView ;

        }

        private void scrollLogView(string s)
        {
            if (lst_log.Items.Count > 0)
            {
                lst_log.ScrollIntoView(lst_log.Items[lst_log.Items.Count - 1]);
            }
        }

        private void Btn_addClient_Click(object sender, RoutedEventArgs e)
        {

            if (((ClientType)(cbbx_ClientType.SelectedItem)).id == 0)
            {
                if (wnd_newClient == null)
                {
                    wnd_newClient = new NewClient();
                    wnd_newClient.newClientEvent += edb.AddClient;
                    wnd_newClient.Owner = this;
                }
                wnd_newClient.ShowDialog();
                if (wnd_newClient.DialogResult == true)
                {
                    dtg_Clients.ItemsSource = edb.ClientsforViews;
                }
                wnd_newClient = null;
            }
            else
            {
                if (wnd_newOrgs == null)
                {
                    wnd_newOrgs = new NewOrgs();
                    wnd_newOrgs.newOrganisationEvent += edb.AddOrganisation;
                    wnd_newOrgs.Owner = this;
                }
                wnd_newOrgs.ShowDialog();
                if (wnd_newOrgs.DialogResult == true)
                {
                    dtg_Clients.ItemsSource = edb.OrganisationforViews;
                }
                wnd_newOrgs = null;




            }





        }

        private void Btn_addacc_Click(object sender, RoutedEventArgs e)
        {
            if (wnd_w_newAccaunt == null)
            {
                wnd_w_newAccaunt = new w_newAccaunt();
            }
            wnd_w_newAccaunt.AccType = edb.enticontext.AccauntTypes.ToList() ;
            wnd_w_newAccaunt.RateType =  edb.enticontext.ratesTypes.ToList();

            Accaunt accforadd = new Accaunt();
            var id = dtg_Clients.SelectedIndex;
            if (dtg_Clients.SelectedItem != null)
            {
                if(((ClientType)cbbx_ClientType.SelectedItem).id == 0)
                    accforadd.OwnerId = ((ClientsforView)(dtg_Clients.SelectedItem)).id;
                else
                    accforadd.OwnerId = ((OrganisationforView)(dtg_Clients.SelectedItem)).id;
                wnd_w_newAccaunt.NewACC = accforadd;
                wnd_w_newAccaunt.ShowDialog();
                if (wnd_w_newAccaunt.DialogResult == true)
                {
                    saveselection(true, true, false);
                    edb.AddAccaunt(accforadd);
                    load_items();
                    restoreselection(false, true, false);
                    
                }
            }
            wnd_w_newAccaunt = null;
        }



        private void Btn_AddCache_Click(object sender, RoutedEventArgs e)
        {
            saveselection(true, true, true);
            decimal value = (Support.GetUserValue(txtbx_CashValue.Text));
            edb.PushMoney(edb.enticontext.Accaunts.Find(((AccauntForView)dtg_Accaunts.SelectedItem).id), value);
            load_items();
            restoreselection(false, true, false);
        }

        private void Btn_PopCache_Click(object sender, RoutedEventArgs e)
        {
            saveselection(true, true, true);
            decimal value = (Support.GetUserValue(txtbx_CashValue.Text));
            edb.PopMoney(edb.enticontext.Accaunts.Find(((AccauntForView)dtg_Accaunts.SelectedItem).id), value);
            load_items();
            restoreselection(false, true, false);
        }


        private void dtg_Clients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtg_Clients.SelectedItem != null)
                if (((ClientType)(cbbx_ClientType.SelectedItem)).id == 0)
                {
                    dtg_Accaunts.ItemsSource = edb.AccauntForViews.Where(a => a.OwnerId == ((ClientsforView)(dtg_Clients.SelectedItem)).id);
                }
                else
                {
                    dtg_Accaunts.ItemsSource = edb.AccauntForViews.Where(a => a.OwnerId == ((OrganisationforView)(dtg_Clients.SelectedItem)).id);
                }

            else
                dtg_Accaunts.ItemsSource = null; ;
        }

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    cbx_ClientType.SelectedIndex = -1;
        //}

        private void Btn_CloseAcc_Click(object sender, RoutedEventArgs e)
        {
            saveselection(true, true, false);
            if (dtg_Accaunts.SelectedItem != null)
                edb.DeleteAccaunt((AccauntForView)(dtg_Accaunts.SelectedItem));
            load_items();
            restoreselection(false, true, false);
        }

        private void cbbx_ClientType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((int)((ClientType)cbbx_ClientType.SelectedItem).id) == 0)
            {
                dtg_Clients.ItemsSource = edb.ClientsforViews;
            }
            else
                dtg_Clients.ItemsSource = edb.OrganisationforViews;
        }

        private void Btn_DellClient_Click(object sender, RoutedEventArgs e)
        {

            if (dtg_Clients.SelectedItem != null)
            {
                //if (((ClientType)cbbx_ClientType.SelectedItem).id == 0)
                    edb.DeleteClient((ClientsforView)(dtg_Clients.SelectedItem));
                load_items();
            }
        }
    }
}
