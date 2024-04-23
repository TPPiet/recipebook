using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class Instruction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string InstructionText { get; set; }

        [Ignore]
        public List<Ingredient> Ingredients { get; set; }
    }
}
