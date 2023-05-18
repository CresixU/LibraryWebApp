<template>
<div>
    <h2 id="roles">Roles</h2>
    <div class="d-flex items-container mt-5" style="min-height: 50px">
        <div class="items-container-item" v-for="role in roles" :key="role.id" :name="role.id">
            <span>{{ role.name }} <span style="color: #b9b9b9">|</span> <span style="color: #fd4758">{{role.power}}</span></span>
            <button v-if="!role.isImmutable" class="btn-close" v-on:click="RoleDelete(role.id)"></button>
        </div>
    </div>
    <span style="position: relative; top: -15px; left: 10px; opacity: 0.6; font-size: 75%">You can't delete any role if there are users in it</span>
    <div class="d-flex">
        <div>
            <label for="role-input">Name</label>
            <input type="text" id="role-input" placeholder="Add new role" style="max-width: 200px" v-model.lazy="roleInput">
        </div>
        <div style="margin-left: 10px">
            <label for="role-power">Power</label>
            <input type="number" id="role-power" placeholder="Role power" style="max-width: 100px" v-model="rolePower">
        </div>
        
        <button class="btn-main" style="margin-left: 10px; border-radius: 25px; max-height: 36px; margin-top: 24px" v-on:click="RoleAdd">Add new</button>
    </div>
    <div class="mt-5">
        <h4>Edit role</h4>
        <select placeholder="Select Role" v-model="selectedRole">
            <option value="" selected disabled>Select role</option>
            <option :value="role.id" v-for="role in roles" :key="role.id" :disabled="role.isImmutable">{{ role.name }}</option>
        </select>
        <div>
            <label for="role-input2">Name</label>
            <input type="text" id="role-input2" placeholder="Add new role" style="max-width: 200px" v-model="roleEditName">
        </div>
        <div>
            <label for="role-power2">Power</label>
            <input type="number" id="role-power2" placeholder="Role power" style="max-width: 100px" v-model="roleEditPower">
        </div>
        <button class="btn-main mt-3" style="border-radius: 25px; max-height: 36px;" v-on:click="RoleChange(this.selectedRole)">Save role</button>
    </div>
</div>    
</template>
<script>
export default {
    data() {
        return {
            roles: [],
            roleInput: '',
            rolePower: 1,
            selectedRole: '',
            roleEditName: '',
            roleEditPower: 1
        }
    },
    methods: {
        async fetchData() {
            const url = `${this.$API_URL}/api/roles`
            const response = await fetch(url, {
                credentials: 'include',
                headers: {
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            this.roles = await response.json()
        },
        async RoleDelete(id) {
            var index = this.roles.findIndex(r => r.id == id)
            if(index == -1) return
            const url = `${this.$API_URL}/api/roles/${id}`
            const response = await fetch(url, {
                method: 'DELETE'
            })
            this.roles.splice(index, 1)
            this.fetchData()
        },
        async RoleAdd() {
            if(this.roleInput == '' || this.rolePower == '') return
            const url = `${this.$API_URL}/api/roles/`
            const response = await fetch(url, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({"name": this.roleInput, "power": this.rolePower})
            })
            this.fetchData()
        },
        async RoleChange(id) {
            var index = this.roles.findIndex(r => r.id == id)
            if(index == -1) return
            const url = `${this.$API_URL}/api/roles/${id}`
            const response = await fetch(url, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({"name": this.roleEditName, "power": this.roleEditPower})
            })
            this.fetchData()
        }
    },
    created() {
        this.fetchData()
    },
    watch: {
        rolePower(value) {
            if(this.rolePower < 1 || this.rolePower > 250) this.rolePower = 1
        },
        selectedRole(value) {
            var thisRole = this.roles.find(r => r.id == value)
            this.roleEditName = thisRole.name
            this.roleEditPower = thisRole.power
        },
        roleEditPower(e) {
            if(this.roleEditPower < 1 || this.roleEditPower > 250) this.roleEditPower = 1
        }
    }
}
</script>