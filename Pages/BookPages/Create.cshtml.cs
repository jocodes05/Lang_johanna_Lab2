
using Lang_johanna_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lang_johanna_Lab2.Pages.BookPages
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Lang_johanna_Lab2Context _context;

        public CreateModel(Lang_johanna_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public IActionResult OnGet()
        {
            Book = new Book();
            Book.BookCategories = new List<BookCategory>();

            LoadDropdowns();
            PopulateAssignedCategoryData(_context, Book);

            return Page();
        }

        private void LoadDropdowns()
        {
            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            }).ToList();

            ViewData["AuthorID"] = new SelectList(
                authorList, "ID", "FullName");

            ViewData["PublisherID"] = new SelectList(
                _context.Publisher.ToList(), "ID", "PublisherName");
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            Book.BookCategories = new List<BookCategory>();

            if (selectedCategories != null)
            {
                foreach (var cat in selectedCategories)
                {
                    Book.BookCategories.Add(new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    });
                }
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
