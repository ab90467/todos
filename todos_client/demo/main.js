import Vue from 'vue'
//import App from './App.vue'
import Tab from '../src/components/Tab.vue';
import Tabs from '../src/components/Tabs.vue';
import Loading from '../src/components/Loading.vue';



import Listtasks from '../src/components/ListTasks.vue';
import ajax from '../src/js/ajax.js';
ajax.init();


// https://css-tricks.com/creating-vue-js-component-instances-programmatically/

new Vue({
    el: '#app',
    components: { Tab, Tabs, Loading, Listtasks },

});