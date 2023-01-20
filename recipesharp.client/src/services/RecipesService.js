import { logger } from "../utils/Logger.js";
import { api } from './AxiosService'
import { AppState } from "../AppState.js"


class RecipesService{
  async getRecipes(){
  const res = await api.get("api/recipes");
  AppState.recipes = res.data
  logger.log(AppState.recipes)
}
}

export const recipesService = new RecipesService();