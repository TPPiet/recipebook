using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RecipeBook.Data
{
    public class RecipeIngredient : Ingredient
    {
        public int RecipeId { get; set; }
    }
}
