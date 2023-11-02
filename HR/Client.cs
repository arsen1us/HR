using System;
using System.Collections.Generic;

namespace HR;

public partial class Client
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public short Age { get; set; }

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
}
