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
        await connection.CreateTableAsync<Ingredient>();
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
        await SaveRecipeIngredientsAsync(recipe.Ingredients, recipe.Id);
        return recipe;
    }

    //Update a recipe
    public async Task<Recipe> UpdateRecipeAsync(Recipe recipe)
    {
        await UpdateRecipeIngredientsAsync(recipe.Ingredients, recipe.Id);
        await connection.UpdateAsync(recipe);
        return recipe;
    }
    
    // delete a recipe and it's ingredients
    public async Task<Recipe> DeleteRecipeAsync(Recipe recipe)
    {
        await DeleteAllRecipeIngredientsAsync(recipe.Id);
        await connection.DeleteAsync(recipe);
        return recipe;
    }

    //Fetches ingredients
    public async Task<List<RecipeIngredient>> GetRecipeIngredientAsync(int recipeId)
    {
        await InitAsync();
        return await connection.Table<RecipeIngredient>().Where(i => i.RecipeId == recipeId).ToListAsync();
    }

    //Creates a new Ingredient to the database
    public async Task<List<RecipeIngredient>> SaveRecipeIngredientsAsync(List<RecipeIngredient> ingredients, int recipeId)
    {
        foreach (var ingredient in ingredients)
        {
            ingredient.RecipeId = recipeId;
            await connection.InsertAsync(ingredient);
        }
        return ingredients;
    }

    //Update a Ingredient
    public async Task<List<RecipeIngredient>> UpdateRecipeIngredientsAsync(List<RecipeIngredient> ingredients, int recipeid)
    {
        await DeleteAllRecipeIngredientsAsync(recipeid);
        return await SaveRecipeIngredientsAsync(ingredients, recipeid);
    }

    // delete an ingredient
    public async Task<RecipeIngredient> DeleteRecipeIngredientAsync(RecipeIngredient ingredient)
    {
        await connection.DeleteAsync(ingredient);
        return ingredient;
    }

    // delete all ingredients of a recipe to be deleted
    public async Task DeleteAllRecipeIngredientsAsync(int recipeId)
    {
        await InitAsync();
        await connection.Table<RecipeIngredient>().DeleteAsync(i => i.RecipeId == recipeId);
    }

    /*
     * 
     * Ingredient methods
     * 
     */
    public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
    {
        await connection.InsertAsync(ingredient);
        return ingredient;
    }

    public async Task<Ingredient> GetIngredientAsync(int id)
    {
        return await connection.GetAsync<Ingredient>(id);
    }

    public async Task<List<Ingredient>> GetAllIngredientsAsync()
    {
        await InitAsync();
        return await connection.Table<Ingredient>().ToListAsync();
    }

    public async Task<Ingredient> UpdateIngredientAsync(Ingredient ingredient)
    {
        await connection.UpdateAsync(ingredient);
        return ingredient;
    }

    public async Task DeleteIngredientAsync(Ingredient ingredient)
    {
        await connection.DeleteAsync(ingredient);
    }
}
