using System;
using System.Collections.Generic;

namespace pwManager.Models;

public partial class Account
{
    public string Name { get; set; } = null!;

    public byte[]? Password { get; set; }

    public string? UserName { get; set; }

    public byte[] Iv { get; set; } = null!;

    public int BelongsToUser { get; set; }

    public int Id { get; set; }

    public virtual User BelongsToUserNavigation { get; set; } = null!;
}
