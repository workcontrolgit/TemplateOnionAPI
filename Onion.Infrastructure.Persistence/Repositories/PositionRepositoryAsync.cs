using $ext_projectname$.Application.Features.Positions.Queries.GetPositions;
using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Application.Interfaces.Repositories;
using $ext_projectname$.Domain.Entities;
using $safeprojectname$.Contexts;
using $safeprojectname$.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace $safeprojectname$.Repositories
{
    public class PositionRepositoryAsync : GenericRepositoryAsync<Position>, IPositionRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Position> _positions;
        private IDataShapeHelper<Position> _dataShaper;
        private readonly IMockService _mockData;



        public PositionRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Position> dataShaper, IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _positions = dbContext.Set<Position>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<bool> IsUniquePositionNumberAsync(string positionNumber)
        {
            return await _positions
                .AllAsync(p => p.PositionNumber != positionNumber);
        }
        public async Task<bool> SeedDataAsync(int rowCount)
        {

            foreach (Position position in _mockData.GetPositions(rowCount))
            {
                await this.AddAsync(position);
            
            }
            return true;
        }

        public async Task<IEnumerable<Entity>> GetPagedPositionReponseAsync(GetPositionsQuery requestParameter)
        {
            var positionNumber = requestParameter.PositionNumber;
            var positionTitle = requestParameter.PositionTitle;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            //setup IQueryAble
            var result = _positions
                .AsNoTracking()
                .AsExpandable();

            // filter
            FilterByColumn(ref result, positionNumber, positionTitle);
            // order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }
            // page
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Position>("new(" + fields + ")");
            }
            // ToList
            var resultData = await result.ToListAsync();
            // Shape data
            return _dataShaper.ShapeData(resultData, fields);

        }
        private void FilterByColumn(ref IQueryable<Position> positions, string positionNumber, string positionTitle)
        {
            if (!positions.Any())
                return;

            if (string.IsNullOrEmpty(positionTitle) && string.IsNullOrEmpty(positionNumber))
                return;

            var predicate = PredicateBuilder.New<Position>();

            if (!string.IsNullOrEmpty(positionNumber))
                predicate = predicate.And(p => p.PositionNumber.Contains(positionNumber.Trim()));

            if (!string.IsNullOrEmpty(positionTitle))
                predicate = predicate.And(p => p.PositionTitle.Contains(positionTitle.Trim()));

            positions = positions.Where(predicate);
        }
    }
}
