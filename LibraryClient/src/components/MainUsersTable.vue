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
                <td><button data-content="Brak" class="btn-main">Brak</button></td>
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
            users: [],
            data: []
        }
        
    },
    methods: {
        async fetchData(p, search = "") {
            this.page = p
            this.search = search
            const url = `${this.$API_URL}/api/users?PageNumber=${this.page}&PageSize=10`
            const response = await fetch(url)
            this.data = await response.json()
            this.users.push(...this.data.items)
        },
    },
    created() {
        this.fetchData(1)
    }
}
</script>