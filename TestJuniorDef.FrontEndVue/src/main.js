import Vue from 'vue'
import { BootstrapVue, IconsPlugin, NavbarPlugin, FormTagsPlugin } from 'bootstrap-vue'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import App from './App.vue'
import router from './router'

import './assets/styles/main.css'

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(NavbarPlugin)
Vue.use(FormTagsPlugin)
// window.axios = require('axios');

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
