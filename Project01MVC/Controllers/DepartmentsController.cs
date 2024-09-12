using Demo.BusinessLogicLayer.Intrerfaces;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        //IGenaricRepository<Department> _repository;
        private IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }
        
        public IActionResult Index()
        {
            //retrive all departments
            var department = _repository.GetAll();
            return View(department);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Create(Department department)
        {
            //sever side validation
            if(!ModelState.IsValid) return View(department);
            _repository.Create(department);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => DepartmentControllerHandler(id, nameof(Details));
        public IActionResult Edit(int? id) => DepartmentControllerHandler(id,nameof(Edit));
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //log exeption 
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
            
        }
        public IActionResult Delete(int? id) => DepartmentControllerHandler(id,nameof(Delete));

        [HttpPost,ActionName("Delete")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteDone(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();
            try
            {
                _repository.Delete(department);
                return RedirectToAction(nameof(Index));
                //
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(department);
        }

        private IActionResult DepartmentControllerHandler(int? id ,string viewName)
        {
            //retrive department and send it to the view
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);

            if (department is null) return NotFound();

            return View(viewName, department);
        }
    }
}
