<template>
  <div v-if="recipe" class="modal fade" id="recipe-modal" tabindex="-1" role="dialog" aria-hidden="true">
  <div class="modal-dialog modal-xl" role="document">
    <div class="modal-content bg-white">
      <section class="row">
        <div class="col-5"><img :src="recipe.imgUrl" class="img-fluid rounded-start" alt="">
        </div>
        <div class="col-7">
          <section class="row justify-content-between">
            <div class="col-10 d-flex align-items-end justify-content-start p-2">
              <h2 class="">{{ recipe.title }}</h2>
              <div>
                <h6 class="shadow-bg p-1 ms-2">{{ recipe.category }}</h6>
              </div>
            </div>
            <div class="col-1">
              <i v-if="recipe.creatorId == account.id" @click="removeRecipe(recipe.id)" class="mdi mdi-delete text-danger btn selectable p-0 fs-5"></i>
            </div>
          </section>
          <!-- SECTION bottom right portion of recipe modal -->
          <section class="row justify-content-evenly mt-3">
            <div class="col-5 card-bg rounded">
              <section class="row title-bg rounded-top">
                <h3>Recipe Steps</h3>
              </section>
              <section class="row">
                <h6>{{ recipe.instructions }}</h6>
              </section>
              <section class="row justify-content-center" v-if="account.id == recipe.creatorId">
                <form>
                  <div class="d-flex search-box">
                    <input v-model="editable.instructions" class="bg-white search" type="text" placeholder="instruction">
                    <button class="bg-white search-btn"><i class="mdi mdi-plus bg-white" ></i></button>
                  </div>
                </form>
              </section>
            </div>
            <div class="col-5  card-bg pb-2 rounded">
              <section class="row title-bg rounded-top">
                <h3>Ingredients</h3>
              </section>
              <section class="row">
                <div class="col-12 d-flex"  v-if="ingredients.length > 0" v-for="i in ingredients">
                  <h6>{{ i.quantity }} {{ i.name }}</h6><button class="btn"><i v-if="recipe.creatorId == account.id" class="mdi mdi-delete" @click="removeIngredient(i)"></i></button>
                </div>
              </section>
              <section class="row" v-if="account.id == recipe.creatorId">
                <form class="p-0" @submit.prevent="createIngredient">
                  <div class="search-box">
                    <input v-model="editable.quantity" class="w-25 bg-white search" type="text" placeholder="quantity">
                    <input v-model="editable.name" class="w-50 bg-white search" type="text" placeholder="add ingredients">
                    <!-- <input v-model="editable.recipeId" class="display-none" type="text" placeholder="add ingredients"> -->
                    <button class="bg-white search-btn"><i class="mdi mdi-plus bg-white" ></i></button>
                  </div>
                </form>
              </section>
            </div>
          </section>
        </div>
      </section>
    </div>
  </div>
</div>

</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { ingredientsService } from "../services/IngredientsService.js";
import { recipesService } from "../services/RecipesService.js";
export default {
  setup(){
    const editable = ref({})
    // onMounted(()=>{
    //   getIngredients()
    // })
    // async function getIngredients(){
    //   try {
    //     const id = recipe.id
    //     await ingredientsService.getIngredients(id)
    //   } catch (error) {
    //     Pop.error(error)
    //     logger.log(error)
    //   }
    //  }
  return {
    editable,
    recipe: computed(()=> AppState.recipe),
    ingredients: computed(()=> AppState.ingredients),
    account: computed(()=> AppState.account),
    async createIngredient(){
      try {
        editable.value.recipeId = this.recipe.id
        await ingredientsService.createIngredient(editable.value)
        editable.value = {}
      } catch (error) {
        Pop.error(error)
        logger.log(error)
      }
    },
   async removeIngredient(i){
      try {
        const id = i.id
        if(await Pop.confirm("Are you sure?")){
          await ingredientsService.removeIngredient(id)
        }
      } catch (error) {
        Pop.error(error)
        logger.log(error)
      }
    },
   async removeRecipe(recipeId){
    try {
      if( await Pop.confirm("Are you sure?")){
        await recipesService.removeRecipe(recipeId)
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
.display-none{
  display: hidden;
}
.shadow-bg{
  background-color: #BBBBBB;
  width: 100%;
  border-radius: 9pt;
  color: white;
}
.img-fluid{
  height: 45vh;
  width: 100%;
  object-fit: cover;
}
.modal-sz{
  width: 55em !important;
  position: center;
}
.search{
  border-top: .5pt solid white;
  border-left: .5pt solid white;
  border-bottom: .5pt solid white;
  border-right: none;
  border-right: none;
  border-top-left-radius: 3pt;
  border-bottom-left-radius: 3pt;
}
.search-btn{
  border-top: .5pt solid white;
  border-bottom: .5pt solid white;
  border-right: .5pt solid white;
  border-left: none;
  border-top-right-radius: 3pt;
  border-bottom-right-radius: 3pt;
}
.search-box{
  height: 100%pt;
  width: 100%;
}
.card-bg{
  background-color: #f0f4f2;
}
.title-bg{
  background-color: #527360;
  color: white;
  text-align: center;
}
</style>