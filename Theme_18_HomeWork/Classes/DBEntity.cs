using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Theme_18_HomeWork
{
    class DBEntity
    {
        public MSSQLLocalDemoEntities enticontext;

        public DBEntity()
        {
            enticontext = new MSSQLLocalDemoEntities();
            enticontext.Accaunts.Load();
            enticontext.Clients.Load();
            enticontext.Organisations.Load();
            enticontext.ClientType.Load();
            enticontext.ratesType.Load();
            enticontext.AccauntType.Load();
        }

//        public List<<anonymus type:int,>> Accforview
//        {
//            get
//            {
//                var r = enticontext.Accaunts.
//Join(
//enticontext.AccauntType,
//a => a.TypeId,
//at => at.id,
//(a, at) => new
//{
//    id = a.id,
//    TypeDesc = at.Description,
//    Owner = a.OwnerId,
//    Balans = a.Balans,
//    a.Capitalisation,
//    a.OpenDate,
//    a.Rates,
//    a.EndDate,
//    a.RatesTypeid

//}).Join(
//       enticontext.ratesType,
//        a => a.RatesTypeid,
//        rt => rt.id,
//        (a, rt) => new
//        {
//            id = a.id,
//            a.TypeDesc,
//            a.Owner,
//            a.Balans,
//            a.Capitalisation,
//            a.OpenDate,
//            a.Rates,
//            a.EndDate,
//            RatesType = rt.Description
//        }).Join(
//                enticontext.Organisations.Select(o => new
//                {
//                    id = o.id,
//                    Owner = o.OrganisationName
//                }).Union(
//               enticontext.Clients.Select(c => new
//               {
//                   id = c.id,
//                   Owner = c.FullName
//               })),
//                a => a.Owner,
//                cs => cs.id,
//                (a, cs) => new
//                {
//                    id = a.id,
//                    a.TypeDesc,
//                    cs.Owner,

//                    a.Balans,
//                    a.Capitalisation,
//                    a.OpenDate,
//                    a.Rates,
//                    a.EndDate,
//                    a.RatesType
//                });
//                return r.ToList();
//            }
//        }
    }
}
