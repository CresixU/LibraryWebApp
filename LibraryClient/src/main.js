import { createApp } from 'vue'
import App from './App.vue'
import router from './router.js'
import VueCookies from 'vue-cookies'

import './assets/main.css'

const app = createApp(App)

app.use(router)
app.use(VueCookies)

//global variable
app.config.globalProperties.$API_URL = `https://localhost:7054`

app.mount('#app')
