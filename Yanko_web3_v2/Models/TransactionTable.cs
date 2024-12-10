using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class TransactionTable
{
    public int TransactionId { get; set; }

    public int RecipientId { get; set; }

    public int SenderId { get; set; }

    public double Sum { get; set; }

    public virtual UserTable Recipient { get; set; } = null!;

    public virtual UserTable Sender { get; set; } = null!;
}
