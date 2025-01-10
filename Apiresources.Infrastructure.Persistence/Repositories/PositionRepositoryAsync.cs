namespace $safeprojectname$.Repositories
{
    // PositionRepositoryAsync handles data operations for Position entities.
    public class PositionRepositoryAsync : GenericRepositoryAsync<Position>, IPositionRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext; // Database context for interacting with database
        private readonly DbSet<Position> _repository; // DbSet for Position entity
        private readonly IDataShapeHelper<Position> _dataShaper; // Helper for shaping data
        private readonly IMockService _mockData; // Mock service for generating dummy data

        // Constructor to initialize the repository with required services
        public PositionRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Position> dataShaper, IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = dbContext.Set<Position>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        // Checks for the uniqueness of the position number in the Position DbSet
        public async Task<bool> IsUniquePositionNumberAsync(string positionNumber)
        {
            return await _repository
                .AllAsync(p => p.PositionNumber != positionNumber);
        }

        // Seeds the database with mock Position data
        public async Task<bool> SeedDataAsync(int rowCount, IEnumerable<Department> departments, IEnumerable<SalaryRange> salaryRanges)
        {
            await this.BulkInsertAsync(_mockData.GetPositions(rowCount, departments, salaryRanges));

            return true; // Returns true upon successful seed
        }

        // Retrieves paged and filtered position data
        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> PagedPositionReponseAsync(PagedPositionsQuery requestParameters)
        {
            var pageNumber = requestParameters.PageNumber;
            var pageSize = requestParameters.PageSize;
            var orderBy = requestParameters.OrderBy;
            var fields = requestParameters.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable for querying Positions
            var result = _repository
                .AsNoTracking()
                .AsExpandable();

            // Count total records
            recordsTotal = await result.CountAsync();

            // Filter data based on search value
            FilterByColumn(ref result, requestParameters.Search.Value);

            // Count records after applying filters
            recordsFiltered = await result.CountAsync();

            // Set record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // Apply ordering
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            // Select specific fields
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Position>("new(" + fields + ")");
            }
            // Apply pagination
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Retrieve data into list
            var resultData = await result.ToListAsync();
            // Shape data based on user selection
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        // Retrieves position data based on detailed query parameters
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

            // Setup IQueryable for querying Positions
            var result = _repository
                .AsNoTracking()
                .AsExpandable();

            // Count total records
            recordsTotal = await result.CountAsync();

            // Filter data based on specific columns
            FilterByColumn(ref result, positionNumber, positionTitle, department);

            // Count records after applying filters
            recordsFiltered = await result.CountAsync();

            // Set record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // Apply ordering
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            // Select specific fields
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Position>("new(" + fields + ")");
            }
            // Apply pagination
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Retrieve data into list
            var resultData = await result.ToListAsync();
            // Shape data based on user selection
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        // Filters the query based on position number, title, and department
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

            qry = qry.Where(predicate); // Apply filtering
        }

        // Filters the query based on a keyword
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

            qry = qry.Where(predicate); // Apply filtering
        }
    }
}