using AutoBogus;
using Bogus;
using $ext_projectname$.Domain.Entities;
using $ext_projectname$.Domain.Enums;
using System;

namespace $safeprojectname$.Mock
{
    public class EmployeeBogusConfig : AutoFaker<Employee>
    {
        public EmployeeBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            RuleFor(p => p.Id, Guid.NewGuid());
            RuleFor(p => p.FirstName, f => f.Name.FirstName());
            RuleFor(p => p.MiddleName, f => f.Name.FirstName());
            RuleFor(p => p.LastName, f => f.Name.LastName());
            RuleFor(p => p.EmployeeTitle, f => f.Name.JobTitle());
            RuleFor(p => p.Suffix, f => f.Name.Suffix());
            RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName));
            RuleFor(p => p.DOB, f => f.Date.Past(18));
            RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
            RuleFor(p => p.EmployeeNumber, f => f.Commerce.Ean13());
            RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(###)-###-####"));
        }
    }
}
