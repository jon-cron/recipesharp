namespace recipesharp.Services;

public class RecipesService
{
    private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }
  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetOneRecipeById(int id, string userId)
  {
    Recipe recipe = _repo.GetOneRecipeById(id);
    if(userId == null)
    {
      throw new Exception("please log in");
    }
    if(recipe == null)
    {
      throw new Exception("no recipe by this id");
    }
    return recipe;
  }
    internal Recipe CreateRecipe(Recipe recipeData, string userId)
  {
    if(userId == null){
      throw new Exception("you must be logged in");
    }
    Recipe recipe = _repo.CreateRecipe(recipeData);
    return recipe;
  }

  internal string RemoveRecipe(int id, string userId)
  {
    Recipe original = GetOneRecipeById(id, userId);
    if(original.CreatorId != userId)
    {
      throw new Exception("not your recipe");
    }
    _repo.RemoveRecipe(id);
    return $"{original.Title} has been removed";
  }

  internal Recipe EditRecipe(Recipe recipeData, string userId)
  {
  Recipe original = GetOneRecipeById(recipeData.Id, userId);

  original.Title = recipeData.Title ?? original.Title;
  original.Instructions = recipeData.Instructions ?? original.Instructions;
  original.ImgUrl = recipeData.ImgUrl ?? original.ImgUrl;
  original.Category = recipeData.Category ?? original.Category;

  bool edited = _repo.EditRecipe(original);
  if(edited)
  {
    return original;
  }
  throw new Exception("recipe was not edited.");
  }

  internal List<Recipe> GetSearch(string search)
  {
    string NewSearch = "%" + search + "%";
    List<Recipe> recipes = _repo.GetSearch(NewSearch);
    return recipes;
  }
}
