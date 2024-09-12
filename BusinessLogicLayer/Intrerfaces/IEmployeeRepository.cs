namespace Demo.BusinessLogicLayer.Intrerfaces
{
    public interface IEmployeeRepository : IGenaricRepository<Employee>
    {
        public IEnumerable<Employee> GetAll(string address);
    }
}
