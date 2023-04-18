<template>
<div>
    <label for="category">Kategoria</label>
    <select name="category" id="category">
        <option selected disabled>Wybierz</option>
        <option value="" v-for="category in categories" v-bind:key="category.name">{{ category.name }}</option>
    </select>
</div>
</template>
<script>

export default {
    data() {
        return {
            categories: null
        }
    },
    methods: {
        async fetchData() {
            const url = this.$API_URL+'/api/categories'
            await fetch(url, {
                method: "GET",
            })
            .then(response => response.json())
            .then(data => { 
                this.categories = data;
            })
        }
    },

    created() {
        this.fetchData()
    }
}
</script>