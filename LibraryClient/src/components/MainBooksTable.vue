<template>
<table>
    <thead>
        <tr>
            <th>Id</th>
            <th>Tytuł</th>
            <th>Autor</th>
            <th>Kategoria</th>
            <th>Rok publikacji</th>
            <th>Dostępność</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        <tr v-for="book in books" :key="book.title">
            <td>{{ book.id }}</td>
            <td>{{ book.title }}</td>
            <td>{{ book.author }}</td>
            <td>{{ book.category }}</td>
            <td>{{ book.publicationYear }}</td>
            <td>{{ book.isAvailable ? 'Tak' : 'Nie' }}</td>
            <td><button v-on:click="Rent(book.id)" data-content="Rent" :disabled="!book.isAvailable" class="btn-main">Rent</button></td>
        </tr>
    </tbody>
</table>
<div class="w-100 pagination" style="justify-content:center">
    <button
     class="btn-main mx-2"
      v-on:click="fetchData(page-1,search,category)"
      :disabled="page <= 1">
      &#60;&#60;
    </button>
    <button
     class="btn-main mx-2"
      v-on:click="fetchData(page+1,search,category)"
      :disabled="page >= data.totalPages">
      &#62;&#62;
    </button>
</div>
</template>

<script>
export default {
    data() {
        return {
            books: [],
            data: [],
            page: 1,
            search: "",
            category: ""
        }
    },
    props: {
        categoryProp: String,
        searchPhrase: String
    },
    methods: {
        async fetchData(p, search = "", category = "") {
            this.page = p
            this.search = search
            this.category = category
            var url = `${this.$API_URL}/api/books?PageNumber=${this.page}&PageSize=10`
            if(search != '') url += `&SearchPhrase=${search}`
            if(category != '') url += `&Category=${category}`
            const response = await fetch(url)
            this.data = await response.json()
            this.books = this.data.items
        },
        Rent(bookId) {
            alert("open modal :)")
        },
    },
    created() {
        this.fetchData(1)
    },
    watch: {
        searchPhrase() {
            this.fetchData(this.page, this.searchPhrase, this.category)
        },
        categoryProp() {
            this.fetchData(this.page, this.search, this.categoryProp)
        }
    }
}
</script>