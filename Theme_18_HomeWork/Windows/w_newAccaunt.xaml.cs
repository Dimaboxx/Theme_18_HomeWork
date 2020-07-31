using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace Theme_18_HomeWork
{
    /// <summary>
    /// Interaction logic for w_newAccaunt.xaml
    /// </summary>
    public partial class w_newAccaunt : Window
    {
        //  public int OwnerId { get; set; }
        public List<AccauntType> AccType
        {
            get
            {
                return null;
            }
            set
            {
                cbbx_acctype.ItemsSource = value;

            }
        }
        public List<ratesType> RateType
        {
            get
            {
                return null;
            }
            set
            {
                cbbx_ratetype.ItemsSource = value;

            }
        }
        //public DataTable Clients
        //{
        //    get
        //    {
        //        return null;
        //    }
        //    set
        //    {
        //        cbbx_Owner.DataContext = value;

        //    }
        //}


        public Accaunt NewACC { get; set; }

        public w_newAccaunt()
        {
            InitializeComponent();

            dp_enddate.BlackoutDates.AddDatesInPast();
            dp_enddate.SelectedDate = DateTime.Now.AddMonths(12);
        }
        //public w_newAccaunt(DataRow row) : this()
        //{

        //}



        private void btn_openAcc_Click(object sender, RoutedEventArgs e)
        {
            if (cbbx_acctype.SelectedItem == null)
            {
                MessageBox.Show("Требуется выбрать тип счета");
            }
            else
            {
                if (cbbx_ratetype.SelectedItem == null)
                {
                    MessageBox.Show("Требуется выбрать тип начисления процентов");

                }
                else
                {
                    //NewACCrow["OwnerId"] = (int)((DataRowView)cbbx_Owner.SelectedItem)["id"];
                    NewACC.TypeId = ((AccauntType)cbbx_acctype.SelectedItem).id;
                    NewACC.OpenDate = DateTime.Now;
                    NewACC.EndDate = (DateTime)dp_enddate.SelectedDate;
                    NewACC.Rates = 6;
                    NewACC.RatesTypeid = ((ratesType)cbbx_ratetype.SelectedItem).id;
                    NewACC.Capitalisation = (bool)cbx_Capitalisation.IsChecked;
                    this.DialogResult = !false;
                    this.Close();
                }
            }



            //AccauntsType accauntsType = new AccauntsType();
            //Accaunt accaunt ;

            //accauntsType = (bool)rb_AccSimple.IsChecked ? AccauntsType.Simple : (bool)rb_AccCridit.IsChecked ? AccauntsType.Credit : AccauntsType.Deposite;
            //if ((bool)rb_AccSimple.IsChecked)
            //{
            //    accaunt = new Accaunt(AccauntsType.Simple, BankClient);
            //}
            //else if((bool)rb_AccCridit.IsChecked)
            //{
            //    accaunt = new Accaunt(AccauntsType.Credit, BankClient)
            //    {
            //        EndDate = (DateTime)dp_enddate.SelectedDate
            //    };

            //}
            //else
            //{
            //    accaunt = new Accaunt(AccauntsType.Deposite, BankClient);
            //    if (dp_enddate.SelectedDate != null)
            //    {
            //        accaunt.EndDate = (DateTime)dp_enddate.SelectedDate;
            //    }
            //    else
            //    {
            //        accaunt.EndDate = DateTime.Now.AddMonths(12);
            //    }

            //    accaunt.Capitalization = (bool)cbx_Capitalisation.IsChecked;
            //    accaunt.ratesType = (bool)rb_RatesZero.IsChecked ? RatesPeriods.Zero :
            //                        (bool)rb_RatesDay.IsChecked ? RatesPeriods.Daily :
            //                        (bool)rb_RatesMonth.IsChecked ? RatesPeriods.Mouth :
            //                        (bool)rb_RatesYear.IsChecked ? RatesPeriods.Year : RatesPeriods.End;



            //}
            //BankClient.AddAccaunt(accaunt);
        }

        private void dp_enddate_MouseEnter(object sender, MouseEventArgs e)
        {
            dp_enddate.IsDropDownOpen = true;
        }


    }
}
