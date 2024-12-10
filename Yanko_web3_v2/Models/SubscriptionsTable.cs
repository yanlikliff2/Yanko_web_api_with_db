using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class SubscriptionsTable
{
    public int SubscriptionsId { get; set; }

    public int UserId { get; set; }

    public int ChannelId { get; set; }

    public int SubscriptionsLevel { get; set; }

    public string? SubscribersCount { get; set; }

    public virtual UserTable User { get; set; } = null!;
}
