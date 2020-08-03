using LogCenterNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Theme_18_HomeWork
{
    class Presenter
    {
        public int ClientTypeSelection;
        public int ClientSelection;
        public int OrganisationsSelection;
        public int AccauntSelection;
        private IViewMain mainview;
        private selecteditems mainviewselecteditems;
        private DBEntity entityDB;
        private LogCenter logCenterDB;
        private WindowsHolder windowsHolder;


        public Presenter(IViewMain view)
        {
            this.mainview = view;
            entityDB = new DBEntity();
            logCenterDB = new LogCenter();
            entityDB.newLogMessage += logCenterDB.AddMessage;
            windowsHolder = new WindowsHolder((Window)view);
            logCenterDB.NewMessage += (r) => { mainview.LogViewRow = r; };
            mainview.xmlLanguage = System.Windows.Markup.XmlLanguage.GetLanguage(System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
        }

        public void Init()
        {
            mainview.ClientTypes = entityDB.enticontext.ClientTypes.ToList();
            mainview.ClientTypeSelectionIndex = 0;
            mainview.clientsforViews = entityDB.ClientsforViews;
            mainview.LogRecords = logCenterDB.records;
        }

        public void ClietTypeSelectionChanged() => loadclients();

        public void ClientsSelectionChange()
        {
            int selectedid = -1;
            if (mainview.ClientSelection != null)
                if (mainviewselecteditems.ClientType == 0)  // 0 is bad style type Client
                {
                    mainviewselecteditems.ClientsforView = mainview.ClienSelectionIndex;

                    selectedid = ((ClientsforView)mainview.ClientSelection).id;
                }
                else
                {
                    mainviewselecteditems.OrganisationforView = mainview.OrganisationsSelectionIndex;
                    selectedid = ((OrganisationforView)mainview.ClientSelection).id;
                }
            mainview.accauntForViews = entityDB.AccauntForViews.Where(e => e.OwnerId == selectedid).ToList();
        }


        public void AddClient()
        {
            if (mainview.ClientTypeSelectionIndex == 0)// 0 is bad style type Client
            {
                var w = windowsHolder.Wnd_newClient;
                w.newClientEvent += entityDB.AddClient;
                w.ShowDialog();
                if (w.DialogResult == true)
                {
                    w = null;
                    mainview.clientsforViews = entityDB.ClientsforViews;
                }
                windowsHolder.Wnd_newClient = null;
            }
            else if (mainview.ClientTypeSelectionIndex == 1) // 1 is bad style type Organisation
            {
                var w = windowsHolder.Wnd_newOrgs;
                w.newOrganisationEvent += entityDB.AddOrganisation;
                w.ShowDialog();
                if (w.DialogResult == true)
                {
                    w = null;
                    mainview.organisationforViews = entityDB.OrganisationforViews;
                }
                windowsHolder.Wnd_newOrgs = null;
            }
        }


        public void AddAccaunt()
        {
            var w = windowsHolder.Wnd_w_newAccaunt;

            w.AccType = entityDB.enticontext.AccauntTypes.ToList();
            w.RateType = entityDB.enticontext.ratesTypes.ToList();

            Accaunt accforadd = new Accaunt();
            if (mainview.ClientSelection != null)
            {
                if (((ClientType)mainview.ClientTypeSelection).id == 0) // 0 is bad style type Client
                    accforadd.OwnerId = ((ClientsforView)(mainview.ClientSelection)).id;
                else
                    accforadd.OwnerId = ((OrganisationforView)(mainview.OrganisationsSelection)).id;
                w.NewACC = accforadd;
                w.ShowDialog();
                if (w.DialogResult == true)
                {
                    int ClientTypeSelectionIndex = mainview.ClientTypeSelectionIndex;
                    int ClientSelectionIndex = mainview.ClienSelectionIndex;
                    entityDB.AddAccaunt(accforadd);
                    loadclients();
                    loadAccs();
                    restoreClentselection();
                }
            }
            windowsHolder.Wnd_w_newAccaunt = null;
        }

        private void loadclients ()
        {
            mainviewselecteditems.ClientType = mainview.ClientTypeSelectionIndex;
            if (mainviewselecteditems.ClientType == 0)  
                mainview.clientsforViews = entityDB.ClientsforViews;
            else
                mainview.organisationforViews = entityDB.OrganisationforViews;
        }

        private void loadAccs()
        {
            mainview.accauntForViews = entityDB.AccauntForViews;
        }

        private void restoreClentselection()
        {
            if (((ClientType)mainview.ClientTypeSelection).id == 0) // 0 is bad style type Client
                mainview.ClienSelectionIndex = mainviewselecteditems.ClientsforView;
            else
                mainview.OrganisationsSelectionIndex = mainviewselecteditems.OrganisationforView;
        }

        public void AddCache() 
        {
            int a = mainview.AccauntSelectionIndex;
            decimal value = (Support.GetUserValue(mainview.CacheBox));
            entityDB.PushMoney(entityDB.enticontext.Accaunts.Find(((AccauntForView)mainview.AccauntSelection).id), value);
            ClientsSelectionChange();
            mainview.AccauntSelectionIndex = a;   
        }

        public void PopCache()
        {
            int a = mainview.AccauntSelectionIndex;

            decimal value = (Support.GetUserValue(mainview.CacheBox));
            entityDB.PopMoney(entityDB.enticontext.Accaunts.Find(((AccauntForView)mainview.AccauntSelection).id), value);
            ClientsSelectionChange();
            mainview.AccauntSelectionIndex = a;
        }


        public void CloseAcc()
        {
            
            if (mainview.AccauntSelection != null)
                entityDB.DeleteAccaunt((AccauntForView)(mainview.AccauntSelection));
            ClientsSelectionChange();
            restoreClentselection();
        }

        public void DelClient()
        {

            if (mainview.ClientSelection != null)
            {
                if (((ClientType)mainview.ClientTypeSelection).id == 0)
                    entityDB.DeleteClient((ClientsforView)(mainview.ClientSelection));
                else if (((ClientType)mainview.ClientTypeSelection).id == 1 )
                    entityDB.DeleteOrgs((OrganisationforView)(mainview.OrganisationsSelection));
                ClietTypeSelectionChanged();
            }
        }




    }
}
