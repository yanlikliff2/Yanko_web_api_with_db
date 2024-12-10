using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class ChannelTable
{
    public int ChannelId { get; set; }

    public string ChannelName { get; set; } = null!;

    public int AutorId { get; set; }

    public virtual ICollection<AdvertisementTable> AdvertisementTables { get; set; } = new List<AdvertisementTable>();

    public virtual UserTable Autor { get; set; } = null!;
}
