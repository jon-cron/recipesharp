<template>
  <div class="fixed-div">
    <section class="recipe-img row align-content-between align-items-start elevation-5 selectable" data-bs-toggle="modal" data-bs-target="#recipe-modal" @click="setToActive" v-if="recipe" :style="`background-image: url(${recipe.imgUrl})`">
      <!-- <img class="recipe-img" :src="recipe.imgUrl" alt=""> -->
      <div class="col-4 text-center text-white p-1">
        <h5 class="shadow-bg p-1 m-2">{{ recipe.category }}</h5>
      </div>
      <div>
      </div>
      <div class="col-12 text-white">
        <div class="shadow-bg ps-2">
          <h4>
            {{ recipe.title }}
          </h4>
          <p>
            {{ recipe.instructions }}
          </p>
        </div>
      </div>
    </section>
    <i v-if="recipe.isFavorite == false && account.id" class="mdi mdi-heart-outline text-danger fs-5 btn selectable" title="Favorite" @click="handleFavorite(recipe)"></i>
    <i v-else-if="recipe.isFavorite == true && account.id" class="mdi mdi-heart text-danger fs-5 btn selectable" title="Favorite" @click="handleFavorite(recipe)"></i>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { ingredientsService } from "../services/IngredientsService.js";
import { favoritesService } from "../services/FavoritesService.js/";
import { recipesService } from "../services/RecipesService.js";

export default {
  props: {recipe: {type: Recipe, required: true}},
  setup(props){
    onMounted(()=>{
      // getRecipeFavorites()
    })
    // NOTE this works kinda.
    // async function getRecipeFavorites(){
    //   try {
    //     let recipe = props.recipe.id
    //     await recipesService.getRecipeFavorites(recipe)
    //   } catch (error) {
    //     Pop.error(error)
    //   }
    // }
    return {
      account: computed(()=> AppState.account),
    favorites: computed(()=> AppState.favorites),

   async setToActive(){
    try {
      AppState.recipe = props.recipe
      await ingredientsService.getIngredients(AppState.recipe.id)
    } catch (error) {
      Pop.error(error)
      logger.log(error)
    }
    },
   async handleFavorite(recipe){
      try {
        // const id = {recipe.id}
        if(recipe.isFavorite){
          await favoritesService.removeFavorite(recipe.id)
          // logger.log(recipe.id)
        } else{
          let favData = {recipeId: recipe.id}
          await favoritesService.postFavorite(favData)
        }
      } catch (error) {
        Pop.error(error)
        logger.log(error)
      }
    }
  }
  }
};
</script>


<style lang="scss" scoped>
.recipe-img{
width: 40vh;
height: 40vh;
background-size: cover;
background-position: center;
}
.shadow-bg{
  background-color: #BBBBBB;
  width: 100%;
  border-radius: 9pt;

}
.fixed-div{
  position: relative;
  // display: flex;

  .btn{
    position: absolute;
    top: 0;
    right: 0;
  }
}
p{
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>