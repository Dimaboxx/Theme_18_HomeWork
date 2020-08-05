using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;

namespace Theme_18_HomeWork
{
    class DBEntity : IDisposable
    {
        private static DBEntity instance;

        public MSSQLLocalDemoEntities enticontext;
        public event Action<string> newLogMessage;
        //public event Action ClentsUpdate;
        //public event Action OrgsUpdate;
        //public event Action AccUpdate;
        //public ObservableCollection<AccauntForView> AccauntForViews;
        //public ObservableCollection<ClientsforView> ClientsforViews;
        //public ObservableCollection<OrganisationforView> OrganisationforViews;



        private DBEntity()
        {
            enticontext = new MSSQLLocalDemoEntities();
            enticontext.Accaunts.Load();
            enticontext.Clients.Load();
            enticontext.Organisations.Load();
            enticontext.ClientTypes.Load();
            enticontext.ratesTypes.Load();
            enticontext.AccauntTypes.Load();

            //loadAccauntForViews();
            //loadClientsforViews();
            //loadOrganisationforViews();



        }


         public static DBEntity Instance
        {
            get
        {
              if(instance == null)
                {
                    instance = new DBEntity();
                }
                return instance;
        }
        }

        public List<AccauntForView> AccauntForViews
        {
            get
            {
                // private void loadAccauntForViews()
                {

                    var r = enticontext.Accaunts.
                    Join(
                    enticontext.AccauntTypes,
                    a => a.TypeId,
                    at => at.id,
                    (a, at) => new
                    {
                        id = a.id,
                        TypeDesc = at.Description,
                        a.OwnerId,
                        Balans = a.Balans,
                        a.Capitalisation,
                        a.OpenDate,
                        a.Rates,
                        a.EndDate,
                        a.RatesTypeid

                    }).Join(
                       enticontext.ratesTypes,
                        a => a.RatesTypeid,
                        rt => rt.id,
                        (a, rt) => new
                        {
                            id = a.id,
                            a.TypeDesc,
                            a.OwnerId,
                            a.Balans,
                            a.Capitalisation,
                            a.OpenDate,
                            a.Rates,
                            a.EndDate,
                            RatesType = rt.Description
                        }).Join(
                                enticontext.Organisations.Select(o => new
                                {
                                    id = o.id,
                                    Owner = o.OrganisationName
                                }).Union(
                               enticontext.Clients.Select(c => new
                               {
                                   id = c.id,
                                   Owner = c.FullName
                               })),
                                a => a.OwnerId,
                                cs => cs.id,
                                (a, cs) => new AccauntForView
                                {
                                    id = a.id,
                                    TypeDesc = a.TypeDesc,
                                    Owner = cs.Owner,
                                    OwnerId = a.OwnerId,
                                    Balans = a.Balans,
                                    Capitalisation = a.Capitalisation,
                                    OpenDate = a.OpenDate,
                                    Rates = a.Rates,
                                    EndDate = a.EndDate,
                                    RatesType = a.RatesType
                                });

                    return r.ToList();
                    //AccauntForViews = r.ToList();
                }
            }
        }

        // private void loadClientsforViews()
        public List<ClientsforView> ClientsforViews
        {
            get
            {
                {

                    //            dtg_Clients.DataContext = mSSqlRep.dt_clients.DefaultView;
                    #region GetClients
                    var ClientsResultQuery = enticontext.Clients.Local.Join(
                              enticontext.ClientTypes,
                              cl => cl.ClientTypeId,
                              clt => clt.id,
                              (cl, clt) => new
                              {
                                  id = cl.id,
                                  ClientType = clt.Description,
                                  FirstName = cl.FirstName,
                                  MidleName = cl.MidleName,
                                  LastName = cl.LastName,
                                  Documents = cl.Documents,
                                  GoodHistory = cl.GoodHistory
                              }).GroupJoin(
                    //enticontext.Accaunts.GroupBy(a => a.OwnerId).SelectMany(g => new { g.Key, Count = g.Count() }).DefaultIfEmpty(),
                    enticontext.Accaunts,
                    cl => cl.id,
                    a => a.OwnerId,
                    (cl, ag) => new ClientsforView
                    {

                        id = cl.id,
                        ClientType = cl.ClientType,
                        FirstName = cl.FirstName,
                        MidleName = cl.MidleName,
                        LastName = cl.LastName,
                        Documents = cl.Documents,
                        GoodHistory = cl.GoodHistory,
                        AccauntCounts = ag.Select(a => a.id).Count()

                    });
                    #endregion
                    return ClientsResultQuery.ToList();

                }
            }
        }

