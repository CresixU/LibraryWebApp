import { createApp } from 'vue'
import App from './App.vue'
import router from './router.js'

import './assets/main.css'

const app = createApp(App)

app.use(router)

//global variable
app.config.globalProperties.$API_URL = `https://localhost:7054`

app.mount('#app')
