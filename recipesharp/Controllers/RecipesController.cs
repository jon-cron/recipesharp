namespace recipesharp.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;

    private readonly Auth0Provider _auth0Provider;

  public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider)
  {
    _recipesService = recipesService;
    _auth0Provider = auth0Provider;
  }

  [HttpGet]
  [Authorize]
  
  public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
  {
    try 
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _recipesService.GetAllRecipes(userInfo?.Id);
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
}