        //    private void loadOrganisationforViews()
        public List<OrganisationforView> OrganisationforViews
        {
            get
            {
                var OrganisationResultQuery = enticontext.Organisations.Local.Join(
                enticontext.ClientTypes,
                o => o.ClientTypeId,
                ct => ct.id,
                (o, ct) => new 
                {
                    id = o.id,
                    ClientType = ct.Description,
                    OrganisationName = o.OrganisationName,
                    BankDetails = o.BankDetails,
                    Adress = o.Adress,
                    o.GoodHistory
                }).GroupJoin(
                    //enticontext.Accaunts.GroupBy(a => a.OwnerId).SelectMany(g => new { g.Key, Count = g.Count() }).DefaultIfEmpty(),
                    enticontext.Accaunts,
                    org => org.id,
                    acc => acc.OwnerId,
                    (o, ag) => new OrganisationforView
                    {
                        id = o.id,
                        ClientType = o.ClientType,
                        OrganisationName = o.OrganisationName,
                        BankDetails = o.BankDetails,
                        Adress = o.Adress,
                        GoodHistory = o.GoodHistory,
                        AccauntCounts = ag.Select(a => a.id).Count()

                    });
                return OrganisationResultQuery.ToList();
            }
        }

        public void AddClient(string FirstName, string MidleName, string LastName, string Documents, bool GoodHistory)
        {
            Client nc = new Client()
            {
                id = nextClientId(),
                FirstName = FirstName,
                MidleName = MidleName,
                LastName = LastName,
                Documents = Documents,
                GoodHistory = GoodHistory,
                ClientTypeId = 0
            };

            enticontext.Clients.Add(nc);
            if (enticontext.SaveChanges() > 0)
            {

                newLogMessage?.Invoke($"Добавлен пользоваетль клиент {nc.id} : " +
                   $"{FirstName}," +
                   $",MidleName : " +
                   $"{MidleName}'," +
                   $"LastName : " +
                   $"{LastName}', " +
                   $"Documents : " +
                   $"{Documents}'," +
                   $"GoodHistory :  " +
                   $"'{ GoodHistory}')");
            }
        }

        private int nextClientId()
        {
            return enticontext.Clients.Select(e => e.id).Union(enticontext.Organisations.Select(e => e.id)).Max() + 1;

        }
        private int nextAccauntId()
        {
            return enticontext.Accaunts.Select(e => e.id).Max() + 1;

        }

        public void AddOrganisation(string OrganisationName, string BankDetails, string Adress, bool GoodHistory)
        {
            Organisation neworganisation = new Organisation
            {
                id = nextClientId(),
                OrganisationName = OrganisationName,
                BankDetails = BankDetails,
                Adress = Adress,
                GoodHistory = GoodHistory,
                ClientTypeId = 1
            };

            enticontext.Organisations.Add(neworganisation);
            //enticontext.SaveChanges();

            if (enticontext.SaveChanges() > 0)
            {
                newLogMessage?.Invoke($"" +
                    $"Добавлена Организация :  {neworganisation.id} " +
                   $"{OrganisationName}," +
                    $"BankDetails : " +
                   $"{BankDetails}," +
                   $"Adress : " +
                   $"{Adress}," +
                   $"GoodHistory :  " +
                   $"'{ GoodHistory}')");
            }

        }

        public void AddAccaunt(DateTime OpenDate, DateTime CloseDate, int TypeId, float Rates, int OwnerId, bool Capitalisation, int RatesTypeid)
        {
            Accaunt accaunt = new Accaunt
            {
                OpenDate = OpenDate,
                EndDate = CloseDate,
                TypeId = TypeId,
                Rates = Rates,
                RatesTypeid = RatesTypeid,
                OwnerId = OwnerId,
                Capitalisation = Capitalisation

            };
            AddAccaunt(accaunt);
        }

        public void AddAccaunt(Accaunt accaunt)
        {
            accaunt.id = nextAccauntId();

            enticontext.Accaunts.Add(accaunt);
            if (enticontext.SaveChanges() > 0)
            {
                string mess = $" " +
                $"Добавлена счет : {accaunt.id} " +
               $"открыт : {accaunt.OpenDate}," +
                $"TypeId : " +
               $"{accaunt.TypeId}," +
               $"Rates : " +
               $"{accaunt.Rates}," +
               $"Capitalisation : " +
               $"{accaunt.Capitalisation}," +
               $"RatesTypeid : " +
               $"{accaunt.RatesTypeid}," +
               $"OwnerId :  " +
               $"'{ accaunt.OwnerId}'";

                if (accaunt.EndDate != null)
                {
                    mess += $",EndDate :  " +
               $"'{ accaunt.EndDate}'";
                }
                newLogMessage?.Invoke(mess);

            }
        }

