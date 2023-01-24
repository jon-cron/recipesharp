namespace recipesharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;

    private readonly Auth0Provider _auth0Provider;

    private readonly IngredientsService _ingredientsService;

    private readonly FavoritesService _favoritesService;


  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService, FavoritesService favoritesService)
  {
    _ingredientsService = ingredientsService;
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
    _favoritesService = favoritesService;
  }

  [HttpGet]
  // [Authorize]
  
  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try 
    {
      List<Recipe> recipes = _recipesService.GetAllRecipes();
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("results/{search}")]
  // [Authorize]
  
  public ActionResult<List<Recipe>> GetSearch(string search)
  {
    try 
    {
      List<Recipe> recipes = _recipesService.GetSearch(search);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}")]
  [Authorize]

  public async Task<ActionResult<Recipe>> GetOneRecipeById(int id)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipesService.GetOneRecipeById(id, userInfo.Id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.CreateRecipe(recipeData, userInfo?.Id);
      recipe.Creator = userInfo;
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]

  public async Task<ActionResult<string>> RemoveRecipe(int id)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipesService.RemoveRecipe(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}/ingredients")]
  // [Authorize]

  public async Task<ActionResult<List<Ingredient>>> GetIngredientsByRecipeId(int id)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(id, userInfo?.Id);
      return Ok(ingredients);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}/favorites")]
  // [Authorize]
  public async Task<ActionResult<List<Favoritor>>> GetFavoritesByRecipeId(int id)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Favoritor> favorites = _favoritesService.GetFavoritesByRecipeId(id, userInfo.Id);
      return Ok(favorites);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPut("{id}")]
  [Authorize]

  public async Task<ActionResult<Recipe>> EditRecipe([FromBody] Recipe recipeData,int id)
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.Id = id;
      Recipe recipe = _recipesService.EditRecipe(recipeData, userInfo?.Id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
