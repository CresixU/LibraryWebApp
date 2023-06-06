<template>
<ModalChoice 
    modalId="modal_delete"
    title="Delete rent"
    btnYes="Yes"
    btnNo="Cancel"
    :trueData="clickedRent"
    v-on:CallParentTrueAction="ModalDeleteAction">
    Do you want to delete rent with id: {{clickedRent}}?
</ModalChoice>
<ModalChoice 
    modalId="modal_return"
    title="Return rent"
    btnYes="Yes"
    btnNo="Cancel"
    :trueData="clickedRent"
    v-on:CallParentTrueAction="ModalRentReturnAction">
    Do you want to return all books? Rent id: {{clickedRent}}
</ModalChoice>
<table>
    <thead>
        <tr>
            <th>Id</th>
            <th>Rent By</th>
            <th>Books</th>
            <th>Rent date</th>
            <th>Return date</th>
            <th colspan="2">Actions</th>
        </tr>
    </thead>
    <tbody>
        <tr v-for="rent in rents" :key="rent.id">
            <td :class="(rent.returnDate != null) ? 'statusgreen' : 'statusred'">{{ rent.id }}</td>
            <td>{{ rent.user }}</td>
            <td>
                <span v-for="(book, index) in rent.books" :key="book.title"><span v-if="index>1">,</span> {{ book.title }} </span>
            </td>
            <td>{{ ConvertDateTime(rent.rentDate) }}</td>
            <td>{{ ConvertDateTime(rent.returnDate) }}</td>
            <td>
                <button 
                class="btn-main" 
                :disabled="rent.returnDate != null"
                @click="ModalRentReturn(rent.id)"
                >Return</button>
            </td>
            <td>
                <button
                class="button"
                :disabled="rent.returnDate == null"
                v-on:click="ModalDelete(rent.id)">
                    <i class="bi bi-trash-fill button" style="color: #e43a4d; font-size: 20px"></i>
                </button>
            </td>
        </tr>
    </tbody>
</table>
<div class="w-100 pagination" style="justify-content:center">
    <button
     class="btn-main mx-2"
      v-on:click="fetchData(page-1,search)"
      :disabled="page <= 1">
      &#60;&#60;
    </button>
    <button
     class="btn-main mx-2"
      v-on:click="fetchData(page+1,search)"
      :disabled="page >= data.totalPages">
      &#62;&#62;
    </button>
</div>
</template>
<script>
import ModalChoice from './Modals/ModalChoice.vue'

export default {
    components: {
        ModalChoice
    },
    data() {
        return {
            rents: [],
            data: [],
            page: 1,
            search: '',
            clickedRent: ''
        }
    },
    props: {
        searchPhrase: String
    },
    methods: {
        async fetchData(p, search = "") {
            this.page = p
            this.search = search
            var url = `${this.$API_URL}/api/rents?PageNumber=${this.page}&PageSize=10`
            if(search != '') url += `&SearchPhrase=${search}`
            const response = await fetch(url, {
                credentials: 'include',
                headers: {
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            this.data = await response.json()
            this.rents = this.data.items
        },
        ConvertDateTime(datetime) {
            if(datetime == null) return ''
            var result = datetime.substr(0,10) + ' ' + datetime.substr(11,5)
            return result
        },
        ModalRentReturn(id) {
            this.clickedRent = id
            var myModal = new bootstrap.Modal(document.getElementById('modal_return'))
            myModal.toggle()
        },
        async ModalRentReturnAction(id) {
            console.log("Returning rent id: "+id)
            var now = Date.now()
            var newDate = new Date(now + 2 * 60 * 60 * 1000);
            var date = new Date(newDate).toJSON()
            const url = `${this.$API_URL}/api/rents/${id}`
            const response = await fetch(url, {
                method: 'PUT',
                headers: { 
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                 },
                body: JSON.stringify({"returnDate": date})
            })
            console.log(response)
            await this.fetchData(1)
        },
        ModalDelete(id) {
            this.clickedRent = id
            var myModal = new bootstrap.Modal(document.getElementById('modal_delete'))
            myModal.toggle()
        },
        async ModalDeleteAction(id) {
            console.log("Deleting id: "+id)
            const url = `${this.$API_URL}/api/rents/${id}`
            const response = await fetch(url, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            await this.fetchData(1)
        }
    },
    created() {
        this.fetchData(1)
    },
    watch: {
        searchPhrase() {
            this.fetchData(this.page, this.searchPhrase)
        }
    }
}
</script>
<style scoped>
.statusred {
    box-shadow: inset 3px 0px #e43a4d;
}
.statusgreen {
    box-shadow: inset 3px 0px #4cb458;
}
</style>