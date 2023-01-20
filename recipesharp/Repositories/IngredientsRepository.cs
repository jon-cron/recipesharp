namespace recipesharp.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    throw new NotImplementedException();
  }
}
