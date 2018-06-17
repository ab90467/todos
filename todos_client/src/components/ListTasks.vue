<template>
    <data-tablee :cols="cols" :rows="rows">
         <template slot="row" slot-scope="{ row, index }">

                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.id }}</span>
                  </td>
                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.tasktype }}</span>
                  </td>
                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.status }}</span>
                  </td>
                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.descr }}</span>
                  </td>
                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.user }}</span>
                  </td>
                  <td v-show="showMenu" class="data-tablee-cell">

                      <svg v-on:click="menuAction(row.userId)"  version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 56 56" 
                          style="enable-background:new 0 0 56 56;cursor:pointer;width:20px;height:20px;" 
                        xml:space="preserve">
                        <g>
                          <path d="M28,0C12.561,0,0,12.561,0,28s12.561,28,28,28s28-12.561,28-28S43.439,0,28,0z M28,54C13.663,54,2,42.336,2,28
                            S13.663,2,28,2s26,11.664,26,26S42.337,54,28,54z"
                            style="fill:#5e6684;stroke-width:10"
                            />
                          <path style="fill:#5e6684;" d="M40,16H16c-0.553,0-1,0.448-1,1s0.447,1,1,1h24c0.553,0,1-0.448,1-1S40.553,16,40,16z"/>
                          <path style="fill:#5e6684;" d="M40,27H16c-0.553,0-1,0.448-1,1s0.447,1,1,1h24c0.553,0,1-0.448,1-1S40.553,27,40,27z"/>
                          <path style="fill:#5e6684;" d="M40,38H16c-0.553,0-1,0.448-1,1s0.447,1,1,1h24c0.553,0,1-0.448,1-1S40.553,38,40,38z"/>
                        </g>
                    </svg>
                    </td>
                 <td v-show="showBackLink" class="data-tablee-cell">
                    <span class="data-tablee-text"><a href="#todolist">BACK</a></span>
                  </td>
                  
        </template>

    </data-tablee>

</template>

<script>
    import DataTablee from 'vue-data-tablee';
    import ajax from '../js/ajax.js';

    export default {
        components: {
            DataTablee
        },
        
        data() {
            return {
                cols : [
                  { label: 'ID', sort: false ,field: 'id'},
                  { label: 'Type', sort: false ,field: 'tasktype'},
                  { label: 'Status', field: 'status'},
                  { label: 'Description', sort: false, field: 'descr' },
                  { label: 'User',field: 'user' }
                ],
                rows: [],
                showMenu : false

            }
            
        },
        created() {


          //const that = this;
          const hashFunc = ()=>{
            const hash = window.location.hash.split('/')[0];
            if(hash ===  '#todolist'){
              this.getStatus(window.location.hash.split('/')[1]);
            }
          }
          
          window.addEventListener('hashchange', () => {
            hashFunc();        
          });
          hashFunc(); 
        },
        methods: {
          getStatus(userId){
            
            const fn = userId ? 'getTasksOnSpesificUser' : 'getList';
            const param = userId ?  userId : 'todolist';
            //console.error('user:', userId,fn, param);
            ajax[fn](param).then((data)=>{
              this.rows = data;
              this.showMenu = !userId;
              this.showBackLink = userId;
            })
          },
          menuAction(id){
            // console.error('menu click ',id);
            window.location.hash = '#todolist/' + id;
          }
          
        }
    };
</script>

<style scoped>
    @import 'listTasks.scss';
 
  </style>
  <style>
  .-unsortable > span.data-tablee-icon {
      display:none;
  }
  th.-sortable {
    cursor: pointer;
    color : #5e6684;
  }
  </style>