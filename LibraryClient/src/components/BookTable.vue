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
            <td>{{ OwnTrueFalseMessage(book.isAvailable) }}</td>
            <td><button v-on:click="Rent(book.id)" data-content="Rent" :disabled="!book.isAvailable" class="btn-main">Rent</button></td>
        </tr>
    </tbody>
</table>
<div class="w-100 pagination">
    Pages...
</div>
</template>

<script>
export default {
    data() {
        return {
            books: null,
            data: null
        }
    },
    methods: {
        async fetchData(page = 1, search = "", category = "") {
            const url = `${this.$API_URL}/api/books?PageNumber=${page}&PageSize=10`
            const response = await fetch(url)
            this.data = await response.json()
            this.books = this.data.items
        },
        OwnTrueFalseMessage(val) {
            if(val) return "Tak"
            return "Nie"
        },
        Rent(bookId) {
            alert("open modal :)")
        }
    },
    created() {
        this.fetchData()
    }
}
</script>