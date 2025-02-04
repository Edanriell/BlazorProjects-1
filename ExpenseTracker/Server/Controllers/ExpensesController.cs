﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseTrackerServerContext _context;

    public ExpensesController(ExpenseTrackerServerContext context)
    {
        _context = context;
    }

    // GET: api/Expenses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpense()
    {
        if (_context.Expense == null) return NotFound();
        return await _context.Expense.ToListAsync();
    }

    // GET: api/Expenses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
        if (_context.Expense == null) return NotFound();
        var expense = await _context.Expense.FindAsync(id);

        if (expense == null) return NotFound();

        return expense;
    }

    // PUT: api/Expenses/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpense(int id, Expense expense)
    {
        if (id != expense.Id) return BadRequest();

        _context.Entry(expense).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExpenseExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Expenses
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Expense>> PostExpense(Expense expense)
    {
        if (_context.Expense == null) return Problem("Entity set 'ExpenseTrackerServerContext.Expense'  is null.");
        _context.Expense.Add(expense);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
    }

    // DELETE: api/Expenses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        if (_context.Expense == null) return NotFound();
        var expense = await _context.Expense.FindAsync(id);
        if (expense == null) return NotFound();

        _context.Expense.Remove(expense);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ExpenseExists(int id)
    {
        return (_context.Expense?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}