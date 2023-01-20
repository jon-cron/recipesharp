namespace recipesharp.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repo;

    private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepository repo, RecipesService recipesService)
  {
    _recipesService = recipesService;
    _repo = repo;
    
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repo.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId, string userId)
  {

    // Recipe recipe = _recipesService.GetOneRecipeById(recipeId, userId);
    // recipeId = recipe.Id;
    if(userId == null )
    {
      throw new Exception("please log in");
    }
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    return ingredients;
  }

  internal string RemoveIngredient(int id)
  {
    bool deleted = _repo.Remove(id);
    if(deleted)
    {
      return "Ingredient has been deleted";
    }
    return "no ingredient by that Id";
  }
}
