using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RecipeBook.Data;

public class RecipeDatabase
{
    string _dbPath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection connection;
    public RecipeDatabase(string dbPath) => _dbPath = dbPath;
    
    //Create database and recipe table
    private async Task InitAsync()
    {
        //Dont create database if it exists
        if (connection != null)
            return;
        //Create database and Recipe table
        connection = new SQLiteAsyncConnection(_dbPath);
        await connection.CreateTableAsync<Recipe>();
    }

    //Fetches recipes
    public async Task<List<Recipe>> GetRecipeAsync()
    {
        await InitAsync();
        return await connection.Table<Recipe>().ToListAsync();
    }

    //Creates a new Recipe to the database
    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        await connection.InsertAsync(recipe);
        return recipe;
    }

    //Update a recipe
    public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
    {
        await connection.UpdateAsync(recipe);
        return recipe;
    }

    public async Task<Recipe> DeleteRecipeAsync(Recipe recipe)
    {
        await connection.DeleteAsync(recipe);
        return recipe;
    }
}
