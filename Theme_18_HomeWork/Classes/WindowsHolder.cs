using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Theme_18_HomeWork
{
    public class WindowsHolder
    {
        NewClient wnd_newClient;
        NewOrgs wnd_newOrgs;
        w_newAccaunt wnd_w_newAccaunt;
        Window mainwindow;



        public WindowsHolder(Window window)
        {
            mainwindow = window;
        }

        public NewClient Wnd_newClient
        {
            get
            {
                if (wnd_newClient == null)
                {
                    wnd_newClient = new NewClient();
                    wnd_newClient.Owner = mainwindow;
                }
                    return wnd_newClient;
            }

            set { wnd_newClient = value; }
        }


        public NewOrgs Wnd_newOrgs
        {
            get
            {
                if (wnd_newOrgs == null)
                {
                    wnd_newOrgs = new NewOrgs();
                    wnd_newOrgs.Owner = mainwindow;
                }
                return wnd_newOrgs;
            }

            set { wnd_newOrgs = value; }
        }
         public w_newAccaunt Wnd_w_newAccaunt
        {
            get
            {
                if (wnd_w_newAccaunt == null)
                {
                    wnd_w_newAccaunt = new w_newAccaunt();
                    wnd_w_newAccaunt.Owner = mainwindow;
                }
                return wnd_w_newAccaunt;
            }

            set { wnd_w_newAccaunt = value; }
        }


    }
}
