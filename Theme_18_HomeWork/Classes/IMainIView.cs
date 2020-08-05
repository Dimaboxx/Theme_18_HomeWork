using LogCenterNameSpace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme_18_HomeWork
{
    interface IViewMain
    {
        List<AccauntForView> accauntForViews { set; }

        List<ClientsforView> clientsforViews { set; }

        List<OrganisationforView> organisationforViews { set; }
        List<ClientType> ClientTypes { set; }

        ObservableCollection<LogRecord> LogRecords {set;}
        
        object ClientTypeSelection { get; }
        int ClientTypeSelectionIndex { get; set; }
        object ClientSelection { get; }
        int ClienSelectionIndex { get; set; }
        object OrganisationsSelection { get; }
        int OrganisationsSelectionIndex { get; set; }
        object AccauntSelection { get; }
        int AccauntSelectionIndex { get; set; }

        LogRecord LogViewRow { set; }

        System.Windows.Markup.XmlLanguage xmlLanguage { set; }

        string CacheBox { get; }


    }
}
