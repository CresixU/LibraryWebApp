<template>
<div class="row">
    <div class="col-3" style="padding-left: 0">
        <router-link to="/admin/categories" v-if="store.IsOwner()">
            <AdminPanelNavItem>Categories</AdminPanelNavItem>
        </router-link>
        <router-link to="/admin/books">
            <AdminPanelNavItem>New book</AdminPanelNavItem>
        </router-link>
        <router-link to="/admin/users">
            <AdminPanelNavItem>New user</AdminPanelNavItem>
        </router-link>
        <router-link to="/admin/roles" v-if="store.IsAdmin()">
            <AdminPanelNavItem>Roles</AdminPanelNavItem>
        </router-link>
    </div>
    <div class="panel-main col-9">
        <router-view></router-view>
    </div>
</div>

</template>
<script>
    import AdminPanelNavItem from '../components/AdminPanelNavItem.vue'
    import { store } from '../store.js'

export default {
    components: {
        AdminPanelNavItem
    },
    data() {
        return {
            store
        }
    }
}
</script>
<style scoped>
.panel-main {
    border-left: 1px solid rgba(105, 105, 105, 0.322)
}
.panel-main > div:not(:first-child) {
    margin-top: 50px;
    border-top: 1px solid rgba(105, 105, 105, 0.322);
}
label {
   display: block;
   position: relative;
   left: 10px;
}
.book-container > div {
    margin: auto;
}
</style>