import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService{
async getIngredients(id){
const res = await api.get(`api/recipes/${id}/ingredients`)
AppState.ingredients = res.data
logger.log("getting ingredients",AppState.ingredients)
}
}

export const ingredientsService = new IngredientsService()