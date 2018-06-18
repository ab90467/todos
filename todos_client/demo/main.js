import Vue from 'vue'
import App from './App.vue'
import Loading from 'vue-loading-overlay';

Vue.use(Loading);

document.location.hash = "#todolist"; //kickstart default view

new Vue({
    el: '#todoapp',
    template: '<App/>',
    components: { App }
})