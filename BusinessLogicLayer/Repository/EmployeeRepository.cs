
namespace Demo.BusinessLogicLayer.Repository
{
    public class EmployeeRepository : GenaricRepository<Employee>, IEmployeeRepository
    {
        //private readonly DataContext _dataContext;
        ////ctor injection
        //public EmployeeRepository(DataContext dataContext)
        //{
        //    _dataContext = dataContext;
        //}
        //public int Create(Employee entity)
        //{
        //    _dataContext.Employees.Add(entity);
        //    return _dataContext.SaveChanges();
        //}

        //public int Delete(Employee entity)
        //{
        //    _dataContext.Employees.Remove(entity);
        //    return _dataContext.SaveChanges();
        //}

        //public Employee? Get(int id) => _dataContext.Employees.Find();
        //public IEnumerable<Employee> GetAll() => _dataContext.Employees.ToList();

        //public int Update(Employee entity)
        //{
        //    _dataContext.Employees.Update(entity);
        //    return _dataContext.SaveChanges();
        //}
        public EmployeeRepository(DbContext context):base(context)
        {
            
        }
        public IEnumerable<Employee> GetAll(string address)
        {
            return _entities.Where(e=> e.Address.ToLower() == address.ToLower()).ToList();
        }
    }
}
