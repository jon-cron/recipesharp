<template>
  <nav class="bg-img container-fluid p-2 fixed-nav elevation-7"> 
    <section class="row justify-content-end">
      <div class="col-3 d-flex justify-content-between">
        <div class="d-flex align-items-center justify-content-evenly" >
            <form action="" class="d-flex search-box" @submit.prevent="search">
              <input v-model="editable.search" class="bg-white search" type="text" placeholder="search...">
              <button class="bg-white search-btn"><i class="mdi mdi-magnify bg-white" ></i></button>
            </form>
          </div>
          <!-- LOGIN COMPONENT HERE -->
          <Login class="elevation-5"/>
        </div>
</section>
<section class="row justify-content-center">
  <div class="col-4 text-center text-white title-bg">
    <h1>Recipe</h1>
    <h6 class="text-center">A recipe has no soul. You as the cook must bring soul to the recipe.</h6>
    <p class="text-end">-Thomas Keller</p>
  </div>
</section>

  </nav>
</template>

<script>
import { ref } from "vue";
import { recipesService } from "../services/RecipesService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import Login from './Login.vue'
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async search(){
        try {
          // logger.log("search", editable.value)
          await recipesService.search(editable.value)
          editable.value = {}
        } catch (error) {
          Pop.error
        }
      }
    }
  },
  components: { Login }
}
</script>

<style scoped>


.input-group{
height: 3em;
}
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
.row{
  margin: 0;
}
.nav-sz{
  width: 100%;
  margin: 0;
  padding: 0;
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
  height: 30pt;
}
.bg-img{
  background-image: url('https://mir-s3-cdn-cf.behance.net/project_modules/fs/03bbd256340483.59aa4a0db2ffd.jpg');
  height: 36vh;
  margin-top: 15pt;
  width: 97%;
  background-position: center;
  background-size: cover;
}
.title-bg{
  background-color: rgba(31, 28, 28, 0.61);
}
h1{
  font-family: Arial, Helvetica, sans-serif;
}
.elevation-7 {
  box-shadow: 2pt 3pt 8pt rgba(53, 50, 50, 0.473);
}
/* .elevation-7 {
  box-shadow: 0 2px 5px -1px rgba(0, 0, 0, 0.2), 0 5px 6px 0 rgba(0, 0, 0, 0.14), 0 1px 12px 0 rgba(0, 0, 0, 0.12);
} */

</style>
