using System;
using System.Collections.Generic;

namespace DRRCore.Domain.Entities.SqlCoreContext;

public partial class UserPermission
{
    public int Id { get; set; }

    public int? IdPermission { get; set; }

    public int? IdUser { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public bool? Enable { get; set; }

    public virtual Permission? IdPermissionNavigation { get; set; }

    public virtual UserLogin? IdUserNavigation { get; set; }
}
