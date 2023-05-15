<template>
<header class="row">
    <div class="col-6 logo">
        <i class="bi bi-book-half" style="margin-right: 12px"></i>
        <span>Library App</span>
    </div>
    <div class="col-6">
        <div v-if="isAuth" class="logout">
        <button class="button" @click="LogOut">
            <i class="bi bi-box-arrow-left mx-2"></i>Log Out
        </button>
        </div>
    </div>
</header>
</template>
<script>

import MainNavElementItem from '../components/MainNavElementItem.vue';
import router from '../router';

export default {
  components: {
    MainNavElementItem
  },
  data() {
    return {
      isAuth: false,
      authCookie: this.$cookies.get('auth')
    }
  },
  methods: {
    CheckCookies() {
      console.log("Checking cookies")
      if(this.$cookies.get('auth') == null) {
        router.replace('/login')
        this.isAuth = false;
      }
      else {
        router.replace('/panel')
        this.isAuth = true;
      }
    },
    LogOut() {
      if(this.$cookies.get('auth') != null) {
        this.$cookies.remove('auth')
        router.replace('/login')
        this.isAuth = false;
      }
    }
  },
  created() {
    this.CheckCookies(),
    this.isAuth = this.authCookie !== null
  },
  watch: {
    isAuth() {
        console.log(`isAuth: ${this.isAuth}`)
    }
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
