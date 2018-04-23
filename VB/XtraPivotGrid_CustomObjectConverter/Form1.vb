Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.Data.PivotGrid
Imports DevExpress.Utils.Serializing.Helpers

Namespace XtraPivotGrid_CustomObjectConverter
	Partial Public Class Form1
		Inherits Form
		Private stream As MemoryStream
		Public Sub New()
			InitializeComponent()
			pivotGridControl1.DataSource = DataHelper.GetData()
			pivotGridControl1.OptionsData.CustomObjectConverter = _
				New CustomObjectConverter()
		End Sub

		' Handles the Save button's Click event to save pivot grid data to a stream
		' (requires data source serialization).
		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) _
				Handles simpleButton1.Click
			If stream IsNot Nothing Then
				stream.Dispose()
			End If
			stream = New MemoryStream()
			pivotGridControl1.SavePivotGridToStream(stream)
		End Sub

		' Handles the Load button's Click event to load pivot grid data from a stream
		' (requires stream content deserialization).
		Private Sub simpleButton2_Click(ByVal sender As Object, ByVal e As EventArgs) _
				Handles simpleButton2.Click
			If stream Is Nothing Then
				Return
			End If
			Dim ds As New PivotFileDataSource(stream, New CustomObjectConverter())
			pivotGridControl1.DataSource = ds
		End Sub
	End Class

	' Implements a custom serializer.
	Public Class CustomObjectConverter
		Implements ICustomObjectConverter

		' Returns a value, indicating whether objects of the specified type
		' can be serialized/deserialized.
		Public Function CanConvert(ByVal type As Type) As Boolean _
				Implements ICustomObjectConverter.CanConvert
			Return type Is GetType(Employee)
		End Function

		' Deserializes objects of the specified type.
		Public Function FromString(ByVal type As Type, ByVal str As String) As Object _
				Implements ICustomObjectConverter.FromString
			If type IsNot GetType(Employee) Then
				Return Nothing
			End If
			Dim array() As String = str.Split("#"c)
			If array.Length >= 3 Then
				Return New Employee(array(0), array(1), Integer.Parse(array(2)))
			ElseIf array.Length = 2 Then
				Return New Employee(array(0), array(1), 0)
			ElseIf array.Length = 1 Then
				Return New Employee(array(0), String.Empty, 0)
			Else
				Return New Employee(String.Empty, String.Empty, 0)
			End If
		End Function

		' Serializes objects of the specified type.
		Public Overloads Function ToString(ByVal type As Type, ByVal obj As Object) As String _
				Implements ICustomObjectConverter.ToString
			If type IsNot GetType(Employee) Then
				Return String.Empty
			End If
			Dim value As Employee = TryCast(obj, Employee)
			Return value.FirstName + "#"c + value.LastName + "#"c + value.Age
		End Function

		' Returns the type by its full name.
		Public Overloads Function [GetType](ByVal typeName As String) As Type _
				Implements ICustomObjectConverter.GetType
			If typeName IsNot GetType(Employee).FullName Then
				Return Nothing
			End If
			Return GetType(Employee)
		End Function
	End Class
End Namespace
