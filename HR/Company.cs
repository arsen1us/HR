using System;
using System.Collections.Generic;

namespace HR;

public partial class Company
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Owner { get; set; } = null!;

    public string FkClientId { get; set; } = null!;

    public virtual Client FkClient { get; set; } = null!;

    public virtual ICollection<Profession> Professions { get; set; } = new List<Profession>();
}
