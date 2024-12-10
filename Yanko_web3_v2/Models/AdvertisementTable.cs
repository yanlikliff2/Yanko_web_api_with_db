using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class AdvertisementTable
{
    public int AdvertisementId { get; set; }

    public int UserId { get; set; }

    public int ObjectId { get; set; }

    public double? Price { get; set; }

    public int? Sale { get; set; }

    public int TegId { get; set; }

    public int ChannelId { get; set; }

    public virtual ChannelTable Channel { get; set; } = null!;

    public virtual ObjectTable Object { get; set; } = null!;

    public virtual TagTable Teg { get; set; } = null!;
}
