import { createApp } from 'vue'
import App from './App.vue'

import './assets/main.css'

//createApp(App).mount('#app')
const app = createApp(App)
//global variable
app.config.globalProperties.$API_URL = `https://localhost:7054`

app.mount('#app')
