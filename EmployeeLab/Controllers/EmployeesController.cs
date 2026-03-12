using Microsoft.AspNetCore.Mvc;
using EmployeeLab.Models;
using EmployeeLab.Repositories;

namespace EmployeeLab.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var employees = _repository.GetAll();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(employee);
                TempData["SuccessMessage"] = "Сотрудник успешно добавлен!";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _repository.Update(employee);
                TempData["SuccessMessage"] = "Данные сотрудника обновлены!";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            TempData["SuccessMessage"] = "Сотрудник удален!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Active()
        {
            var employees = _repository.GetActive();
            ViewBag.Filter = "Активные сотрудники";
            return View("Index", employees);
        }

        public IActionResult ByDepartment(string department)
        {
            var employees = _repository.GetByDepartment(department);
            ViewBag.Filter = $"Отдел: {department}";
            return View("Index", employees);
        }
    }
}