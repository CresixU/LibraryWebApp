// store.js
import { reactive } from 'vue'

export const store = reactive({
  isUserLogged: false,
  user: null,
  IsAdmin() {
    if(this.user.RoleName == 'Admin') return true
    return false
  },
  IsOwner() {
    if(this.user.RoleName == 'Admin' || this.user.RoleName == 'Owner')
      return true
    return false
  },
  IsUser() {
    if(this.user.RoleName == 'User') return true
    return false
  }
})