        public void DeleteClient(ClientsforView client)
        {

            var q = enticontext.Accaunts.GroupBy(a => a.OwnerId).Where(e => e.Key == client.id).Select(r => r.Count());
            q.Load();

            if (q.FirstOrDefault() > 0)
                MessageBox.Show("Не возможно удалить клиента при наличии открытых счетов");
            else
            {
                Client cd = enticontext.Clients.Where(c => c.id == client.id).FirstOrDefault();
                string smess = $"удален пользоваетль клиент {cd.id} : " +
$"{cd.FirstName}," +
$",MidleName : " +
$"{cd.MidleName}'," +
$"LastName : " +
$"{cd.LastName}', " +
$"Documents : " +
$"{cd.Documents}'," +
$"GoodHistory :  " +
$"'{ cd.GoodHistory}')";
                enticontext.Entry(cd).State = EntityState.Deleted;
                if (enticontext.SaveChanges() > 0)
                {
                    ClientsforViews.Remove(client);
                    newLogMessage?.Invoke(smess);
                }
                //enticontext.Clients.Remove(client);
            }
        }

        public void DeleteOrgs(OrganisationforView client)
        {

            var q = enticontext.Accaunts.GroupBy(a => a.OwnerId).Where(e => e.Key == client.id).Select(r => r.Count());
            q.Load();

            if (q.FirstOrDefault() > 0)
                MessageBox.Show("Не возможно удалить клиента при наличии открытых счетов");
            else
            {
                Organisation od = enticontext.Organisations.Where(c => c.id == client.id).FirstOrDefault();
                string smess = $"удален пользоваетль организация {od.id} : " +
                   $"{od.OrganisationName}," +
                    $"BankDetails : " +
                   $"{od.BankDetails}," +
                   $"Adress : " +
                   $"{od.Adress}," +
                   $"GoodHistory :  " +
                   $"'{ od.GoodHistory}')";
                enticontext.Entry(od).State = EntityState.Deleted;
                if (enticontext.SaveChanges() > 0)
                {
                    OrganisationforViews.Remove(client);
                    newLogMessage?.Invoke(smess);
                }
                //enticontext.Clients.Remove(client);
            }
        }




















        public void DeleteAccaunt(AccauntForView accauntForView)
        {

            if (accauntForView.Balans > 0)
                MessageBox.Show("Не возможно удалить счет при наличии остатка средств");
            else
            {
                Accaunt acd = enticontext.Accaunts.Where(a => a.id == accauntForView.id).FirstOrDefault();
                string mess = $" " +
                                $"Удален счет : {acd.id} " +
                               $"открыт : {acd.OpenDate}," +
                                $"TypeId : " +
                               $"{acd.TypeId}," +
                               $"Rates : " +
                               $"{acd.Rates}," +
                               $"Capitalisation : " +
                               $"{acd.Capitalisation}," +
                               $"RatesTypeid : " +
                               $"{acd.RatesTypeid}," +
                               $"OwnerId :  " +
                               $"'{ acd.OwnerId}'";

                if (acd.EndDate != null)
                {
                    mess += $",EndDate :  " +
               $"'{ acd.EndDate}'";
                    enticontext.Entry(acd).State = EntityState.Deleted;
                    if (enticontext.SaveChanges() > 0)
                    {
                        AccauntForViews.Remove(accauntForView);
                        newLogMessage?.Invoke(mess);
                    }
                    //enticontext.Clients.Remove(client);
                }
            }
        }

        public void PopMoney(Accaunt accaunt, decimal money)
        {
            string mess;
            if (accaunt.Balans >= money)
            {
                accaunt.Balans -= money;
                mess = $"со счета {accaunt.id} списано: {money} ";
            }
            else
            {
                mess = $"На счету {accaunt.id} недостаточно средств. \n доступно : {accaunt.Balans}  запрошено: {money} ";

            }
            MessageBox.Show(mess);
            newLogMessage?.Invoke(mess);
            enticontext.SaveChanges();

        }

        public void PushMoney(Accaunt accaunt, decimal money)
        {
            if(money > 0)
            {
            accaunt.Balans += money;
                if( enticontext.SaveChanges() > 0)
                {
            string mess = $"На счет {accaunt.id} зачислены {money}";
            MessageBox.Show(mess);
            newLogMessage?.Invoke(mess);
                }
            }

        }

        public void Dispose()
        {
            enticontext.Dispose();
            throw new NotImplementedException();
        }
    }
}
