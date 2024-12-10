using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class CommentTable
{
    public int CommentId { get; set; }

    public int ObjectId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public virtual ObjectTable Object { get; set; } = null!;
}
