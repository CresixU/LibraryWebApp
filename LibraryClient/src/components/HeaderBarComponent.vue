<template>
<header class="row">
    <div class="col-6 logo">
        <i class="bi bi-book-half" style="margin-right: 12px"></i>
        <span>Library App</span>
    </div>
    <div class="col-6">
        <div v-if="store.isUserLogged" class="logout">
        <button class="button" @click="LogOut">
            <i class="bi bi-box-arrow-left mx-2"></i>Log Out
        </button>
        </div>
    </div>
</header>
</template>
<script>

import MainNavElementItem from '../components/MainNavElementItem.vue'
import router from '../router'
import { store } from '../store.js'

export default {
  components: {
    MainNavElementItem
  },
  data() {
    return {
      isAuth: false,
      store
    }
  },
  methods: {
    CheckCookies() {
      if(this.$cookies.get('auth') == null) {
        this.store.isUserLogged = false
        router.replace('/login')
      }
      else {
        this.store.isUserLogged = true
        if(this.store.user == null) this.store.user = JSON.parse(atob(this.$cookies.get('auth').split('.')[1]))
        router.replace('/panel')
      }
    },
    LogOut() {
      if(this.$cookies.get('auth') != null) {
        this.$cookies.remove('auth')
        this.store.isUserLogged = false
        this.store.user = null
        router.replace('/login')
      }
    },
  },
  created() {
    this.CheckCookies()
  }
}
</script>

<style scoped>
header > :nth-child(2) {
  justify-content: right;
  align-items: center;
  display: flex;
}
.logout > button {
  background: transparent;
  color: white;
  font-size: 25px;
  font-weight: 100;
  letter-spacing: 2px;
  margin-right: 10px;
  margin-left: auto;
}
</style>
