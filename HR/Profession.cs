using System;
using System.Collections.Generic;

namespace HR;

public partial class Profession
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Salary { get; set; }

    public string FkCompanyId { get; set; } = null!;

    public virtual Company FkCompany { get; set; } = null!;
}
