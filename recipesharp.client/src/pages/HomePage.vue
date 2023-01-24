<template>
  <div>
    <span class="row justify-content-center fixed-row align-content-center elevation-5 text-center">
  <div class="col-3"><h6 class="selectable" @click="getRecipes" >Home</h6></div>
  <div class="col-4"><h6 class="selectable" @click="myRecipes">My Recipes</h6></div>
  <div class="col-3"><h6 class="selectable" @click="filterFav">Favorites</h6></div>
</span>

    <section class="row justify-content-evenly mt-5 mx-0">
      <div class="col-12 col-lg-3 col-md-6 d-flex justify-content-center p-1 my-3" v-for="r in recipes">
        <RecipeCard :recipe = "r"/>
      </div>
    </section>
    <i v-if="account.id" class="btn fixed-btn mdi text-white mdi-plus fs-2 selectable" title="New Recipe" type="button" data-bs-target="#new-recipe" data-bs-toggle="offcanvas"></i>
  </div>
</template>

<script>
import { onMounted, computed } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipesService } from "../services/RecipesService.js"
import { AppState } from "../AppState.js"
import RecipeCard from "../components/RecipeCard.vue"
import { favoritesService } from "../services/FavoritesService.js";

export default {
  setup() {
    onMounted(()=>{
      // getRecipes()
    })
    async function getRecipes(){
       try {
         await recipesService.getRecipes()
       } catch (error) {
         Pop.error(error)
         logger.log(error)
       }
     }
    return {
      myRecipes(){
        let filteredArr = AppState.recipes.filter(r => r.creatorId == this.account.id)
        AppState.recipes = filteredArr
      },
      filterFav(){
       let filteredArr = AppState.recipes.filter(r => r.isFavorite == true)
        AppState.recipes = filteredArr
      },
      async getRecipes(){
         try {
           await recipesService.getRecipes()
         } catch (error) {
           Pop.error(error)
           logger.log(error)
         }
       },
      recipes: computed(()=> AppState.recipes),
      account: computed(()=> AppState.account),
      favorites: computed(()=> AppState.favorites),
      recipe: computed(()=> AppState.recipe)
    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;

  .home-card {
    width: 50vw;
  }
}
.fixed-btn{
    position: fixed;
    bottom: 50px;
    right: 50px; 
    background-color: #527360;
    border-radius: 45em;
  }
  .bg-btn{
    background-color: green($color: #2f811e) !important;
  }
  div{
    position: relative;

    span{
      position: absolute;
      top: -60pt;
      left: 580pt
    }
  }
  .fixed-row{
    background-color: white;
    width: 40vh;
    height: 40pt;
    color: #51bb3c;
  }

</style>
