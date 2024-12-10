using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class TagTable
{
    public int TegId { get; set; }

    public string TegName { get; set; } = null!;

    public virtual ICollection<AdvertisementTable> AdvertisementTables { get; set; } = new List<AdvertisementTable>();
}
