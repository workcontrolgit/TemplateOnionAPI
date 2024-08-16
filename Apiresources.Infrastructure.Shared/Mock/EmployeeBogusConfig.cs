namespace $safeprojectname$.Mock
{
public class EmployeeBogusConfig : AutoFaker<Employee>
{
    public EmployeeBogusConfig()
    {
        Randomizer.Seed = new Random(8675309);
        RuleFor(p => p.Id, f => Guid.NewGuid());
        RuleFor(p => p.FirstName, f => f.Name.FirstName());
        RuleFor(p => p.MiddleName, f => f.Name.FirstName());
        RuleFor(p => p.LastName, f => f.Name.LastName());
        RuleFor(p => p.Prefix, f => f.Name.Prefix());
        RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName));
        RuleFor(p => p.Birthday, f => f.Date.Past(18));
        RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
        RuleFor(p => p.EmployeeNumber, f => f.Commerce.Ean13());
        RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(###)-###-####"));
    }
}
}