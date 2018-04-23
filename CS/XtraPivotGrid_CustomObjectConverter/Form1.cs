using System;
using System.IO;
using System.Windows.Forms;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils.Serializing.Helpers;

namespace XtraPivotGrid_CustomObjectConverter {
    public partial class Form1 : Form {
        MemoryStream stream;
        public Form1() {
            InitializeComponent();
            pivotGridControl1.DataSource = DataHelper.GetData();
            pivotGridControl1.OptionsData.CustomObjectConverter = new CustomObjectConverter();
        }

        // Handles the Save button's Click event to save pivot grid data to a stream
        // (requires data source serialization).
        private void simpleButton1_Click(object sender, EventArgs e) {
            if (stream != null) stream.Dispose();
            stream = new MemoryStream();
            pivotGridControl1.SavePivotGridToStream(stream);
        }

        // Handles the Load button's Click event to load pivot grid data from a stream
        // (requires stream content deserialization).
        private void simpleButton2_Click(object sender, EventArgs e) {
            if (stream == null) return;
            PivotFileDataSource ds = new PivotFileDataSource(stream, new CustomObjectConverter());
            pivotGridControl1.DataSource = ds;
        }
    }

    // Implements a custom serializer.
    public class CustomObjectConverter : ICustomObjectConverter {

        // Returns a value, indicating whether objects of the specified type
        // can be serialized/deserialized.
        public bool CanConvert(Type type) {
            return type == typeof(Employee);
        }

        // Deserializes objects of the specified type.
        public object FromString(Type type, string str) {
            if (type != typeof(Employee))
                return null;
            string[] array = str.Split('#');
            if (array.Length >= 3)
                return new Employee(array[0], array[1], int.Parse(array[2]));
            else if (array.Length == 2)
                return new Employee(array[0], array[1], 0);
            else if (array.Length == 1)
                return new Employee(array[0], string.Empty, 0);
            else
                return new Employee(string.Empty, string.Empty, 0);
        }

        // Serializes objects of the specified type.
        public string ToString(Type type, object obj) {
            if (type != typeof(Employee))
                return string.Empty;
            Employee value = obj as Employee;
            return value.FirstName + '#' + value.LastName + '#' + value.Age;
        }

        // Returns the type by its full name.
        public Type GetType(string typeName) {
            if (typeName != typeof(Employee).FullName)
                return null;
            return typeof(Employee);
        }
    }
}
