using $ext_projectname$.Application.Features.Positions.Queries.GetPositions;
using $ext_projectname$.Application.Interfaces;
using $ext_projectname$.Application.Interfaces.Repositories;
using $ext_projectname$.Application.Parameters;
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
        private readonly DbSet<Position> _repository;
        private readonly IDataShapeHelper<Position> _dataShaper;
        private readonly IMockService _mockData;

        public PositionRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Position> dataShaper, IMockService mockData) : base(dbContext)
        {
            _repository = dbContext.Set<Position>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<bool> IsUniquePositionNumberAsync(string positionNumber)
        {
            return await _repository
                .AllAsync(p => p.PositionNumber != positionNumber);
        }

        public async Task<bool> SeedDataAsync(int rowCount)
        {
            await this.BulkInsertAsync(_mockData.GetPositions(rowCount));

            return true;

        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedPositionReponseAsync(GetPositionsQuery requestParameters)
        {
            var positionNumber = requestParameters.PositionNumber;
            var positionTitle = requestParameters.PositionTitle;

            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _repository
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, positionNumber, positionTitle);

            // Count records after filter
            recordsFiltered = await result.CountAsync();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Position>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();
            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        private void FilterByColumn(ref IQueryable<Position> qry, string positionNumber, string positionTitle)
        {
            if (!qry.Any())
                return;

            if (string.IsNullOrEmpty(positionTitle) && string.IsNullOrEmpty(positionNumber))
                return;

            var predicate = PredicateBuilder.New<Position>();

            if (!string.IsNullOrEmpty(positionNumber))
                predicate = predicate.Or(p => p.PositionNumber.Contains(positionNumber.Trim()));

            if (!string.IsNullOrEmpty(positionTitle))
                predicate = predicate.Or(p => p.PositionTitle.Contains(positionTitle.Trim()));

            qry = qry.Where(predicate);
        }
    }
}