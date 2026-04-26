using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA11V2MDK02
{

    [Serializable]
    public class Customer : IComparable<Customer>
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public string CreditCardNumber { get; set; }
        public string BankAccountNumber { get; set; }

        public int CompareTo(Customer other)
        {
            return FirstName.CompareTo(other.FirstName);
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {Patronymic}, Адрес: {Address}, Карта: {CreditCardNumber}, Счёт: {BankAccountNumber}";
        }
    }
}