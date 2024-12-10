using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<UserTable> UserTables { get; set; } = new List<UserTable>();
}
