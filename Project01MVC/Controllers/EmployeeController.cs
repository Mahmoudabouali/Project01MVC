using Demo.BusinessLogicLayer.Intrerfaces;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _repository = employeeRepository;
        }

        public IActionResult Index()
        {
            //retrive all employee
            var employees = _repository.GetAll();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(Employee employees)
        {
            //sever side validation
            if (!ModelState.IsValid) return View(employees);
            _repository.Create(employees);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));
        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee emplyees)
        {
            if (id != emplyees.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(emplyees);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //log exeption 
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(emplyees);

        }
        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));

        [HttpPost, ActionName("Delete")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteDone(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employees = _repository.Get(id.Value);
            if (employees is null) return NotFound();
            try
            {
                _repository.Delete(employees);
                return RedirectToAction(nameof(Index));
                //
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(employees);
        }

        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {
            //retrive department and send it to the view
            if (!id.HasValue) return BadRequest();
            var employees = _repository.Get(id.Value);

            if (employees is null) return NotFound();

            return View(viewName, employees);
        }
    }
}
