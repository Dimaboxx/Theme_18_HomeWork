using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Theme_18_HomeWork
{
    class NewClientPresenter
    {
        DBEntity entity;
        INewClientView view;
        public NewClientPresenter(INewClientView newClientView)
        {
              view = newClientView;
            entity = DBEntity.Instance;
        }

        public void AddClient()
        {
            if (!String.IsNullOrWhiteSpace(view.FirstName) &&
                !String.IsNullOrWhiteSpace(view.LastName) &&
                !String.IsNullOrWhiteSpace(view.Documents))
                entity.AddClient(view.FirstName, view.MidleName, view.LastName, view.Documents, view.GoodHistory);
            else
                MessageBox.Show("FirstName, LastName или Documents не могут быть пустым!", "Обнаружено пустое поле", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
