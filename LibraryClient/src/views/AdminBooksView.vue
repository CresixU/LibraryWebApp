<template>
    <div class="col filters">
        <h3 class="text-center">Filters</h3>
        <SearchBox class="my-3" @searchPhrase="handleSearchPhrase"/>
        <Categories class="my-3" @category="handleSelect" />
        <div>
          <button class="btn-main my-3 d-block px-4" style="margin-left: auto"
          @click="handleButtonClick">Search</button>
        </div>
      </div>
      <div class="col">
        <MainBooksTable :searchPhrase="searchPhraseTable" :categoryProp="categoryTable"/>
      </div>
</template>
<script>
import Categories from '../components/filters/Categories.vue';
import SearchBox from '../components/filters/SearchBox.vue';
import MainBooksTable from '../components/MainBooksTable.vue';

export default {
  components: {
    Categories,
    SearchBox,
    MainBooksTable,
  },
  data() {
    return {
      searchPhrase: '',
      searchPhraseTable: '',
      category: '',
      categoryTable: ''
    }
  },
  methods: {
    handleSearchPhrase(searchPhrase) {
      this.searchPhrase = searchPhrase
    },
    handleSelect(category) {
      this.category = category
    },
    handleButtonClick() {
      this.searchPhraseTable = this.searchPhrase
      this.categoryTable = this.category
    }
  }
}
</script>