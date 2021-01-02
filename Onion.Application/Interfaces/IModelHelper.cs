namespace $safeprojectname$.Interfaces
{
    public interface IModelHelper
    {
        string GetModelFields<T>();
        string ValidateModelFields<T>(string fields);
    }
}