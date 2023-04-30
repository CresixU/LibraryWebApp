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
<div class="w-100 pagination">
    <button
     class="btn-main"
     style="margin-left:auto"
      v-on:click="fetchData(page+1,search,category)">
      Fetch more!
    </button>
</div>
</template>

<script>
export default {
    data() {
        return {
            books: [],
            data: null,
            page: 1,
            search: "",
            category: ""
        }
    },
    methods: {
        async fetchData(p, search = "", category = "") {
            this.page = p
            this.search = search
            this.category = category
            console.log(this.page)
            const url = `${this.$API_URL}/api/books?PageNumber=${this.page}&PageSize=10`
            const response = await fetch(url)
            this.data = await response.json()
            this.books.push(...this.data.items)
        },
        Rent(bookId) {
            alert("open modal :)")
        },
    },
    created() {
        this.fetchData(1)
    }
}
</script>