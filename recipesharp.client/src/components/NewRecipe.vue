<template>
<div class="offcanvas offcanvas-start" tabindex="-1" id="new-recipe" aria-labelledby="offcanvasExampleLabel">
  <div class="offcanvas-header">
    <h5 class="offcanvas-title" id="offcanvasExampleLabel">New Recipe</h5>
    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
  </div>
  <div class="offcanvas-body">
    <form @submit.prevent="createRecipe">
  <div class="form-group my-1">
    <label for="title">Recipe title</label>
    <input v-model="editable.title" style="text-transform:uppercase" required minlength="10" type="text" class="form-control" id="title" placeholder="Enter title here...">
  </div>
  <div class="form-group my-1">
    <label for="instructions">Recipe Instructions</label>
    <input v-model="editable.instructions" required minlength="50" type="text" class="form-control" id="instructions" placeholder="Enter instructions here...">
  </div>
  <div class="form-group my-1">
    <label for="imgUrl">Recipe Image</label>
    <input v-model="editable.imgUrl" required type="text" class="form-control" id="imgUrl" placeholder="Image Url here...">
  </div>
  <div class="form-group my-1">
    <label for="category">Category</label>
    <select v-model="editable.category" class="form-select form-select-sm" aria-label=".form-select-sm example">
  <option selected>Open this select category</option>
  <option value="American">American</option>
  <option value="Appetizer">Appetizer</option>
  <option value="Baking">Baking</option>
  <option value="BBQ">BBQ</option>
  <option value="Breakfast">Breakfast</option>
  <option value="Dinner">Dinner</option>
  <option value="Drinks">Drinks</option>
  <option value="Desserts">Dessert</option>
  <option value="French">French</option>
  <option value="Indian">Indian</option>
  <option value="Italian">Italian</option>
  <option value="Lunch">Lunch</option>
  <option value="Mexican">Mexican</option>
  <option value="Salads">Salads</option>
  <option value="Soups">Soups</option>
  <option value="Misc">Misc</option>
</select>
  </div>
  <button class="btn btn-success mt-2">Submit</button>
</form>
  </div>
</div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { recipesService } from "../services/RecipesService.js";
export default {
  setup(){
    const editable = ref({})
  return {
    editable,
    async createRecipe(){
      try {
        await recipesService.createRecipe(editable.value)
        editable.value = {}
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

</style>