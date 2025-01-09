namespace $safeprojectname$.Helpers
{
    public class ModelHelper : IModelHelper
    {
        /// <summary>
        /// Checks if specified field names exist in a model class and returns them as a comma-separated string.
        /// </summary>
        /// <typeparam name="T">The type of the model class.</typeparam>
        /// <param name="fields">A comma-separated string containing the field names to check.</param>
        /// <returns>A comma-separated string containing only the field names that exist in the model class.</returns>
        public string ValidateModelFields<T>(string fields)
        {
            // Initialize an empty string to store the return value.
            string retString = string.Empty;

            // Define binding flags to include instance, non-public, and public properties of the model class.
            var bindingFlags = System.Reflection.BindingFlags.Instance |
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Public;
            
            // Get a list of property names from the model class using the specified binding flags.
            var listFields = typeof(T).GetProperties(bindingFlags).Select(f => f.Name).ToList();
            
            // Split the input fields string into an array.
            string[] arrayFields = fields.Split(',');
            
            // Iterate through each field in the array.
            foreach (var field in arrayFields)
            {
                // Trim any leading or trailing whitespace from the field name and check if it exists in the list of model properties.
                if (listFields.Contains(field.Trim(), StringComparer.OrdinalIgnoreCase))
                    retString += field + ","; // If the field exists, add it to the return string.
            };
            
            // Return the comma-separated string containing only the existing fields.
            return retString;
        }

        /// <summary>
        /// Returns a comma-separated string containing all of the property names in a model class.
        /// </summary>
        /// <typeparam name="T">The type of the model class.</typeparam>
        /// <returns>A comma-separated string containing the property names of the model class.</returns>
        public string GetModelFields<T>()
        {
            // Initialize an empty string to store the return value.
            string retString = string.Empty;

            // Define binding flags to include instance, non-public, and public properties of the model class.
            var bindingFlags = System.Reflection.BindingFlags.Instance |
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Public;
            
            // Get a list of property names from the model class using the specified binding flags.
            var listFields = typeof(T).GetProperties(bindingFlags).Select(f => f.Name).ToList();

            // Iterate through each field in the list and add it to the return string.
            foreach (string field in listFields)
            {
                retString += field + ",";
            }

            // Return the comma-separated string containing all of the fields.
            return retString;
        }
    }
}