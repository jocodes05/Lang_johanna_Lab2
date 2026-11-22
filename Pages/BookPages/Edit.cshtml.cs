
using Lang_johanna_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lang_johanna_Lab2.Pages.BookPages
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Lang_johanna_Lab2Context _context;

        public EditModel(Lang_johanna_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Book = await _context.Book
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == id);

            if (Book == null)
                return NotFound();

            LoadDropdowns();
            PopulateAssignedCategoryData(_context, Book);

            return Page();
        }

        private void LoadDropdowns()
        {
            var authors = _context.Author.Select(a => new
            {
                a.ID,
                FullName = a.LastName + " " + a.FirstName
            }).ToList();

            ViewData["AuthorID"] = new SelectList(
                authors, "ID", "FullName", Book.AuthorID);

            ViewData["PublisherID"] = new SelectList(
                _context.Publisher.ToList(),
                "ID", "PublisherName", Book.PublisherID);
        }

        public async Task<IActionResult> OnPostAsync(
            string[] selectedCategories)
        {
            if (Book == null)
                return NotFound();

            var bookToUpdate = await _context.Book
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.ID == Book.ID);

            if (bookToUpdate == null)
                return NotFound();

            bookToUpdate.Title = Book.Title;
            bookToUpdate.AuthorID = Book.AuthorID;
            bookToUpdate.Price = Book.Price;
            bookToUpdate.PublishingDate = Book.PublishingDate;
            bookToUpdate.PublisherID = Book.PublisherID;

            UpdateBookCategories(
                _context, selectedCategories, bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
