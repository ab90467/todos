import Vue from 'vue'
//import App from './App.vue'
import Tab from '../src/components/Tab.vue';
import Tabs from '../src/components/Tabs.vue';

import Listtasks from '../src/components/ListTasks.vue';

import Loading from '../src/components/Loading.vue';


import User from '../src/components/User.vue';

//import { VTooltip } from 'v-tooltip';
//VTooltip.options.defaultTemplate = '<div class="foo" role="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div>'
/*import { VTooltip, VPopover, VClosePopover } from 'v-tooltip'

Vue.directive('tooltip', VTooltip)
Vue.directive('close-popover', VClosePopover)
Vue.component('v-popover', VPopover)*/
new Vue({
    el: '#todoapp',
    components: { Tab, Tabs, Loading, Listtasks, User }
});