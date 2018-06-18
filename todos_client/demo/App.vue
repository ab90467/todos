<template>
  <div id="app">
        <tabs>
            <tab id="todolist" name="Todos">
                <h2>List of tasks</h2>
                <div class="app-v-newtask--container">
                  <button class="app-v-newtask--btn" v-show="shownewTaskBtn" aria-desc="click here to create new task" type="button" onclick="document.location.hash='task';return false;">Create new task</button>
                  <button class="app-v-newtask--btn" v-show="shownewTaskBtn" aria-desc="click here to create new user" type="button" onclick="document.location.hash='user';return false;">Create new user</button>
                </div>
                <listtasks></listtasks>
                <shortlinks></shortlinks>

            </tab>
            <tab id="task" name="Task">
                <h2>{{editmodeLabel}} task</h2>
                <Task></Task>
                <shortlinks></shortlinks>

            </tab>
            <tab id="user" name="User">
                <h2>{{editmodeLabel}} user</h2>
                <User></User>
                <shortlinks></shortlinks>

            </tab>
        </tabs>
        <footer>
          <span class="footer-element">ACOS TODO demoapp. For meeting Thuesday 19/06/2018 09:00 </span>
          <span class="footer-element"><a href="https://www.acos.no/om-acos/vil-du-vare-med-pa-var-internasjonale-satsing/" target="_blank"> Description position</a></span>
          <span class="footer-element"><a href="https://github.com/ab90467/todos" target="_blank">GIT repo</a></span>
        </footer>
  </div>      
</template>

<script>
  import Tab from '../src/components/Tab.vue';
  import Tabs from '../src/components/Tabs.vue';
  import Listtasks from '../src/components/ListTasks.vue';
  import User from '../src/components/User.vue';
  import Task from '../src/components/Task.vue';
  import Shortlinks from '../src/components/Shortlinks.vue';

  export default {
    components: { Tab, Tabs, Listtasks, User, Task, Shortlinks },
    data (){ 
      return {
        editmodeLabel : null,
        shownewTaskBtn : false
      };
    },
    
    created() {
      const hashChangeAction = () => {
        this.editmodeLabel = (document.location.hash.match(/.*\/\d$/)) ? "Edit" : "New";   
        this.shownewTaskBtn = (document.location.hash.match(/.*#todolist\/\d$/)) ? false : true;  
      }
      hashChangeAction();
      window.addEventListener('hashchange', () => {
          hashChangeAction();
      });
      
    },
  }
</script>

<style scoped>
  #todoapp {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    color: #2c3e50;
  }
  .app-v-newtask--btn {
    background-color: #4CAF50; /* Green */
    border: none;
    color: white;
    padding: 10px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
  }
  .app-v-newtask--container{
    padding:10px 0 30px;
  }
  footer {
    display: flex;
    align-items: center;
    justify-content: center;
    color :#5e6684;
    height: 100%;
    margin-top: -50px;
  }
  .footer-element{
    padding-right:10px
  }
</style>
<style>

   @import '../src/css/bootstrap.css';
   @import  '../node_modules/vue-loading-overlay/dist/vue-loading.min.css';
</style>
