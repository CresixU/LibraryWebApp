<template>
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
</template>
<script>
import Categories from '../components/filters/Categories.vue';
import SearchBox from '../components/filters/SearchBox.vue';
import BookTable from '../components/BookTable.vue';

export default {
  components: {
    Categories,
    SearchBox,
    BookTable,
  }
}
</script>