<template>
  <div class="container-fluid">
    <section class="row p-2">
      <router-link :to="{ name: 'Home'}">
        <div class="col-1 border border-dark">
          <button  class="btn"><i class="mdi mdi-arrow-left"></i> Home</button>
        </div>
      </router-link>
    </section>
    <section class="row">
      <div class="about text-center">
    <h1>Welcome {{ account.name }}</h1>
    <img class="rounded" :src="account.picture" alt="" />
    <p>{{ account.email }}</p>
  </div>
    </section>
  </div>

</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {

  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
