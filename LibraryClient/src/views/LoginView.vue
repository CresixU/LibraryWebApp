<template>
    <div class="box">
        <div class="header">
            <h2>Login Panel</h2>
        </div>
        <div class="login-box">
            <div>
                <label for="login">Email</label>
                <input type="text" id="login" placeholder="Your email" v-model="email">
            </div>
            <div>
                <label for="pass">Password</label>
                <input type="password" id="pass" placeholder="Your password" v-model="password">
            </div>
            <div>
                <button class="btn-main mt-4 w-100" @click="LogIn()">Log in</button>
            </div>
        </div>
    </div>
</template>
<script>
import router from '../router'
import { store } from '../store.js'

export default {
    data() {
        return {
            email: '',
            password: '',
            store
        }
    },
    methods: {
        async LogIn() {
            const url = `${this.$API_URL}/api/account/login`
            try {
                const response = await fetch(url, {
                method: 'POST',
                credentials: 'include',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({"email": this.email, "password": this.password})
                })
                .then(response => {
                    if(!response.ok)
                    {
                        throw new Error("Incorrect data")
                    }
                    return response.text()
                })
                .then(data => {
                    this.store.user = JSON.parse(atob(data.split('.')[1]))
                    this.store.isUserLogged = true
                    this.$cookies.set('auth',data)
                    this.$router.replace('/panel')
                })
                
            }
            catch(error) {
                alert(error)
                console.error("There has been a problem with your fetch operation:", error);
            }      
        },
        CheckCookies() {
            if(this.$cookies.isKey('auth')) router.replace('/panel')
        }
    },
    created() {
        this.CheckCookies()
    }
}
</script>
<style scoped>
.box {
    position: absolute;
    border-radius: 15px 15px 0px 0px;
    width: 700px;
    background-color: rgb(250, 211, 255);
    left: 50%;
    right: 50%;
    transform: translate(-50%);
}
.header {
    background: var(--main-gradient);
    border-radius: 15px 15px 0px 0px;
    padding: 5px 10px;
}
.header h2 {
    color: white;
    text-align: center;
}
.login-box {
    padding: 30px;
    max-width: 500px;
    margin: auto;
}
.login-box  input {
    border-bottom: 1px solid blue;
    margin-bottom: 10px;
}
</style>