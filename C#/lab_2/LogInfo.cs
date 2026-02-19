using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    interface LogInfo
    {
        string Name { get; }
        string Log { get; }
        string LogInfo { get => $"[{{date}}] Статус: {{GetStatus()}}"; }
    }
}
