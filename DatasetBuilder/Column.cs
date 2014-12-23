namespace Viewsource.DatasetBuilder
{
    using System;

    public class Column
    {
        public Column()
        {
            Type = typeof(string);
        }

        public string Name { get; set; }

        public Type Type { get; set; }
    }
}