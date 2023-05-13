﻿namespace Ordering.Core.Entities
{
    public abstract class AuditEntry
    {
        public int Id { get; protected set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
