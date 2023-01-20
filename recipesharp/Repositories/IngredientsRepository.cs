namespace recipesharp.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (quantity, name, recipeId)
    VALUES
    (@quantity, @name, @recipeId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, ingredientData);
    ingredientData.id = id;
    return ingredientData;

  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE
    recipeId = @recipeId;
    ";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new {recipeId}).ToList();
    return ingredients;
  }

  internal bool Remove(int id)
  {
    string sql = @"
    DELETE FROM ingredients
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new {id});

    return rows > 0;
  }
}
