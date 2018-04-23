using System;
using System.Collections;
using System.Data;

namespace XtraPivotGrid_CustomObjectConverter {
    class Employee : IComparable {
        string fFirstName, fLastName;
        int fAge;
        public Employee(string firstName, string lastName, int age) {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
        public string FirstName {
            get { return fFirstName; }
            set { fFirstName = value; }
        }
        public string LastName {
            get { return fLastName; }
            set { fLastName = value; }
        }
        public int Age {
            get { return fAge; }
            set { fAge = value; }
        }
        public override int GetHashCode() {
            return ToString().GetHashCode();
        }
        public override bool Equals(object obj) {
            return CompareTo(obj) == 0;
        }
        public override string ToString() {
            return FirstName + ' ' + LastName;
        }
        #region IComparable Members
        public int CompareTo(object obj) {
            Employee value = obj as Employee;
            return Comparer.Default.Compare(ToString(), value.ToString());
        }
        #endregion
    }
    class DataHelper {
        public static DataTable GetData() {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("SalesPerson", typeof(Employee));
            dt.Rows.Add("Alice Mutton", 250, new Employee("Janet", "Leverling", 23));
            dt.Rows.Add("Geitost", 120, new Employee("Andrew", "Fuller", 32));
            dt.Rows.Add("Chai", 70, new Employee("Anne", "Dodsworth", 43));
            dt.Rows.Add("Chocolade", 15, new Employee("Andrew", "Fuller", 32));
            dt.Rows.Add("Filo Mix", 9, new Employee("Anne", "Dodsworth", 43));
            dt.Rows.Add("Alice Mutton", 63, new Employee("Andrew", "Fuller", 32));
            dt.Rows.Add("Chai", 99, new Employee("Janet", "Leverling", 23));
            return dt;
        }
    }
}
