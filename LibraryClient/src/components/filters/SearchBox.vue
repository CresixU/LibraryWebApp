<template>
<div>
    <input
     type="text" 
     name="search_box"
     placeholder="Search phrase..."
     v-model="searchPhrase"
     @input="emitSearchPhrase"
      >
</div>

</template>
<script>
export default {
    data() {
        return {
            searchPhrase: ''
        }
    },
    methods: {
        emitSearchPhrase() {
            this.$emit('searchPhrase', this.searchPhrase)
        }
    }
}
</script>