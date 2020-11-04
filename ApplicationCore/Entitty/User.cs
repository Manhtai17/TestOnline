using ApplicationCore.Entity;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entity
{
    public partial class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
        public string FullName { get; set; }
    }
}
