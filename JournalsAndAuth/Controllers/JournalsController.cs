using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JournalsAndAuth.Data;
using JournalsAndAuth.Models;
using JournalsAndAuth.Areas.Identity.Data;

namespace JournalsAndAuth.Controllers
{
    public class JournalsController : Controller
    {
        private readonly JournalsContext _context;

        public JournalsController(JournalsContext context)
        {
            _context = context;
        }

        // GET: Journals
        public async Task<IActionResult> Index()
        {
            // query the User from the database with the same name of the currently logged in user
            JournalsUser? user = _context.Users
                .Include(u => u.Journals)
                    .ThenInclude(j => j.Blog)
                .FirstOrDefault(u => u.UserName == User.Identity.Name);

            if(user != null)
            {
                return View(user.Journals.ToHashSet());   
            } else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> AddNote(int? id)
        {
            if(id != null)
            {
                ViewData["JournalId"] = id;
                return View();
            } else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([Bind("JournalId,Body")] Note note)
        {
            try
            {
                // add the user id of the logged in user to the note
                note.UserId = _context.Users.First(u => u.UserName == User.Identity.Name).Id;
                // re-evaluate the modelstate

                ModelState.ClearValidationState(nameof(note.UserId));

                if (TryValidateModel(note)) {
                    _context.Notes.Add(note);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(note);
                }
            } catch (Exception ex)
            {
                return BadRequest();
            }
            
        }

        // GET: Journals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Journals == null)
            {
                return NotFound();
            }

            var journal = await _context.Journals
                .Include(j => j.Blog)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // GET: Journals/Create
        public IActionResult Create(int? blogid)
        {
            if(blogid == null || _context.Blogs.Find(blogid) == null)
            {
                return BadRequest(); 
            } 

            ViewData["BlogId"] = blogid;
            return View();
        }

        // POST: Journals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Body,BlogId")] Journal journal)
        {
            journal.PublicationTime = DateTime.Now;

            // this journal belongs to this user
            // query the User from the database using the Name of the logged in User
            try
            {
                journal.User = _context.Users.First(u => u.UserName == User.Identity.Name);
                journal.UserId = journal.User.Id;

                // clearValidationState will clear the validation of a specified property of the model
                ModelState.ClearValidationState(nameof(journal.UserId));

                if (TryValidateModel(journal))
                {
                    _context.Add(journal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Id", journal.BlogId);
                return View(journal);
            } catch(Exception ex)
            {
                return BadRequest();
            }

            
        }

        // GET: Journals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Journals == null)
            {
                return NotFound();
            }

            var journal = await _context.Journals.FindAsync(id);
            if (journal == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Id", journal.BlogId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", journal.UserId);
            return View(journal);
        }

        // POST: Journals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Body,PublicationTime,BlogId,UserId")] Journal journal)
        {
            if (id != journal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(journal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JournalExists(journal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.Blogs, "Id", "Id", journal.BlogId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", journal.UserId);
            return View(journal);
        }

        // GET: Journals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Journals == null)
            {
                return NotFound();
            }

            var journal = await _context.Journals
                .Include(j => j.Blog)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (journal == null)
            {
                return NotFound();
            }

            return View(journal);
        }

        // POST: Journals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Journals == null)
            {
                return Problem("Entity set 'JournalsContext.Journals'  is null.");
            }
            var journal = await _context.Journals.FindAsync(id);
            if (journal != null)
            {
                _context.Journals.Remove(journal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JournalExists(int id)
        {
          return (_context.Journals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
