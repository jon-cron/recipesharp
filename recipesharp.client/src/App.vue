<template>
  <header>
    <Navbar/>
  </header>
  <main>
    <router-view />
  </main>
   <footer >
  </footer>
  <NewRecipe/>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from "./components/Navbar.vue"
import NewRecipe from "./components/NewRecipe.vue"

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, Navbar, NewRecipe }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
