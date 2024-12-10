using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class CollectionTable
{
    public int CollectionId { get; set; }

    public int UserId { get; set; }

    public string CollectionName { get; set; } = null!;

    public virtual ICollection<ObjectTable> ObjectTables { get; set; } = new List<ObjectTable>();

    public virtual UserTable User { get; set; } = null!;
}
