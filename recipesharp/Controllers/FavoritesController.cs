namespace recipesharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0Provider;

  public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
  {
    _favoritesService = favoritesService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Favoritor>> CreateFav([FromBody] Favorite favoriteData)
  {
    try 
    {
      Favoritor userInfo = await _auth0Provider.GetUserInfoAsync<Favoritor>(HttpContext);
      favoriteData.accountId = userInfo.Id;
      int id = _favoritesService.CreateFav(favoriteData);
      userInfo.FavoriteId = id;
      return Ok(userInfo);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]

  public async Task<ActionResult<string>> RemoveFavorite(int id)
  {
    try 
    {
      Favoritor userInfo = await _auth0Provider.GetUserInfoAsync<Favoritor>(HttpContext);
      string message = _favoritesService.RemoveFavorite(id, userInfo?.Id);
      return Ok(message);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
