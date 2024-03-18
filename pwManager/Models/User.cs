using System;
using System.Collections.Generic;

namespace pwManager.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public byte[]? Salt { get; set; }

    public byte[]? PasswordHash { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
