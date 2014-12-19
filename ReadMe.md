Allows easy, fluent, building of datasets.
==========================================

Single fluent interface, only 4 methods

AddDataTable - Create a new DataTable

AddColumn - Add a column to the current DataTable

AddRow - Add a row to the current DataTable

Build - Generate and return the final DataSet


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
