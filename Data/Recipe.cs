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

    [Ignore]
    public List<Instruction> Instructions { get; set; }

    [Ignore]
    public List<RecipeIngredient> Ingredients { get; set; }
}