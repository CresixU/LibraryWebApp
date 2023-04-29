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
      <MainNavElementItem classProp="bi bi-people">Users</MainNavElementItem>
      <MainNavElementItem classProp="bi bi-journal">Books</MainNavElementItem>
      <MainNavElementItem classProp="bi bi-card-list">Rents</MainNavElementItem>
    </div>
    <div class="row px-2 py-4">
      <BooksView></BooksView>
      <!--
      <div class="col filters">
        <h3 class="text-center">Filters</h3>
        <SearchBox class="my-3"/>
        <Categories class="my-3"/>
        <div>
          <button class="btn-main my-3 d-block px-4" style="margin-left: auto">Search</button>
        </div>
      </div>
      <div class="col">
        <BookTable/>
      </div>
      -->
    </div>
  </main>
  <footer>

  </footer>

</div>
</template>

<script>


import Categories from './components/filters/Categories.vue';
import SearchBox from './components/filters/SearchBox.vue';
import BookTable from './components/BookTable.vue';
import MainNavElementItem from './components/MainNavElementItem.vue';

import BooksView from './views/BooksView.vue'

export default {
  components: {
    Categories,
    SearchBox,
    BookTable,
    MainNavElementItem,
    BooksView
  }
}
</script>

<style scoped>
</style>
