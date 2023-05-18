<template>
<div>
    <h2 id="categories">Categories</h2>
    <div class="d-flex items-container mt-5" style="min-height: 50px">
        <div class="items-container-item" v-for="category in categories" :key="category.id" :name="category.id">
            <span>{{ category.name }}</span>
            <button class="btn-close" v-on:click="CategoryDelete(category.id)"></button>
        </div>
    </div>
    <span style="position: relative; top: -15px; left: 10px; opacity: 0.6; font-size: 75%">You can't delete any category if there are books in it</span>
    <div class="d-flex">
        <input type="text" placeholder="Add new category" style="max-width: 200px" v-model.lazy="categoryInput">
        <button class="btn-main" style="margin-left: 10px; border-radius: 25px;" v-on:click="CategoryAdd">Add</button>
    </div>
    <div class="mt-5">
        <h4>Edit category</h4>
        <select placeholder="Select Category" v-model="selectedCategory">
            <option value="0" selected>Select category</option>
            <option :value="category.id" v-for="category in categories" :key="category.id">{{ category.name }}</option>
        </select>
        <div>
            <label for="category-input2">New Name</label>
            <input type="text" id="category-input2" placeholder="New Category name" style="max-width: 200px" v-model="categoryEditName">
        </div>
        <button class="btn-main" style=" border-radius: 25px; max-height: 36px; margin-top: 24px" v-on:click="CategoryChange(this.selectedCategory)">Save category</button>
    </div>
</div>
</template>
<script>
export default {
    data() {
        return {
            categories: [],
            categoryInput: '',
            selectedCategory: '',
            categoryEditName: ''
        }
    },
    methods: {
        async fetchData() {
            var url = `${this.$API_URL}/api/categories`
            var response = await fetch(url)
            this.categories = await response.json()
        },
        async CategoryAdd() {
            const url = `${this.$API_URL}/api/categories`
            const response = await fetch(url, {
                method: 'POST',
                credentials: 'include',
                headers: {
                     'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                },
                body: JSON.stringify({"name": this.categoryInput})
            })
            this.fetchData()
            this.categoryInput = ''
        },
        async CategoryDelete(id) {
            var index = this.categories.findIndex(c => c.id == id)
            if(index == -1) return
            const url = `${this.$API_URL}/api/categories/${id}`
            const response = await fetch(url, {
                method: 'DELETE',
                credentials: 'include',
                headers: {
                     'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                },
            })
            this.categories.splice(index, 1)
            this.fetchData()
        },
        async CategoryChange(id) {
            if(id == '') return
            const url = `${this.$API_URL}/api/categories/${id}`
            const response = await fetch(url, {
                credentials: 'include',
                method: 'PUT',
                headers: {
                     'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                },
                body: JSON.stringify({"name": this.categoryEditName})
            })
            this.fetchData()
            this.categoryEditName = ''
        } 
    },
    created() {
        this.fetchData()
    },
    watch: {
        selectedCategory(value) {
            var thisCategory = this.categories.find(c => c.id == value)
            if(thisCategory==undefined)  {
                this.categoryEditName = ''
                return
            }
            this.categoryEditName = thisCategory.name
        }
    }
}

</script>
<style scoped>
label {
   display: block;
   position: relative;
   left: 10px;
}
</style>