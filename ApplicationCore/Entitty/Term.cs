using ApplicationCore.Entity;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entity
{
    public partial class Term : BaseEntity
    {
        public Guid TermId { get; set; }
        public string TermName { get; set; }
        public int? Classes { get; set; }
    }
}
