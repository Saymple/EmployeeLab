using EmployeeLab.Models;

namespace EmployeeLab.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees;
        private int _nextId = 1;

        public InMemoryEmployeeRepository()
        {
            _employees = new List<Employee>();
            SeedData();
        }

        private void SeedData()
        {
            Add(new Employee
            {
                FirstName = "Иван",
                LastName = "Петров",
                Position = "Разработчик",
                Department = "IT",
                Salary = 120000,
                HireDate = new DateTime(2022, 3, 15),
                Email = "ivan.petrov@company.ru",
                Phone = "+7 (999) 123-45-67",
                IsActive = true
            });

            Add(new Employee
            {
                FirstName = "Мария",
                LastName = "Сидорова",
                Position = "Менеджер проектов",
                Department = "Управление",
                Salary = 150000,
                HireDate = new DateTime(2021, 8, 1),
                Email = "maria.sidorova@company.ru",
                Phone = "+7 (999) 765-43-21",
                IsActive = true
            });

            Add(new Employee
            {
                FirstName = "Алексей",
                LastName = "Иванов",
                Position = "Тестировщик",
                Department = "IT",
                Salary = 90000,
                HireDate = new DateTime(2023, 1, 10),
                Email = "alexey.ivanov@company.ru",
                IsActive = false
            });
        }

        public IEnumerable<Employee> GetAll() => _employees;

        public Employee? GetById(int id) => _employees.FirstOrDefault(e => e.Id == id);

        public void Add(Employee employee)
        {
            employee.Id = _nextId++;
            _employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var existing = GetById(employee.Id);
            if (existing != null)
            {
                existing.FirstName = employee.FirstName;
                existing.LastName = employee.LastName;
                existing.Position = employee.Position;
                existing.Department = employee.Department;
                existing.Salary = employee.Salary;
                existing.HireDate = employee.HireDate;
                existing.Email = employee.Email;
                existing.Phone = employee.Phone;
                existing.IsActive = employee.IsActive;
            }
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _employees.Remove(employee);
            }
        }

        public IEnumerable<Employee> GetByDepartment(string department)
        {
            return _employees.Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Employee> GetActive()
        {
            return _employees.Where(e => e.IsActive);
        }
    }
}