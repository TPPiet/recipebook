using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RecipeBook.Data;

public class Recipe
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }

    [Ignore]
    public List<Ingredient> Ingredients { get; set; }
}