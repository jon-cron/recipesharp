namespace recipesharp.Repositories;

public class RecipesRepository
{
    private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
string sql = @"
INSERT INTO recipes
(title,instructions,`imgUrl`,category,`creatorId`)
VALUES
(@title, @instructions, @imgUrl, @category, @creatorId);
SELECT LAST_INSERT_ID();
";
int id = _db.ExecuteScalar<int>(sql, recipeData);
recipeData.Id = id;
return recipeData;
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

  internal Recipe GetOneRecipeById(int id)
  {
    string sql = @"
    SELECT
    re.*,
    ac.*
    FROM recipes re
    JOIN accounts ac ON ac.id = re.creatorId 
    WHERE re.id = @id;
    ";
    return _db.Query<Recipe, Account, Recipe>(sql, (recipe, account)=>{
      recipe.Creator = account;
      return recipe;
    }, new {id}).FirstOrDefault();
  }
}
