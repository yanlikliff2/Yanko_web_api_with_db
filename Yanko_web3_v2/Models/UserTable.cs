using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class UserTable
{
    public UserTable()
    {

    }

    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<ChannelTable> ChannelTables { get; set; } = new List<ChannelTable>();

    public virtual ICollection<CollectionTable> CollectionTables { get; set; } = new List<CollectionTable>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<TransactionTable> TransactionTableRecipients { get; set; } = new List<TransactionTable>();

    public virtual ICollection<TransactionTable> TransactionTableSenders { get; set; } = new List<TransactionTable>();
}
