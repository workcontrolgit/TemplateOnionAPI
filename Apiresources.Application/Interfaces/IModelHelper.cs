namespace $safeprojectname$.Interfaces
{
    /// <summary>
    /// Interface for model helper.
    /// </summary>
    public interface IModelHelper
    {
        /// <summary>
        /// Gets the fields of a model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <returns>The fields of the model as a string.</returns>
        string GetModelFields<T>();

        /// <summary>
        /// Validates the fields of a model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="fields">The fields to validate as a string.</param>
        /// <returns>A validation message if there are any errors, or null if all fields are valid.</returns>
        string ValidateModelFields<T>(string fields);
    }
}