<template>
<div class="modal fade" :id="modalId" tabindex="-1">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">{{ title }}</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <p><slot>Default text</slot></p>
                <div>
                    <label for="user-input">User name or email</label>
                    <input type="text" id="user-input" v-model="userInput">
                </div>
                <div>
                    <table>
                        <thead>
                            <tr>
                                <th>Select</th>
                                <th>Fullname</th>
                                <th>Email</th>
                                <th>Address</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="user in users" :key="user.id">
                                <td><input type="radio" name="user" :value="user.id" v-model="selectedUser"></td>
                                <td>{{ user.firstname }} {{ user.lastname }}</td>
                                <td>{{ user.email }}</td>
                                <td>{{ user.city }} {{ user.street }} {{ user.number }}</td>
                            </tr>
                        </tbody>
                    </table>
                    <p>Selected user id: {{ selectedUser }}</p>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">{{ btnNo }}</button>
                <button type="button" class="btn btn-primary" data-bs-dismiss="modal" @click="CallParentTrueAction()">{{ btnYes }}</button>
            </div>
        </div>
    </div>
</div>
</template>
<script>
export default {
    props: {
        modalId: {
            type: String,
            required: true
        },
        title: {
            type: String, 
            default: 'No title'
        },
        btnYes: {
            type: String,
            default: 'Save changes'
        },
        btnNo: {
            type: String,
            default: 'Close'
        },
        trueData: {}
    },
    data() {
        return {
            userInput: '',
            users: '',
            selectedUser: ''
        }
    },
    methods: {
        CallParentTrueAction() {
            this.$emit('CallParentTrueAction', this.selectedUser)
        },
        async fetchData() {
            var url = `${this.$API_URL}/api/users?PageNumber=1&PageSize=10&SearchPhrase=${this.userInput}`
            const response = await fetch(url, {
                credentials: 'include',
                headers: {
                     'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                },
            })
            var data = await response.json()
            this.users = data.items
        },
    },
    watch: {
        userInput() {
            if(this.userInput.length > 3) {
                this.fetchData();
            }
        }
    }
}
</script>