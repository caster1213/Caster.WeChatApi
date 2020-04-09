using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Caster.WeChat.Common
{
    /// <summary>
    /// 订单表格
    /// </summary>
    public class OrderTable
    {
        private readonly List<OrderRow> _rows;

        public OrderTable(string input)
        {
            _rows = new List<OrderRow>();
            Init(input);
        }
        /// <summary>
        /// 读取csv中的数据
        /// </summary>
        /// <param name="order"></param>
        private void Init(string order)
        {
            using (StringReader sr = new StringReader(order))
            {
                string field = string.Empty;
                List<string> stringRows = new List<string>();
                int index = -1;
                while (sr.Peek() > -1)
                {
                    index++;
                    if (index == 0)
                    {
                        field = sr.ReadLine();
                        continue;
                    }

                    stringRows.Add(sr.ReadLine());
                }
                stringRows.RemoveAt(stringRows.Count - 1);
                stringRows.RemoveAt(stringRows.Count - 1);
                foreach (var row in stringRows)
                {
                    _rows.Add(new OrderRow(field, row));
                }
            }
        }


        public IEnumerable<OrderRow> Rows => _rows;


        public int Count => _rows.Count;
    }

    public class OrderRow
    {
        private readonly string[] _fields;
        private readonly string[] _values;

        public OrderRow(string field, string value)
        {
            _fields = field.Split(",");
            _values = value.Split(",")
                .Select(x => x.Remove(x.IndexOf("`", StringComparison.Ordinal), 1))
                .ToArray();
        }

        public string this[string key]
        {
            get
            {
                for (int i = 0; i < _fields.Length - 1; i++)
                {
                    if (_fields[i] == key)
                    {
                        return _values[i];
                    }
                }
                return null;
            }
        }
    }
}