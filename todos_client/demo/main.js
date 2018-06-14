import Vue from 'vue'
//import App from './App.vue'
import Tab from '../src/components/Tab.vue';
import Tabs from '../src/components/Tabs.vue';
import Loading from '../src/components/Loading.vue';

/* eslint-disable no-new */
/*new Vue({
    el: '#app',
    template: '<App/>',
    components: { App, Tab, Tabs }
});*/

new Vue({
    el: '#app',
    components: { Tab, Tabs, Loading }
})