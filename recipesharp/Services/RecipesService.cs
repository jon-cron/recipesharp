namespace recipesharp.Services;

public class RecipesService
{
    private readonly RecipeRepository _repo;

  public RecipesService(RecipeRepository repo)
  {
    _repo = repo;
  }

  internal List<Recipe> GetAllRecipes(string userId)
  {
    if(userId == null){
      throw new Exception("you are not logged in");
    }
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }
}
