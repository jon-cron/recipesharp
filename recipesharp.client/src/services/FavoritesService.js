import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class FavoritesService{

async getFav(){
    const res = await api.get("account/favorites")
    logger.log("getting favorites", res.data)
  }
async postFav(favData){
const res = await api.post(`api/favorites`, favData)
logger.log("Making favorite",res.data)
}
async deleteFav(id){
const res = await api.delete(`api/favorites${id}`)
logger.log("favorite deleted", res.data)
}

}

export const favoritesService = new FavoritesService()