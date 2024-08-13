using System;

namespace $safeprojectname$.Application.Features.SalaryRanges.Queries.GetSalaryRanges
{
    public class GetSalaryRangesViewModel //: SalaryRange
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }

    }
}