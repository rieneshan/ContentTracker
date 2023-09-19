import './assets/main.css'

import { createApp } from 'vue'

import Vuex from "vuex"
import * as VueRouter from "vue-router"
import Login from "./components/Login.vue"
import Watched from "./components/Watched.vue"
import Episodes from "./components/Episodes.vue"
import Series from "./components/Series.vue"
import Register from "./components/Register.vue"

import App from './App.vue'

const store = new Vuex.Store(
    {
        state: {
            authenticated: false
        },
        mutations: {
            setAuthentication(state, status) {
                state.authenticated = status;
            }
        }
    }
);

const routes = [
    {
        path: '/',
        redirect: {
            name: "Login"
        }
    },
    {
        path: "/Login",
        name: "Login",
        component: Login
    },
    {
        path: "/Register",
        name: "Register",
        component: Register
    },
    {
        path: "/Watched",
        name: "Watched",
        component: Watched,
        beforeEnter: (to, from, next) => {
            if (store.state.authenticated == false) {
                next(false);
            } else {
                next();
            }
        }
    },
    {
        path: "/Episodes/:id",
        name: "Episodes",
        component: Episodes,
        props: true,
        beforeEnter: (to, from, next) => {
            if (store.state.authenticated == false) {
                next(false);
            } else {
                next();
            }
        }
    }
    ,
    {
        path: "/Series",
        name: "Series",
        component: Series,
        beforeEnter: (to, from, next) => {
            if (store.state.authenticated == false) {
                next(false);
            } else {
                next();
            }
        }
    }
]

const router = VueRouter.createRouter({
    
    history: VueRouter.createWebHashHistory(),
    routes, 
})

const app = createApp(App)
app.use(router)
app.use(store)

app.mount('#app')
