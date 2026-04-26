using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LABA11V2MDK02
{
    public static class CustomerManager
    {
        public static List<Customer> Customers = new List<Customer>();

        public static void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
            Customers.Sort();
        }

        public static List<Customer> GetByCreditCardRange(string min, string max)
        {
            var result = new List<Customer>();
            foreach (var c in Customers)
            {
                if (string.Compare(c.CreditCardNumber, min) >= 0 && string.Compare(c.CreditCardNumber, max) <= 0)
                {
                    result.Add(c);
                }
            }
            return result;
        }

        public static Customer GetByName(string name)
        {
            foreach (var c in Customers)
                if (c.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return c;
            return null;
        }

        public static Customer GetByCardNumber(string cardNumber)
        {
            foreach (var c in Customers)
            {
                if (c.CreditCardNumber.Trim() == cardNumber.Trim())
                {
                    return c;
                }
            }
            return null;
        }

        public static void SaveToFile(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, Customers);
            }
        }

        public static void LoadFromFile(string filename)
        {
            if (!File.Exists(filename)) return;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var loaded = (List<Customer>)formatter.Deserialize(fs);
                Customers.AddRange(loaded);
                Customers.Sort();
            }
        }
    }
}