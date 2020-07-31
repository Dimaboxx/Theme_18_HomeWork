using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme_18_HomeWork
{ 
    class Presenter
    {
        public int ClientTypeSelection;
        public int ClientSelection;
        public int OrganisationsSelection;
        public int AccauntSelection;
        private IView mainview;
        private selecteditems mainviewselecteditems;




        public Presenter(IView view)
        {
            this.mainview = view;
        }

        public void SelectionChanged(bool SaveClientType, bool SaveClient, bool SaveAccaunt)
        {
            if (SaveClientType)
                mainviewselecteditems.ClientType = mainview.ClientTypeSelectionIndex;
            if (SaveClient)
            {
                if (mainviewselecteditems.ClientType == 0)  // 0 is bad style type Clients
                    mainviewselecteditems.ClientsforView = mainview.ClienSelectionIndex;
                else
                    mainviewselecteditems.OrganisationforView = mainview.OrganisationsSelectionIndex;
            }
            if (SaveAccaunt)
                mainviewselecteditems.AccauntForView = mainview.AccauntSelectionIndex;
        }




    }
}
