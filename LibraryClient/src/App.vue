<template>
<div>
  <header>
    Library Application
  </header>
  <nav>

  </nav>
  <aside>
    <div class="filters">
      <h3>Filtry</h3>
      <SearchBox/>
      <Categories/>

    </div>
  </aside>
  <main>
    
    <div class="data">
      <BookTable/>
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

export default {
  components: {
    Categories,
    SearchBox,
    BookTable
  }
}
</script>

<style scoped>
</style>
