using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFC.Model
{
    public class ProcResult
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public ProcResult()
        {
            success = false;
            message = "";
            data = null;
        }
    }
}