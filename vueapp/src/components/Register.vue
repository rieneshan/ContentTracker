<template>
    <div class="lightCard">
        <h1>Login</h1>
        <input type="text" name="username" v-model="input.username" placeholder="Username" />
        <input type="password" name="password" v-model="input.password" placeholder="Password" />
        <input type="password" name="password" v-model="input.confirmpassword" placeholder="Password" />
        <button type="button" class="btn" v-on:click="register()">Register</button>
    </div>
</template>

<script>
    export default {
        name: "Login",
        data() {
            return {
                input: {
                    username: "",
                    password: "",
                    confirmpassword: ""
                }
            }
        },
        methods: {
            register() {


                const requestOptions = {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email: this.input.username, password: this.input.password, confirmpassword: this.input.confirmpassword })
                };
                fetch('Auth/Register', requestOptions)
                .then(async response => {
                const data = await response.text();
                        
                    if (!response.ok) {

                        console.error('Auth Failed');

                    } else if (data == "true") {

                        this.$store.commit("setAuthentication", true);
                        this.$router.replace({ name: "Watched" });

                    } else {

                        console.error('Auth Failed Unauthorized');

                    }


                })
                .catch(error => {
                        this.errorMessage = error;
                        console.error('Error!', error);
                });
            }
        }
    }
</script>

<style scoped></style>