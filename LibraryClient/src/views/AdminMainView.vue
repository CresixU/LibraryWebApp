<template>
    <div class="row nav w-100">
        <div class="col-3"></div>
        <div class="col-9 row">
          <router-link exact-active-class="active" to="/panel/users" class="col nav-element">
            <MainNavElementItem classProp="bi bi-people">Users</MainNavElementItem>
          </router-link>
          <router-link exact-active-class="active" to="/panel/books" class="col nav-element">
            <MainNavElementItem classProp="bi bi-journal">Books</MainNavElementItem>
          </router-link>
          <router-link exact-active-class="active" to="/panel/rents" class="col nav-element">
            <MainNavElementItem classProp="bi bi-card-list">Rents</MainNavElementItem>
          </router-link>
          <router-link exact-active-class="active" to="/admin" class="col nav-element">
            <MainNavElementItem classProp="bi bi-person-gear">Panel</MainNavElementItem>
          </router-link>
        </div>
      </div>
      <div class="row px-2 py-4">
        <router-view></router-view>
      </div>
</template>
<script>

import MainNavElementItem from '../components/MainNavElementItem.vue';

export default {
  components: {
    MainNavElementItem,
  }
}
</script>
<style scroped>
.active > *{
  color: #10d7e1
}
</style>
