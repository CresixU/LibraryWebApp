<template>
<div>
    <label for="category">Kategoria</label>
    <select name="category" id="category">
        <option selected disabled>Wybierz</option>
        <option :value="category.name" v-for="category in categories" v-bind:key="category.name">{{ category.name }}</option>
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
            const url = `${this.$API_URL}/api/categories`
            const response = await fetch(url)
            this.categories = await response.json()
        }
    },

    created() {
        this.fetchData()
    }
}
</script>