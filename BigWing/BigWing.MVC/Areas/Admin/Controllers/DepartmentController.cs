using BigWing.BL.VM.Department;
using BigWing.Core.Entities;
using BigWing.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigWing.MVC.Areas.Admin.Controllers;
[Area("Admin")]
public class DepartmentController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _context.Departments.ToListAsync());
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateVM vm)
    {
        if(!ModelState.IsValid)
            return View();
        Department department = new Department
        {
            Name = vm.Name,
        };
        await _context.AddAsync(department);
        await _context.SaveChangesAsync();  
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var data = await _context.Departments.FindAsync(id);
        if (data == null)
            return NotFound();

        DepartmentUpdateVM vm = new DepartmentUpdateVM
        {
            Name = data.Name
        };
        return View(vm);    
    }
    [HttpPost]
    public async Task<IActionResult> Update(int? id, DepartmentUpdateVM vm)
    {
        if (!ModelState.IsValid)
            return View();
        if (!id.HasValue)
            return BadRequest();

        var data = await _context.Departments.FindAsync(id);
        if (data == null)
            return NotFound();
            
        data.Name = vm.Name; 
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> ToggleDepartmentVisibility(int? id, bool IsDeleted)
    {
        if (!id.HasValue)
            return BadRequest(); 
        
        var data = await _context.Departments.FindAsync(id);
        if(data == null)    
            return NotFound();

        data.IsDeleted = IsDeleted;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));  
    }

    public async Task<IActionResult> Hide(int? id)
        =>  await ToggleDepartmentVisibility(id, true);

    public async Task<IActionResult> Show(int? id)
        => await ToggleDepartmentVisibility(id, false);

    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue)
            return BadRequest();

        var data = await _context.Departments.FindAsync(id);
        if (data == null)
            return NotFound();

        _context.Departments.Remove(data);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); 
    }
}
