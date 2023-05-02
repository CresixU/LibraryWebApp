<template>
<div>
    <label for="category">Category</label>
    <select name="category" id="category"
    v-model="category"
    @change="emitSelectCategory">
        <option value="" selected>Select</option>
        <option :value="category.name" v-for="category in categories" v-bind:key="category.name">{{ category.name }}</option>
    </select>
</div>
</template>
<script>

export default {
    data() {
        return {
            categories: null,
            category: ''
        }
    },
    methods: {
        async fetchData() {
            const url = `${this.$API_URL}/api/categories`
            const response = await fetch(url)
            this.categories = await response.json()
        },
        emitSelectCategory() {
            this.$emit('category', this.category)
        }
    },
    created() {
        this.fetchData()
    }
}
</script>