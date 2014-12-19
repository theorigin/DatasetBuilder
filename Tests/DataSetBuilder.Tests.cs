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
                    .AddDataTable("Logins")
                    .AddColumn("Id", typeof(Guid))
                    .AddColumn("UserId", typeof(int))
                    .AddColumn("LoginDate", typeof(DateTime))
                    .AddRow("0C1E1152-7A47-4798-9A64-2900FEAC79B7", 1, "2014-12-19");

            var result = datasetBuilder.Build();

            Assert.That(result.Tables.Count, Is.EqualTo(2));
            Assert.That(result.Tables[0].TableName, Is.EqualTo("Users"));
            Assert.That(result.Tables[1].TableName, Is.EqualTo("Logins"));
        }

        [Test(Description = "If nothing is setup on the builder expect an empty dataset to be returned")]
        public void EmptyDatasetReturnedWhenNoSetupPerformed()
        {
            var datasetBuilder = new DataSetBuilder();

            Assert.IsInstanceOf<DataSet>(datasetBuilder.Build());
        }
    }
}
