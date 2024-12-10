using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class ObjectTable
{
    public string Link { get; set; } = null!;

    public int ObjectId { get; set; }

    public string? ObjectDescription { get; set; }

    public int AuthorId { get; set; }

    public int? CollectionId { get; set; }

    public virtual ICollection<AdvertisementTable> AdvertisementTables { get; set; } = new List<AdvertisementTable>();

    public virtual CollectionTable? Collection { get; set; }

    public virtual ICollection<CommentTable> CommentTables { get; set; } = new List<CommentTable>();

    public virtual ICollection<ImageTable> ImageTables { get; set; } = new List<ImageTable>();
}
