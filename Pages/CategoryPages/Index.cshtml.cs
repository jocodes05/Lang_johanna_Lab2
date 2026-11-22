
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lang_johanna_Lab2.Models;

namespace Lang_johanna_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Lang_johanna_Lab2Context _context;

    public IndexModel(Lang_johanna_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public Category SelectedCategory { get; set; }

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category.ToListAsync();

        if (id != null)
        {
            SelectedCategory = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                        .ThenInclude(b => b.Author)
                .FirstOrDefaultAsync(c => c.ID == id);
        }
    }
}
