import { logger } from "../utils/Logger.js";
import { api } from './AxiosService'
import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js";
import { favoritesService } from "../services/FavoritesService.js";
import { AuthService } from "./AuthService.js";


class RecipesService{
  async getRecipes(){
  const res = await api.get("api/recipes");
  AppState.recipes = res.data.map(r => new Recipe(r))
  logger.log(AppState.recipes)
  // await AuthService.getFav()
  // setTimeout(()=>{
    favoritesService.getFav()
  // }, 700)

}
async createRecipe(recipeData){
  const res = await api.post("api/recipes", recipeData);
  AppState.recipes.push(new Recipe(res.data))
  logger.log("Current Recipes plus New Recipe", AppState.recipes)
}
async removeRecipe(id){
  const res = await api.delete(`api/recipes/${id}`)
  logger.log(res.data)
  AppState.recipes = AppState.recipes.filter(r => r.id != id)
}
}

export const recipesService = new RecipesService();

// Slice bananas into chips. Combine flour, salt, buttermilk, eggs, and baking powder in a large bowl. Heat skillet on medium. Pour a 1/4 cup of mixture into skillet.