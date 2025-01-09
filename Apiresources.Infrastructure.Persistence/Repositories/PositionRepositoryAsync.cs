namespace $safeprojectname$.Repositories
{
    public class PositionRepositoryAsync : GenericRepositoryAsync<Position>, IPositionRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Position> _repository;
        private readonly IDataShapeHelper<Position> _dataShaper;
        private readonly IMockService _mockData;

        public PositionRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Position> dataShaper, IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = dbContext.Set<Position>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<bool> IsUniquePositionNumberAsync(string positionNumber)
        {
            return await _repository
                .AllAsync(p => p.PositionNumber != positionNumber);
        }

        public async Task<bool> SeedDataAsync(int rowCount, IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges)
        {
            await this.BulkInsertAsync(_mockData.GetPositions(rowCount, departments, salaryRanges));

            return true;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> PagedPositionReponseAsync(PagedPositionsQuery requestParameters)
        {
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
            FilterByColumn(ref result, requestParameters.Search.Value);

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

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPositionReponseAsync(GetPositionsQuery requestParameters)
        {
            var positionNumber = requestParameters.PositionNumber;
            var positionTitle = requestParameters.PositionTitle;
            var department = requestParameters.Department;

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
            FilterByColumn(ref result, positionNumber, positionTitle, department);

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

        private void FilterByColumn(ref IQueryable<Position> qry, string positionNumber, string positionTitle, string department)
        {
            if (!qry.Any())
                return;

            if (string.IsNullOrEmpty(positionTitle) && string.IsNullOrEmpty(positionNumber) && string.IsNullOrEmpty(department))
                return;

            var predicate = PredicateBuilder.New<Position>();

            if (!string.IsNullOrEmpty(positionNumber))
                predicate = predicate.Or(p => p.PositionNumber.Contains(positionNumber.Trim()));

            if (!string.IsNullOrEmpty(positionTitle))
                predicate = predicate.Or(p => p.PositionTitle.Contains(positionTitle.Trim()));

            if (!string.IsNullOrEmpty(department))
                predicate = predicate.Or(p => p.Department.Name.Contains(department.Trim()));

            qry = qry.Where(predicate);
        }

        private void FilterByColumn(ref IQueryable<Position> qry, string keyword)
        {
            if (!qry.Any())
                return;

            if (string.IsNullOrEmpty(keyword))
                return;

            var predicate = PredicateBuilder.New<Position>();

            if (!string.IsNullOrEmpty(keyword))
                predicate = predicate.Or(p => p.PositionNumber.Contains(keyword.Trim()));
            predicate = predicate.Or(p => p.PositionTitle.Contains(keyword.Trim()));
            predicate = predicate.Or(p => p.Department.Name.Contains(keyword.Trim()));

            qry = qry.Where(predicate);
        }
    }
}