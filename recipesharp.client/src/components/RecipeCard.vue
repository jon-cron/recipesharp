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
    <i class="mdi mdi-heart-outline fs-5 btn selectable" title="Favorite" @click="handleFavorite(recipe.id)"></i>
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

export default {
  props: {recipe: {type: Recipe, required: true}},
  setup(props){
  return {
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
    handleFavorite(recipeId){
      try {
        const isFav = this.favorites.find(f => f.recipeId == recipeId)
        logger.log(isFav)
        
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