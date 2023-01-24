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
    favoritesService.getFav()

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
async getRecipeFavorites(id){
  const res = await api.get(`api/recipes/${id}/favorites`)
  AppState.fav = res.data
  logger.log("Recipe favorites",AppState.fav)
}
async editRecipe(recipeData){
  // logger.log(recipeData)
  const res = await api.put(`api/recipes/${recipeData.recipeId}`, recipeData)
  AppState.recipe = new Recipe(res.data)
  logger.log(res.data)
}
async search(searchData){
  // logger.log(searchData)
  const res = await api.get(`api/recipes/results/` + searchData.search)
  // logger.log(res.data)
  AppState.recipes = res.data.map(r => new Recipe(r))
}
}

export const recipesService = new RecipesService();

// async editInstructions(){
//   try {
//     editable.value.recipeId = this.recipe.id
//     await recipesService.editRecipe(editable.value)
//     editable.value = {}
//   } catch (error) {
//     Pop.error(error)
//   }
// },