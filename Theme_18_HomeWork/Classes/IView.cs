using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theme_18_HomeWork
{
    interface IView
    {
        List<AccauntForView> accauntForViews { set; }

        List<ClientsforView> clientsforViews { set; }

        List<OrganisationforView> organisationforViews { set; }

        //int ClientTypeSelection { get; }
        int ClientTypeSelectionIndex { get; set; }
        //int ClientSelection { get; }
        int ClienSelectionIndex { get; set; }
        //int OrganisationsSelection { get; }
        int OrganisationsSelectionIndex { get; set; }
        //int AccauntSelection { get; }
        int AccauntSelectionIndex { get; set; }


    }
}
