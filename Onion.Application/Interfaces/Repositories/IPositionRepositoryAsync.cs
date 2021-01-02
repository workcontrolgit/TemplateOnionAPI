using $safeprojectname$.Features.Positions.Queries.GetPositions;
using $ext_projectname$.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace $safeprojectname$.Interfaces.Repositories
{
    public interface IPositionRepositoryAsync : IGenericRepositoryAsync<Position>
    {
        Task<bool> IsUniquePositionNumberAsync(string positionNumber);
        Task<bool> SeedDataAsync(int rowCount);
        Task<IEnumerable<Entity>> GetPagedPositionReponseAsync(GetPositionsQuery requestParameters);
    }
}
