<template>
<div>
  <header class="row">
    <div class="col-6 logo">
      <i class="bi bi-book-half" style="margin-right: 12px"></i>
      <span>Library App</span>
    </div>
    <div class="col-6">
    </div>
  </header>
  <main>
    <div class="row nav w-100">
      <div class="col"></div>
      <router-link to="/" class="col nav-element">
        <MainNavElementItem classProp="bi bi-people">Users</MainNavElementItem>
      </router-link>
      <router-link to="/books" class="col nav-element">
        <MainNavElementItem classProp="bi bi-journal">Books</MainNavElementItem>
      </router-link>
      <router-link to="/" class="col nav-element">
        <MainNavElementItem classProp="bi bi-card-list">Rents</MainNavElementItem>
      </router-link>
    </div>
    <div class="row px-2 py-4">
      <router-view></router-view>
    </div>
  </main>
  <footer>

  </footer>

</div>
</template>

<script>

import MainNavElementItem from './components/MainNavElementItem.vue';

export default {
  components: {
    MainNavElementItem,
  }
}
</script>

<style scoped>
</style>
