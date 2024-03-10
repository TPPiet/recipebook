using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    internal class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required double Amount { get; set; }
        public required string Unit { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
}
