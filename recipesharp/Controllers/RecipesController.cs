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
}
