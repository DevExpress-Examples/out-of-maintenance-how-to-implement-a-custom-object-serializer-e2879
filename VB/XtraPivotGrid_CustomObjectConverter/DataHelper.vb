Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Data

Namespace XtraPivotGrid_CustomObjectConverter
	Friend Class Employee
		Implements IComparable
		Private fFirstName, fLastName As String
		Private fAge As Integer
		Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal age As Integer)
			Me.FirstName = firstName
			Me.LastName = lastName
			Me.Age = age
		End Sub
		Public Property FirstName() As String
			Get
				Return fFirstName
			End Get
			Set(ByVal value As String)
				fFirstName = value
			End Set
		End Property
		Public Property LastName() As String
			Get
				Return fLastName
			End Get
			Set(ByVal value As String)
				fLastName = value
			End Set
		End Property
		Public Property Age() As Integer
			Get
				Return fAge
			End Get
			Set(ByVal value As Integer)
				fAge = value
			End Set
		End Property
		Public Overrides Function GetHashCode() As Integer
			Return ToString().GetHashCode()
		End Function
		Public Overrides Overloads Function Equals(ByVal obj As Object) As Boolean
			Return CompareTo(obj) = 0
		End Function
		Public Overrides Function ToString() As String
			Return FirstName & " "c + LastName
		End Function
		#Region "IComparable Members"
		Public Function CompareTo(ByVal obj As Object) As Integer Implements IComparable.CompareTo
			Dim value As Employee = TryCast(obj, Employee)
			Return Comparer.Default.Compare(ToString(), value.ToString())
		End Function
		#End Region
	End Class
	Friend Class DataHelper
		Public Shared Function GetData() As DataTable
			Dim dt As New DataTable()
			dt.Columns.Add("ProductName", GetType(String))
			dt.Columns.Add("Quantity", GetType(Integer))
			dt.Columns.Add("SalesPerson", GetType(Employee))
			dt.Rows.Add("Alice Mutton", 250, New Employee("Janet", "Leverling", 23))
			dt.Rows.Add("Geitost", 120, New Employee("Andrew", "Fuller", 32))
			dt.Rows.Add("Chai", 70, New Employee("Anne", "Dodsworth", 43))
			dt.Rows.Add("Chocolade", 15, New Employee("Andrew", "Fuller", 32))
			dt.Rows.Add("Filo Mix", 9, New Employee("Anne", "Dodsworth", 43))
			dt.Rows.Add("Alice Mutton", 63, New Employee("Andrew", "Fuller", 32))
			dt.Rows.Add("Chai", 99, New Employee("Janet", "Leverling", 23))
			Return dt
		End Function
	End Class
End Namespace
