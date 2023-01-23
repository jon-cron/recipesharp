<template>
  <section class="recipe-img row align-content-between align-items-start elevation-5 selectable" v-if="recipe" :style="`background-image: url(${recipe.imgUrl})`">
<!-- <img class="recipe-img" :src="recipe.imgUrl" alt=""> -->
<div class="col-4 text-center text-white p-1">
  <h5 class="shadow-bg m-2">{{ recipe.category }}</h5>
</div>
<div>
  <i class="mdi mdi-heart-outline fs-5 btn" title="Favorite"></i>
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
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe.js";
export default {
  props: {recipe: {type: Recipe, required: true}},
  setup(props){
  return {  }
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
  background-color: rgba(27, 25, 25, 0.342);
  width: 100%;
  border-radius: 9pt;

}
.row{
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