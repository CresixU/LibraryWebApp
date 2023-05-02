<template>
<table>
    <thead>
        <tr>
            <th>Id</th>
            <th>Rent By</th>
            <th>Books</th>
            <th>Rent date</th>
            <th>Return date</th>
            <th>Actions</th>
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
            <td><button class="btn-main" :disabled="rent.returnDate != null">Return</button></td>
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
export default {
    data() {
        return {
            rents: [],
            data: [],
            page: 1,
            search: ''
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
            const response = await fetch(url)
            this.data = await response.json()
            this.rents = this.data.items
        },
        ConvertDateTime(datetime) {
            if(datetime == null) return ''
            var result = datetime.substr(0,10) + ' ' + datetime.substr(11,5)
            return result
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