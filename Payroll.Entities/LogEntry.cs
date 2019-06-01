using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Entities
{
    public class LogEntry: IEntityBase
    {
        public int ID { get; set; }

        public int TimeCardId { get; set; }

        public DateTime Logdate { get; set; }

        public TimeSpan EntryTime { get; set; }

        public TimeSpan DepartTime { get; set; }

        public TimeSpan WorkerdHours { get; set; }

        public string LogEntryImage { get; set; }
    }
}
