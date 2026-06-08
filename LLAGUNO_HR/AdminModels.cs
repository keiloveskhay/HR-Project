using System;

namespace IDK2
{
    public class Department { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public override string ToString() => Name; }
    public class Role { public string Id { get; set; } = Guid.NewGuid().ToString(); public string DepartmentId { get; set; } = string.Empty; public string Name { get; set; } = string.Empty; public override string ToString() => Name; }
    public class EmploymentType { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Name { get; set; } = string.Empty; public override string ToString() => Name; }
    public class Vacancy { public string Id { get; set; } = Guid.NewGuid().ToString(); public string Title { get; set; } = string.Empty; public string DepartmentId { get; set; } = string.Empty; public string DepartmentName { get; set; } = string.Empty; public string RoleId { get; set; } = string.Empty; public string RoleName { get; set; } = string.Empty; public string EmploymentType { get; set; } = string.Empty; public string Description { get; set; } = string.Empty; public string OpenDate { get; set; } = string.Empty; public string CloseDate { get; set; } = string.Empty; public string Status { get; set; } = string.Empty; }
    public class HiringDecision { public string Id { get; set; } = Guid.NewGuid().ToString(); public string VacancyId { get; set; } = string.Empty; public string CandidateName { get; set; } = string.Empty; public string CandidateEmail { get; set; } = string.Empty; public string Decision { get; set; } = string.Empty; public string DecisionDate { get; set; } = string.Empty; public string Notes { get; set; } = string.Empty; }
    public class AuditEntry { public long Id { get; set; } public string Timestamp { get; set; } = string.Empty; public string Actor { get; set; } = string.Empty; public string Action { get; set; } = string.Empty; public string Details { get; set; } = string.Empty; }
}
