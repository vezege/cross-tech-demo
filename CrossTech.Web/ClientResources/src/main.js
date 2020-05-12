import Vue from 'vue';
import App from "./App.vue";

import { store } from './store';

import axios from 'axios';
import Notifications from 'vue-notification';

import 'bootstrap';
import './assets/styles/app.scss';

Vue.config.productionTip = false;
window.axios = axios;
Vue.use(Notifications);

import EmployeesTab from "./components/EmployeesTab.vue";
import UsersTab from "./components/UsersTab.vue";

Vue.component('employees-tab', EmployeesTab);
Vue.component('users-tab', UsersTab);

new Vue({
  store: store,
  render: h => h(App)
}).$mount("#app");
