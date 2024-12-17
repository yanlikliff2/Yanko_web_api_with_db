using System;
using System.Collections.Generic;
using Yanko_web3_v2.Entities;

namespace Yanko_web3_v2.Models;

public partial class UserTable
{
    public UserTable()
    {
        ChannelTables = new List<ChannelTable>();
        CollectionTables = new List<CollectionTable>();
        TransactionTableRecipients = new List<TransactionTable>();
        TransactionTableSenders = new List<TransactionTable>();
        //Role = new Role();
    }

    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int? RoleId { get; set; }


    public bool AcceptTerms { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? Verified { get; set; }
    public bool IsVerified => Verified.HasValue || PassordReset.HasValue;
    public string? ResetToken {  get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PassordReset {  get; set; }
    public DateTime Created {  get; set; }
    public DateTime Updated { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }

    public virtual ICollection<ChannelTable> ChannelTables { get; set; } = new List<ChannelTable>();

    public virtual ICollection<CollectionTable> CollectionTables { get; set; } = new List<CollectionTable>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<TransactionTable> TransactionTableRecipients { get; set; } = new List<TransactionTable>();

    public virtual ICollection<TransactionTable> TransactionTableSenders { get; set; } = new List<TransactionTable>();

    public bool OwnsToken(string token)
    {
        return this.RefreshTokens.Find(x => x.Token == token) != null;
    }
}
