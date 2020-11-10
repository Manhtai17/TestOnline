using ApplicationCore.Entitty;
using ApplicationCore.Entity;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entity
{
    public partial class Exam : BaseEntity
    {
        public Guid ExamId { get; set; }
        public Guid? ContestId { get; set; }
        public Guid? UserId { get; set; }
        public int? Point { get; set; }
        public ulong? Status { get; set; }
        public ulong? IsDoing { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Object Question { get; set; }
        public string Answer { get; set; }
        public string Result { get; set; }

    }
}
