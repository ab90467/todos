import Vue from 'vue'
//import App from './App.vue'
import Tab from '../src/components/Tab.vue';
import Tabs from '../src/components/Tabs.vue';

import Listtasks from '../src/components/ListTasks.vue';

import User from '../src/components/User.vue';
import Task from '../src/components/Task.vue';

import Loading from 'vue-loading-overlay';

document.location.hash = "#todolist"; //kickstart default view
Vue.use(Loading);

new Vue({
    el: '#todoapp',
    components: { Tab, Tabs, Listtasks, User, Task }
})