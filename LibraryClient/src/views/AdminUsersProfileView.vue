<template>
<div>
    <div class="profile">
        <h2>User profile id {{ data.id }}</h2>
        <table>
            <tr>
                <td>Fullname</td>
                <td>{{ data.firstname }} {{ data.lastname }}</td>
            </tr>
            <tr>
                <td>Email</td>
                <td>{{ data.email }}</td>
            </tr>
            <tr>
                <td>City</td>
                <td>{{ data.city }}</td>
            </tr>
            <tr>
                <td>Street</td>
                <td>{{ data.street }} {{ data.number }}</td>
            </tr>
            <tr>
                <td>Postal code</td>
                <td>{{ data.postalCode }}</td>
            </tr>
        </table>
    </div>
    <div class="mt-5">
        <table>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Books</th>
                    <th>Rent date</th>
                    <th>Return date</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="rent in rents.items" :key="rent.id">
                    <td :class="(rent.returnDate != null) ? 'statusgreen' : 'statusred'">{{ rent.id }}</td>
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
              v-on:click="fetchData(page-1)"
              :disabled="page <= 1">
              &#60;&#60;
            </button>
            <button
             class="btn-main mx-2"
              v-on:click="fetchData(page+1)"
              :disabled="page >= rents.totalPages">
              &#62;&#62;
            </button>
        </div>
    </div>
</div>
</template>
<script>
export default {
    data() {
        return {
            data: [],
            rents: [],
            page: 1
        }
    },
    methods: {
        async fetchData(page) {
            this.page = page
            const url = `${this.$API_URL}/api/users/${this.$route.params.id}`
            const response = await fetch(url, {
                credentials: 'include',
                headers: {
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            this.data = await response.json()

            const url2 = `${this.$API_URL}/api/rents/${this.$route.params.id}?PageNumber=${this.page}&PageSize=10`
            const response2 = await fetch(url2, {
                credentials: 'include',
                headers: {
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            this.rents = await response2.json()
        },
        ConvertDateTime(datetime) {
            if(datetime == null) return ''
            var result = datetime.substr(0,10) + ' ' + datetime.substr(11,5)
            return result
        }
    },
    created() {
        this.fetchData(1)
    }
}
</script>
<style scoped>
.profile table td, .profile table tr {
    background-color: transparent;
}
</style>