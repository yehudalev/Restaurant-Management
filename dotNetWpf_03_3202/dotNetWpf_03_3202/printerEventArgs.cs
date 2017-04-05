using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetWpf_03_3202
{
    class printerEventArgs
    {
        public bool isCritical { get; set; }
        public DateTime timeOfError { get; set; }
        public string errorMessege { get; set; }
        public string printerName { get; set; }
        public printerEventArgs(bool critical, string error, string name)
        {
            isCritical = critical;
            errorMessege = error;
            printerName = name;
            timeOfError = DateTime.Now;
        }
    }
}
