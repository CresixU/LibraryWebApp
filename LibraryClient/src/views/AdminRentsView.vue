<template>
    <div class="col filters">
        <h3 class="text-center">Filters</h3>
        <SearchBox class="my-3" @searchPhrase="handleSearchPhrase"/>
        <div>
          <button
           class="btn-main my-3 d-block px-4"
            style="margin-left: auto"
            v-on:click="handleButtonClick"
            >Search</button>
        </div>
      </div>
      <div class="col">
        <MainRentsTable :searchPhrase="searchPhraseTable"/>
      </div>
</template>
<script>
import SearchBox from '../components/filters/SearchBox.vue';
import MainRentsTable from '../components/MainRentsTable.vue';

export default {
  components: {
    SearchBox,
    MainRentsTable
  },
  data() {
    return {
      searchPhrase: '',
      searchPhraseTable: ''
    }
  },
  methods: {
    handleSearchPhrase(searchPhrase) {
      this.searchPhrase = searchPhrase
    },
    handleButtonClick() {
      this.searchPhraseTable = this.searchPhrase
    }
  }
}
</script>