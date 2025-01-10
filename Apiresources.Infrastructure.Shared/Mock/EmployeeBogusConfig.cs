namespace $safeprojectname$.Mock
{
    // Class to create mock Employee data using AutoFaker
public class EmployeeBogusConfig : AutoFaker<Employee>
{
        // Constructor to define mock rules
    public EmployeeBogusConfig()
    {
            // Set a consistent random seed for reproducibility
        Randomizer.Seed = new Random(8675309);

            // Rule for generating a random unique identifier for Employee Id
        RuleFor(p => p.Id, f => Guid.NewGuid());

            // Rule for generating a random FirstName
        RuleFor(p => p.FirstName, f => f.Name.FirstName());

            // Rule for generating a random MiddleName
        RuleFor(p => p.MiddleName, f => f.Name.FirstName());

            // Rule for generating a random LastName
        RuleFor(p => p.LastName, f => f.Name.LastName());

            // Rule for generating a random prefix (e.g., Mr., Ms.)
        RuleFor(p => p.Prefix, f => f.Name.Prefix());

            // Rule for generating an email address using FirstName and LastName
        RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.FirstName, p.LastName));

            // Rule for generating a birthdate at least 18 years in the past
        RuleFor(p => p.Birthday, f => f.Date.Past(18));

            // Rule for randomly selecting a value from the Gender enum
        RuleFor(p => p.Gender, f => f.PickRandom<Gender>());

            // Rule for generating a random EmployeeNumber using EAN-13 format
        RuleFor(p => p.EmployeeNumber, f => f.Commerce.Ean13());

            // Rule for generating a phone number with a specific format
        RuleFor(p => p.Phone, f => f.Phone.PhoneNumber("(###)-###-####"));
    }
}
}