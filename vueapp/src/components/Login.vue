<template>
    <div class="lightCard">
        <h1>Login</h1>
        <input type="text" name="username" v-model="input.username" placeholder="Username" />
        <input type="password" name="password" v-model="input.password" placeholder="Password" />
        <button type="button" class="btn" v-on:click="authorize()">Login</button>
    </div>
    <br>
    <div class="lightCard">
        <h1>Register Account</h1>
        <router-link to="/Register"><button type="button" class="btn">Register</button></router-link>
    </div>
</template>

<script>
    export default {
        name: "Login",
        data() {
            return {
                input: {
                    username: "",
                    password: ""
                }
            }
        },
        methods: {
            authorize() {

                if (this.input.username != "" && this.input.password != "" && this.input.username != null && this.input.password != null) {
                    const requestOptions = {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ email: this.input.username, password: this.input.password })
                    };
                    fetch('Auth/Login', requestOptions)
                        .then(async response => {
                            const data = await response.text();

                            if (!response.ok) {

                                alert('Authentication Failed');
                                console.error('Auth Failed');

                            } else if (data != "Unauthorized") {

                                this.$store.commit("setAuthentication", true);
                                this.$router.replace({ name: "Watched" });

                            } else {

                                alert('Authentication Failed Invalid Credentials');
                                console.error('Auth Failed Unauthorized');

                            }


                        })
                        .catch(error => {
                            this.errorMessage = error;
                            console.error('Error!', error);
                        });
                }
                else {
                    alert('UserName or Password cannot be empty');
                    console.log("UserName or Password cannot be empty");
                }
            }
        }
    }
</script>

<style scoped></style>