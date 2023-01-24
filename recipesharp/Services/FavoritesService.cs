namespace recipesharp.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;
    private readonly RecipesService _recipesService;

  public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  internal int CreateFav(Favorite favoriteData)
  {
    Recipe recipe = _recipesService.GetOneRecipeById(favoriteData.recipeId, favoriteData.accountId);
    int id = _repo.CreateFav(favoriteData);
    return id;
  }

  internal List<Favoritor> GetFavoritesByRecipeId(int recipeId, string accountId)
  {
    Recipe recipe = _recipesService.GetOneRecipeById(recipeId, accountId);
    List<Favoritor> favorites = _repo.GetFavoritesByRecipeId(recipeId);
    return favorites;
  }

  internal string RemoveFavorite(int id, string userId)
  {
    Favorite favorite = _repo.GetOneFavorite(id);
    if(favorite == null){
      throw new Exception("still favorited");
    }
   bool results = _repo.RemoveFavorite(id);
   if(results == false) throw new Exception("not member at id");

      return "unfavorited";
  }
  internal Favorite GetOneFavorite(int id)
  {
    Favorite favorite = _repo.GetOneFavorite(id);
    return favorite;
  }

  internal List<Favorite> GetMyFav(string accountId)
  {
    List<Favorite> favorites = _repo.GetMyFav(accountId);
    return favorites;
  }
}
