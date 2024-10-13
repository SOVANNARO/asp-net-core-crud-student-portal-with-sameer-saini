using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using crud_student_portal_with_sameer_saini.Data;
using crud_student_portal_with_sameer_saini.Models.Entities;
using Microsoft.EntityFrameworkCore;
using crud_student_portal_with_sameer_saini.Models;

namespace crud_student_portal_with_sameer_saini.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult Add() => View();

        [HttpPost]
        public async Task<ActionResult> Add(AddStudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var students = await _dbContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Student viewModel)
        {
            var student = await _dbContext.Students.FindAsync(viewModel.Id);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = viewModel.Name;
            student.Email = viewModel.Email;
            student.Phone = viewModel.Phone;
            student.Subscribed = viewModel.Subscribed;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Student viewModel)
        {
            var student = await _dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (student is not null)
            {
                _dbContext.Students.Remove(viewModel);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }
    }
}