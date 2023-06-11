<template>
<div class="user-container" id="users">
    <h2>New user</h2>
    <div class="mt-5 mb-2">
        <label for="user-firstname">Firstname</label>
        <input type="text" name="user-firstname" id="user-firstname" v-model="firstname">
    </div>
    <div class="mb-2">
        <label for="user-lastname">Lastname</label>
        <input type="text" name="user-lastname" id="user-lastname" v-model="lastname">
    </div>
    <div class="mb-2">
        <label for="user-email">Email</label>
        <input type="text" name="user-email" id="user-email" v-model="email">
        <p style="color: red; margin-top: -3px;" id="mail-checker"></p>
    </div>
    <div class="mb-2">
        <label for="user-password">Password</label>
        <input type="text" name="user-password" id="user-password" v-model="password">
    </div>
    <div class="mb-2">
        <label for="user-city">City</label>
        <input type="text" name="user-city" id="user-city" v-model="city">
    </div>
    <div class="mb-2">
        <label for="user-street">Street</label>
        <input type="text" name="user-street" id="user-steet" v-model="street">
    </div>
    <div class="mb-2">
        <label for="user-housenumber">House number</label>
        <input type="text" name="user-housenumber" id="user-housenumber" v-model="number">
    </div>
    <div class="mb-2">
        <label for="user-postalcode">Postal Code</label>
        <input type="text" name="user-postalcode" id="user-postalcode" v-model="postalcode">
    </div>
    <div>
        <button class="btn-main" style="border-radius: 15px" @click="CreateUser">Add new</button>
    </div>
</div>
</template>
<script>
export default {
    data() {
        return {
            firstname: '',
            lastname: '',
            email: '',
            password: '123456',
            city: '',
            street: '',
            number: '',
            postalcode: ''
        }
    },
    methods: {
        async CreateUser() {
            if(!this.CheckMail()) {
                alert("Mail is taken")
                return false
            }
            const url = `${this.$API_URL}/api/account/register`
            const response = await fetch(url, {
                credentials: 'include',
                method: 'POST',
                headers: { 
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                },
                body: JSON.stringify(
                {
                    "Firstname": this.firstname,
                    "Lastname": this.lastname,
                    "Email": this.email,
                    "Password": this.password,
                    "ConfirmPassword": this.password,
                    "City": this.city,
                    "Street": this.street,
                    "Number": this.number,
                    "PostalCode": this.postalcode
                })
            })
            .then(res => {
                if(!res.ok) {
                    alert("One or more fields were wrong")
                    return
                }
                this.firstname = '',
                this.lastname = '',
                this.email = '',
                this.password = '',
                this.city = '',
                this.street = '',
                this.number = '',
                this.postalcode = ''
                alert("User created");
            })
            
        },
        async CheckMail() {
        const url = `${this.$API_URL}/api/account/checkmail?email=${this.email}`
            const response = await fetch(url, {
                credentials: 'include',
                method: 'GET',
                headers: { 
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$cookies.get('auth')}`
                }
            })
            .then(res => {
                if(!res.ok) {
                    document.getElementById('mail-checker').innerHTML = 'Mail is taken'
                    return false
                }
                else document.getElementById('mail-checker').innerHTML = ''
                return true
                
            })
        
        },
    },
    watch: {
        email() {
            this.CheckMail()
        }
    }
}
</script>
<style>
label {
    display: block;
    position: relative;
    left: 10px;
 }
</style>