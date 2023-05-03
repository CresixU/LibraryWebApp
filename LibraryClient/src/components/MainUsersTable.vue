<template>
    <table>
        <thead>
            <tr>
                <th>Id</th>
                <th>Imię Nazwisko</th>
                <th>Email</th>
                <th>Adres</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="user in users" :key="user.id">
                <td>{{ user.id }}</td>
                <td>{{ user.firstname }} {{ user.lastname }}</td>
                <td>{{ user.email }}</td>
                <td>{{ user.city }} {{ user.street }} {{ user.number }}</td>
                <td>
                    <router-link :to="'user/'+user.id">
                        <button class="btn-main">Profile</button>
                    </router-link>
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
export default {
    data() {
        return {
            users: [],
            data: [],
            page: 1,
            search: ''
        }
        
    },
    methods: {
        async fetchData(p, search = "") {
            this.page = p
            this.search = search
            var url = `${this.$API_URL}/api/users?PageNumber=${this.page}&PageSize=10`
            if(search != '') { 
                url += `&SearchPhrase=${search}`
            }
            const response = await fetch(url)
            this.data = await response.json()
            this.users = this.data.items
        },
    },
    props: {
        searchPhrase: String
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