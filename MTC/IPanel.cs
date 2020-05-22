using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC
{
    interface IPanel
    {
        string AktualnaSciezka { get; }
        string[] Dyski { get; }
        string[] ZawartoscSciezki { get; }
    }
}
