using Demo.BusinessLogicLayer.Repository;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        IDepartmentRepository _repository;

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
        public IActionResult Create(Department department)
        {
            //sever side validation
            if(!ModelState.IsValid) return View(department);
            _repository.Create(department);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            //retrive department and send it to the view
            if(!id.HasValue) return BadRequest(); 
            var department = _repository.Get(id.Value);

            if (department is null) return NotFound();
            
            return View(department);
        }
        public IActionResult Edit(int? id)
        {
            //retrive department and send it to the view
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }
        [HttpPost]
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
        public IActionResult Delete(int? id)
        {
            //retrive department and send it to the view
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);

            if (department is null) return NotFound();

            return View(department);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteDone(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();
            try
            {
                _repository.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(department);
        }
    }
}
