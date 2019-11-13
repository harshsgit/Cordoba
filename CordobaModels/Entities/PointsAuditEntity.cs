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

        public string MinusPointTotalEmail { get; set; }
    }

    public class PointReviewEntity
    {

        public string emailAddress { get; set; }
        public decimal? Uploadpoints { get; set; }
        public decimal? existingPoints { get; set; }
        public decimal? adjustmentPoints { get; set; }
        public string comment { get; set; }
        public string Note { get; set; }
        public bool? executed { get; set; }
        public decimal UniqueNo { get; set; }
    }

    public class auditPointTMP
    {
        public decimal UniqueNo { get; set; }
        public string emailAddress { get; set; }
        public decimal? adjustmentPoints { get; set; }
        public string comment { get; set; }

    }

    public class CustomerPointDetail
    {
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public string URL { get; set; }
        public int ImportedPoints { get; set; }
        public int TotalPoints { get; set; }
        public string EmailAddress { get; set; }
    }
}
