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
        </tr>
    </tbody>
</table>
</template>

<script>
export default {
    data() {
        return {
            books: null
        }
    },
    methods: {
        async fetchData() {
            const url = `${this.$API_URL}/api/books`
            const response = await fetch(url)
            this.books = await response.json()
        },
        OwnTrueFalseMessage(val) {
            if(val) return "Tak"
            return "Nie"
        }
    },
    created() {
        this.fetchData()
    }
}
</script>