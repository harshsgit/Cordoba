using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels.Entities
{
    public class PointsAuditEntity
    {
        public int points_audit_id { get; set; }
        public int customer_id { get; set; }
        public int adjustment { get; set; }
        public string comment { get; set; }
        public DateTime timestamp { get; set; }

        public string Withdrawal { get; set; }
        public string Deposit { get; set; }
        public string invalidEmail { get; set; }
        public int code { get; set; }

        public string MinusPointTotalEmail{ get; set; }
    }
}
