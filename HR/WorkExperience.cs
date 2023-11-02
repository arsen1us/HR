using System;
using System.Collections.Generic;

namespace HR;

public partial class WorkExperience
{
    public string Id { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Profession { get; set; } = null!;

    public DateTime BeginningOfWork { get; set; }

    public bool NowWorking { get; set; }

    public DateTime EndingOfWork { get; set; }

    public double Salary { get; set; }

    public string FkClientId { get; set; } = null!;

    public virtual Client FkClient { get; set; } = null!;
}
