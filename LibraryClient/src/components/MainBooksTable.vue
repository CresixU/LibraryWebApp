<template>
    <ModalChoiceUserIdInput
    modalId="modal_rent"
    title="Rent a book"
    btnYes="Yes"
    btnNo="Cancel"
    :trueData="clickedBook"
    v-on:CallParentTrueAction="ModalRentAction">
    Do you want to rent this book? Book id: {{clickedBook}}
</ModalChoiceUserIdInput>
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
            <td><button v-on:click="ModalRent(book.id)" data-content="Rent" :disabled="!book.isAvailable" class="btn-main">Rent</button></td>
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
import ModalChoiceUserIdInput from './Modals/ModalChoiceUserIdInput.vue'

export default {
    components: {
        ModalChoiceUserIdInput
    },
    data() {
        return {
            books: [],
            data: [],
            page: 1,
            search: "",
            category: "",
            clickedBook: '',
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
            const response = await fetch(url, {
                credentials: 'include',
                headers: {
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            this.data = await response.json()
            this.books = this.data.items
        },
        ModalRent(bookId) {
            this.clickedBook = bookId
            var myModal = new bootstrap.Modal(document.getElementById('modal_rent'))
            myModal.toggle()
        },
        async ModalRentAction(selectedUser) {
            const url = `${this.$API_URL}/api/rents`
            const response = await fetch(url, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({"userId": selectedUser, "BookIds": [this.clickedBook]})
            })
            this.fetchData(1)
        }
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