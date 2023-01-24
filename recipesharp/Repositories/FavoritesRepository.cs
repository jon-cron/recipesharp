namespace recipesharp.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateFav(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites
    (recipeId, accountId)
    VALUES
    (@recipeId, @accountId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, favoriteData);
    return id;
  }



  internal List<Favoritor> GetFavoritesByRecipeId(int recipeId)
  {
    string sql = @"
    SELECT 
    ac.*,
    fa.id AS FavoriteId
    FROM favorites fa
    JOIN accounts ac ON ac.id = fa.accountId
    WHERE fa.recipeId = @recipeId;
    ";
    return _db.Query<Favoritor>(sql, new {recipeId}).ToList();
  }

  internal List<Favorite> GetMyFav(string accountId)
  {
    string sql = @"
    SELECT 
    *
    FROM favorites
    WHERE accountId = @accountId
    ";
    List<Favorite> favorites = _db.Query<Favorite>(sql, new{accountId}).ToList();
    return favorites;
  }

  internal Favorite GetOneFavorite(int id)
  {
    string sql = @"
    SELECT
    *
    FROM favorites
    WHERE id = @id;
    ";
    Favorite favorite = _db.Query<Favorite>(sql, new {id}).FirstOrDefault();
    return favorite;
  }
  internal bool RemoveFavorite(int id)
  {
    string sql = @"
    DELETE FROM favorites
    WHERE id = @id
    ";
   int rows = _db.Execute(sql, new {id});
   return rows > 0;

  }
}
