using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RecipeBook.Data;

public class Database
{
    string _dbPath;
    public string StatusMessage { get; set; }
    private SQLiteAsyncConnection connection;
    public Database(string dbPath) => _dbPath = dbPath;
    
    //Create database and recipe table
    private async Task InitAsync()
    {
        //Dont create database if it exists
        if (connection != null)
            return;
        //Create database and Recipe table
        connection = new SQLiteAsyncConnection(_dbPath);
        await connection.CreateTableAsync<Recipe>();
        await connection.CreateTableAsync<RecipeIngredient>();
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
        await SaveIngredientsAsync(recipe.Ingredients, recipe.Id);
        return recipe;
    }

    //Update a recipe
    public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
    {
        await UpdateIngredientsAsync(recipe.Ingredients, recipe.Id);
        await connection.UpdateAsync(recipe);
        return recipe;
    }
    
    // delete a recipe and it's ingredients
    public async Task<Recipe> DeleteRecipeAsync(Recipe recipe)
    {
        await DeleteAllIngredientsAsync(recipe.Id);
        await connection.DeleteAsync(recipe);
        return recipe;
    }

    //Fetches ingredients
    public async Task<List<RecipeIngredient>> GetIngredientAsync(int recipeId)
    {
        await InitAsync();
        return await connection.Table<RecipeIngredient>().Where(i => i.RecipeId == recipeId).ToListAsync();
    }

    //Creates a new Ingredient to the database
    public async Task<List<RecipeIngredient>> SaveIngredientsAsync(List<RecipeIngredient> ingredients, int recipeId)
    {
        foreach (var ingredient in ingredients)
        {
            ingredient.RecipeId = recipeId;
            await connection.InsertAsync(ingredient);
        }
        return ingredients;
    }

    //Update a Ingredient
    public async Task<List<RecipeIngredient>> UpdateIngredientsAsync(List<RecipeIngredient> ingredients, int recipeid)
    {
        await DeleteAllIngredientsAsync(recipeid);
        return await SaveIngredientsAsync(ingredients, recipeid);
    }

    // delete an ingredient
    public async Task<RecipeIngredient> DeleteIngredientAsync(RecipeIngredient ingredient)
    {
        await connection.DeleteAsync(ingredient);
        return ingredient;
    }

    // delete all ingredients of a recipe to be deleted
    public async Task DeleteAllIngredientsAsync(int recipeId)
    {
        await InitAsync();
        await connection.Table<RecipeIngredient>().DeleteAsync(i => i.RecipeId == recipeId);
    }
}
