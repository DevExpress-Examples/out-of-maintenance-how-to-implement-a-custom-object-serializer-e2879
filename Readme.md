<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128582094/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2879)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/XtraPivotGrid_CustomObjectConverter/Form1.cs) (VB: [Form1.vb](./VB/XtraPivotGrid_CustomObjectConverter/Form1.vb))
<!-- default file list end -->
# How to implement a custom object serializer


<p>The following example shows how to implement a custom serializer.</p>
<p>Custom serializers are required when data source field values are custom objects (not numeric or string values). In this example, the data source contains a <em>Sales Person</em> field whose values are <em>Employee</em> objects, exposing the <em>FirstName</em>, <em>LastName</em> and <em>Age</em> properties. The <strong>Employee </strong>class implements the <strong>IComparable</strong> interface, and overrides the <strong>GetHashCode</strong>, <strong>Equals</strong> and <strong>ToString</strong> methods (required to display and handle custom objects).</p>
<p>The custom serializer is represented by the <strong>CustomObjectConverter</strong> class, which implements the <a href="https://documentation.devexpress.com/#CoreLibraries/clsDevExpressUtilsSerializingHelpersICustomObjectConvertertopic">ICustomObjectConverter </a>interface. The <strong>ToString</strong> and <strong>FromString</strong> methods are implemented to serialize and deserialize the <em>Employee</em> objects, respectively. A <strong>CustomObjectConverter</strong> class instance is assigned to the <a href="https://documentation.devexpress.com/#CoreLibraries/DevExpressXtraPivotGridPivotGridOptionsData_CustomObjectConvertertopic">PivotGridOptionsData.CustomObjectConverter</a> property. It is used for serializing <em>Sales Person</em> field values when pivot grid data is saved to a stream and restored back by an end-user via clicking the <strong>Save</strong> and <strong>Load</strong> buttons respectively.</p>

<br/>


