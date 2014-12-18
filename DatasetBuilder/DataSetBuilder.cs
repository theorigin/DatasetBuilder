namespace Viewsource.DatasetBuilder
{
    using System;
    using System.Data;

    public class DataSetBuilder
    {
        private readonly DataSet dataSet = new DataSet();

        public DataSetBuilder AddDataTable(string tableName)
        {
            dataSet.Tables.Add(new DataTable(tableName));

            return this;
        }

        public DataSetBuilder AddColumn(string columnName)
        {
            AddColumn(columnName, typeof(string));

            return this;
        }

        public DataSetBuilder AddColumn(string columnName, Type type)
        {
            var datatable = dataSet.Tables[dataSet.Tables.Count - 1];

            datatable.Columns.Add(new DataColumn(columnName, type));

            return this;
        }

        public DataSetBuilder AddRow(params object[] row)
        {
            var datatable = dataSet.Tables[dataSet.Tables.Count - 1];
            var dataRow = datatable.NewRow();

            for (var i = 0; i <= row.GetUpperBound(0); i++)
                dataRow[i] = row[i];

            datatable.Rows.Add(dataRow);

            return this;
        }

        public DataSet Build()
        {
            return dataSet;
        }
    }
}
