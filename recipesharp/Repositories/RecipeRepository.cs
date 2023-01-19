namespace recipesharp.Repositories;

public class RecipeRepository
{
    private readonly IDbConnection _db;

  public RecipeRepository(IDbConnection db)
  {
    _db = db;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT
    re.*,
    ac.*
    FROM recipes re
    JOIN accounts ac ON ac.id = re.creatorId;
    ";
  List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) => 
  {
    recipe.Creator = account;
    return recipe;
  }).ToList();
  return recipes;
  }
}
