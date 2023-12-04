using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class JSDataTable
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<Object> data { get; set; }
        public Object extra { get; set; }

        public JSDataTable()
        {
            draw = 1;
            recordsTotal = 0;
            recordsFiltered = 0;
            data = new List<object>();
        }
    }
}