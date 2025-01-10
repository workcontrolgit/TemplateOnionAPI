namespace $safeprojectname$.Helpers
{
    public class DataShapeHelper<T> : IDataShapeHelper<T>
    {
        // A collection of PropertyInfo objects that represents all the properties of type T.
        public PropertyInfo[] Properties { get; set; }

        public DataShapeHelper()
        {
            // Get all public instance properties of type T.
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        // Returns a collection of Entity objects that contain only the specified fields from the given entities.
        public IEnumerable<Entity> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            // Get the required properties based on the fieldsString parameter.
            var requiredProperties = GetRequiredProperties(fieldsString);

            // Fetch the data and return it as a collection of Entity objects.
            return FetchData(entities, requiredProperties);
        }

        public async Task<IEnumerable<Entity>> ShapeDataAsync(IEnumerable<T> entities, string fieldsString)
        {
            // Get the required properties based on the fieldsString parameter.
            var requiredProperties = GetRequiredProperties(fieldsString);

            // Fetch the data and return it as a collection of Entity objects using Task.Run().
            return await Task.Run(() => FetchData(entities, requiredProperties));
        }

        // Returns an Entity object that contains only the specified fields from the given entity.
        public Entity ShapeData(T entity, string fieldsString)
        {
            // Get the required properties based on the fieldsString parameter.
            var requiredProperties = GetRequiredProperties(fieldsString);

            // Fetch and return the data for a single entity.
            return FetchDataForEntity(entity, requiredProperties);
        }

        // Returns a collection of PropertyInfo objects that represent the specified fields in type T.
        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                // Split the fieldsString parameter by commas and remove any empty entries.
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    // Find the PropertyInfo object that matches the current field name, ignoring case.
                    var property = Properties.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property == null)
                        continue;

                    requiredProperties.Add(property);
                }
            }
            else
            {
                // If no fieldsString parameter was provided, return all properties in type T.
                requiredProperties = Properties.ToList();
            }

            return requiredProperties;
        }

        // Returns a collection of Entity objects that contain only the specified fields from the given entities.
        private IEnumerable<Entity> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<Entity>();

            foreach (var entity in entities)
            {
                // Fetch and add the data for each entity to the shapedData collection.
                var shapedObject = FetchDataForEntity(entity, requiredProperties);
                shapedData.Add(shapedObject);
            }

            return shapedData;
        }

        // Returns an Entity object that contains only the specified fields from the given entity.
        private Entity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new Entity();

            foreach (var property in requiredProperties)
            {
                // Get the value of the current property from the entity and add it to the shapedObject.
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }

            return shapedObject;
        }
    }
}