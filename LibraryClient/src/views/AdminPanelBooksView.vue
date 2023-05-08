<template>
<div class="book-container" id="books">
    <h2>New Book</h2>
    <div class="mt-5">
        <div class="mb-2">
            <label for="book-title">Title</label>
            <input type="text" placeholder="Title" v-model="title">
        </div>
        <div class="mb-2">
            <label for="book-author">Author</label>
            <input type="text" placeholder="Author" v-model="author">
        </div>
        <div class="mb-2">
            <label for="book-year">Release year</label>
            <input type="number" min="1" :max="new Date().getFullYear()" placeholder="ex. 1990" v-model="year">
        </div>
        <div class="mb-3">
            <label for="book-author">Book Category</label>
            <select placeholder="ex. Fantasy" v-model="category">
                <option 
                    v-for="category in categories"
                    :key="category.id"
                    :value="category.id">
                    {{category.name}}
                </option>
            </select>
        </div>
        <div>
            <button class="btn-main" style="border-radius: 15px" @click="CreateBook">Add new</button>
        </div>
    </div>
</div>
</template>
<script>
export default {
    data() {
        return {
            categories: [],
            title: '',
            author: '',
            year: '',
            category: ''
        }
    },
    methods: {
        async fetchData() {
            var url = `${this.$API_URL}/api/categories`
            var response = await fetch(url)
            this.categories = await response.json()
        },
        async CreateBook() {
            if(this.title == '' || this.author == '' || this.year == '' || this.category == '') return
            const url = `${this.$API_URL}/api/books`
            const response = await fetch(url, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({"Title": this.title, "Author": this.author, "PublicationYear": this.year, "CategoryId": this.category})
            })
            this.title = ''
            this.author = ''
            this.year = ''
            this.category = ''
        }
    },
    created() {
        this.fetchData()
    }
}
</script>
<style scoped>
label {
    display: block;
    position: relative;
    left: 10px;
 }
 .book-container > div {
     margin: auto;
 }
</style>