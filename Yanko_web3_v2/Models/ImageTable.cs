using System;
using System.Collections.Generic;

namespace Yanko_web3_v2.Models;

public partial class ImageTable
{
    public int ImageId { get; set; }

    public byte[] Image { get; set; } = null!;

    public int ObjectId { get; set; }

    public virtual ObjectTable Object { get; set; } = null!;
}
