using PExplain.PortableExecutable;
using System;
using System.Collections.Generic;

namespace PExplain.Output
{
    public class Table : IGroup
    {
        public string Name { get; }
        public IReadOnlyCollection<IGroup> Entries { get; } = new List<IGroup>();

        public Table(string name, IEnumerable<IGroup> rows)
        {
            Name = name;
            Entries = new List<IGroup>(rows);
        }

        public Table(string name, object data)
        {
            Name = name;

            var rows = new List<IGroup>();
            foreach (var property in data.GetType().GetProperties())
            {
                var value = (dynamic)property.GetValue(data);

                if (property.PropertyType.IsGenericType &&
                    typeof(IEnumerable<>).IsAssignableFrom(property.PropertyType))
                {
                    rows.AddRange(value.Select(new Func<dynamic, Table>(v => new Table(property.Name, v))));
                }
                else if (typeof(IInfo).IsAssignableFrom(property.PropertyType))
                {
                    rows.Add(new Row(property.Name, value));
                }
                else
                {
                    rows.Add(new Table(property.Name, value));
                }
            }
            Entries = rows;
        }
    }
}
