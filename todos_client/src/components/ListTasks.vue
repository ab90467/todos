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
                  <td class="data-tablee-cell">

                      <svg v-on:click="menuAction(row.id)"  version="1.1" id="Capa_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 56 56" style="enable-background:new 0 0 56 56;cursor:pointer;width:20px;height:20px" xml:space="preserve">
                        <g>
                          <path d="M28,0C12.561,0,0,12.561,0,28s12.561,28,28,28s28-12.561,28-28S43.439,0,28,0z M28,54C13.663,54,2,42.336,2,28
                            S13.663,2,28,2s26,11.664,26,26S42.337,54,28,54z"/>
                          <path d="M40,16H16c-0.553,0-1,0.448-1,1s0.447,1,1,1h24c0.553,0,1-0.448,1-1S40.553,16,40,16z"/>
                          <path d="M40,27H16c-0.553,0-1,0.448-1,1s0.447,1,1,1h24c0.553,0,1-0.448,1-1S40.553,27,40,27z"/>
                          <path d="M40,38H16c-0.553,0-1,0.448-1,1s0.447,1,1,1h24c0.553,0,1-0.448,1-1S40.553,38,40,38z"/>
                        </g>
                    </svg>

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
                count : 99
            }
            
        },
        created() {

          const that = this;
          window.addEventListener('hashchange', () => {
            const hash = window.location.hash.split('/')[0];
            if(hash ===  '#todolist'){
                that.getStatus();
            }           
          });
          this.getStatus();
        },
        methods: {
          getStatus(){
            ajax.getList('todolist','myData').then((data)=>{
              this.rows = data;
            })
          },
          menuAction(id){
            console.error('menu click ',id);
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
  }
  </style>