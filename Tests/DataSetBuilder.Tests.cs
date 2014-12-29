namespace Viewsource.DatasetBuilder.Tests
{
    using System;
    using System.Data;

    using NUnit.Framework;

    [TestFixture]
    public class DataSetBuilderTests
    {
        [Test(Description = "Correct tables, columns and data generated")]
        public void CorrectDatasetReturned()
        {
            var datasetBuilder =
                new DataSetBuilder().AddDataTable("Users")
                    .AddColumn("Id", typeof(int))
                    .AddColumn("Firstname")
                    .AddColumn("Lastname")
                    .AddColumn("DateOfBirth", typeof(DateTime))
                    .AddColumn("IsActive", typeof(bool))
                    .AddRow(1, "Andy", "Robinson", "1980-04-06", true)
                    .AddRow(2, "Dave", "Clarke", "1970-03-28", true)
                    .AddDataTable("Logins")
                    .AddColumn("Id", typeof(Guid))
                    .AddColumn("UserId", typeof(int))
                    .AddColumn("LoginDate", typeof(DateTime))
                    .AddRow(Guid.NewGuid(), 1, "2014-12-19");

            var result = datasetBuilder.Build();

            Assert.That(result.Tables.Count, Is.EqualTo(2));
            Assert.That(result.Tables[0].TableName, Is.EqualTo("Users"));
            Assert.That(result.Tables[1].TableName, Is.EqualTo("Logins"));

            var users = result.Tables["Users"];

            Assert.That(users.Rows.Count, Is.EqualTo(2));
            Assert.That(users.Rows[0][0].ToString(), Is.EqualTo("Andy"));
            Assert.That(users.Rows[0][1].ToString(), Is.EqualTo("Robinson"));
            Assert.That(users.Rows[0][2].ToString(), Is.EqualTo(""));
        }

        [Test(Description = "If nothing is setup on the builder expect an empty dataset to be returned")]
        public void EmptyDatasetReturnedWhenNoSetupPerformed()
        {
            var datasetBuilder = new DataSetBuilder();

            Assert.IsInstanceOf<DataSet>(datasetBuilder.Build());
        }

        [Test(Description = "Correct columns are created with correct data types")]
        public void CorrectColumnsAreCreated()
        {
            var datasetBuilder = new DataSetBuilder();

            var dataSet = datasetBuilder.AddDataTable("Users")
                .AddColumn("Id", typeof(int))
                .AddColumn("Firstname")
                .AddColumn("Lastname")
                .AddColumn("DateOfBirth", typeof(DateTime))
                .AddColumn("IsActive", typeof(bool))
                .Build();

            Assert.That(dataSet.Tables.Count, Is.EqualTo(1));

            var table = dataSet.Tables[0];
            Assert.That(table.Columns.Count, Is.EqualTo(5));

            Assert.That(table.Columns.Contains("Id"));
            Assert.That(table.Columns.Contains("Firstname"));
            Assert.That(table.Columns.Contains("Lastname"));
            Assert.That(table.Columns.Contains("DateOfBirth"));
            Assert.That(table.Columns.Contains("IsActive"));

            Assert.That(table.Columns[0].DataType, Is.EqualTo(typeof(int)));
            Assert.That(table.Columns[1].DataType, Is.EqualTo(typeof(string)));
            Assert.That(table.Columns[2].DataType, Is.EqualTo(typeof(string)));
            Assert.That(table.Columns[3].DataType, Is.EqualTo(typeof(DateTime)));
            Assert.That(table.Columns[4].DataType, Is.EqualTo(typeof(bool)));
        }

        [Test(Description = "Correct columns added when using AddColumns method")]
        public void CorrectColumnsWhenAddingMultiple()
        {
            var datasetBuilder = new DataSetBuilder();

            var dataSet = datasetBuilder
                .AddDataTable("Users")
                .AddColumns(new Column { Name = "Id", Type = typeof(int) }, new Column { Name = "FirstName" })
                .Build();

            Assert.That(dataSet.Tables.Count, Is.EqualTo(1));

            var table = dataSet.Tables[0];
            Assert.That(table.Columns.Count, Is.EqualTo(2));

            Assert.That(table.Columns.Contains("Id"));
            Assert.That(table.Columns.Contains("Firstname"));

            Assert.That(table.Columns[0].DataType, Is.EqualTo(typeof(int)));
            Assert.That(table.Columns[1].DataType, Is.EqualTo(typeof(string)));
        }
    }
}
