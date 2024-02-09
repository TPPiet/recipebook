using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data;
public class ShoppingListItem
{
    public string? Title { get; set; }
    public bool IsDone { get; set; } = false;
}
