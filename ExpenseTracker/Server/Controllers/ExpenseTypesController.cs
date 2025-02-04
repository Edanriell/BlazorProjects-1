﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseTypesController : ControllerBase
{
    private readonly ExpenseTrackerServerContext _context;

    public ExpenseTypesController(ExpenseTrackerServerContext context)
    {
        _context = context;
    }

    // GET: api/ExpenseTypes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseType>>> GetExpenseType()
    {
        if (_context.ExpenseType == null) return NotFound();
        return await _context.ExpenseType.ToListAsync();
    }

    // GET: api/ExpenseTypes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseType>> GetExpenseType(int id)
    {
        if (_context.ExpenseType == null) return NotFound();
        var expenseType = await _context.ExpenseType.FindAsync(id);

        if (expenseType == null) return NotFound();

        return expenseType;
    }

    // PUT: api/ExpenseTypes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpenseType(int id, ExpenseType expenseType)
    {
        if (id != expenseType.Id) return BadRequest();

        _context.Entry(expenseType).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExpenseTypeExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/ExpenseTypes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ExpenseType>> PostExpenseType(ExpenseType expenseType)
    {
        if (_context.ExpenseType == null)
            return Problem("Entity set 'ExpenseTrackerServerContext.ExpenseType'  is null.");
        _context.ExpenseType.Add(expenseType);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetExpenseType", new { id = expenseType.Id }, expenseType);
    }

    // DELETE: api/ExpenseTypes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpenseType(int id)
    {
        if (_context.ExpenseType == null) return NotFound();
        var expenseType = await _context.ExpenseType.FindAsync(id);
        if (expenseType == null) return NotFound();

        _context.ExpenseType.Remove(expenseType);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ExpenseTypeExists(int id)
    {
        return (_context.ExpenseType?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}