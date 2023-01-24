import { AppState } from "../AppState.js"
import { Favorite } from "../models/Favorite.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService{

async getFav(){
    const res = await api.get("account/favorites")
    AppState.myFavorites = []
   AppState.myFavorites = res.data.map(f => new Favorite(f))
    logger.log("[my Favorites]",AppState.myFavorites)
  this.decideFav()
  }

async postFavorite(favoriteData){
const res = await api.post('api/favorites', favoriteData)
this.getFav()
// logger.log(favoriteData)
// const fav = AppState.recipes.findIndex(r => r.id == favoriteData.recipeId)
// fav.isFavorite = true
// AppState.favorites.push(res.data)
// logger.log("Making favorite",AppState.favorites)
}
async removeFavorite(recipeId){
 const favorite = AppState.myFavorites.find(f => f.recipeId == recipeId)
const res = await api.delete(`api/favorites/${favorite.id}`)
this.getFav()
// const unFav = AppState.recipes.find(r => r.id == id)
// unFav.isFavorite = false
// const rIndex =AppState.recipes.findIndex(r => r.id == id)
// AppState.recipes = AppState.recipes.splice(rIndex, 1, unFav)
// const fIndex = AppState.favorites.findIndex(f => f.recipeId == id)
// AppState.favorites.splice(fIndex, 1)
// logger.log("favorite deleted", AppState.favorites, res.data)
}
decideFav(){
  for (let i = 0; i < AppState.myFavorites.length; i++) {
    let isFound = AppState.recipes.find(r => r.id == AppState.myFavorites[i].recipeId)
     if(isFound){
       isFound.isFavorite = true
     }
   }
}
}

export const favoritesService = new FavoritesService()