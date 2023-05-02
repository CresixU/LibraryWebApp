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
        <MainUsersTable :searchPhrase="searchPhraseTable"/>
      </div>
</template>

<script>
import MainUsersTable from '../components/MainUsersTable.vue'
import SearchBox from '../components/filters/SearchBox.vue'
export default {
    components: {
        MainUsersTable,
        SearchBox
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