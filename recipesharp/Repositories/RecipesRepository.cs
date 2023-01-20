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

  internal bool EditRecipe(Recipe original)
  {
    string sql = @"
    UPDATE recipes
    SET
    title = @title,
    instructions = @instructions,
    category = @category,
    imgUrl = @imgUrl
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;
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

  internal void RemoveRecipe(int id)
  {
    string sql = @"
    DELETE FROM recipes
    WHERE id = @id;
    ";
    _db.Execute(sql, new {id});
  }
}
