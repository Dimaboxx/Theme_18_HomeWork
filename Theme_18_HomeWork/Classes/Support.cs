using System.Windows;

namespace Theme_18_HomeWork
{
    static class Support
    {
        //public static Support()
        //{

        //}


        public static decimal GetUserValue(string stringunput)
        {
            decimal value = 0;
            if (!decimal.TryParse(stringunput, out value))
            {
                MessageBox.Show($"Введенное число некоректно: {stringunput}\n " +
                $"в системе установлен разделитель длобной и целой части" +
                $" \"{System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator}\""
                );
                return 0;
            }
            else if (value < 0)
            {
                MessageBox.Show("Введено отрицательное число .");
                return 0;
            }
            else
                return value;
        }
    }


    public struct selecteditems
    {

        public int ClientType { get; set; }
        public int ClientsforView { get; set; }
        public int OrganisationforView { get; set; }
        public int AccauntForView { get; set; }
    }

    //public struct selecteditems
    //{

    //    public ClientType ClientType { get; set; }
    //    public ClientsforView ClientsforView { get; set; }
    //    public OrganisationforView OrganisationforView { get; set; }
    //    public AccauntForView AccauntForView { get; set; }
    //}
}
