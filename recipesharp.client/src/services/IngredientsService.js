import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class IngredientsService{
async getIngredients(id){
const res = await api.get(`api/recipes/${id}/ingredients`)
AppState.ingredients = res.data
// logger.log("getting ingredients",AppState.ingredients)
}

async createIngredient(ingredientData){
  // logger.log(ingredientData)
  const res = await api.post(`api/ingredients`, ingredientData)
  AppState.ingredients.push(res.data)
  // logger.log('adding ingredient', res.data)
}
async removeIngredient(id){
  const res = await api.delete(`api/ingredients/${id}`)
 const index = AppState.ingredients.findIndex(i => i.id = id)
  AppState.ingredients.splice(index, 1)
  // logger.log(res.data)
}
}

export const ingredientsService = new IngredientsService()