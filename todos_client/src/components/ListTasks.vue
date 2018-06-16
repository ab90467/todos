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
                    <span class="data-tablee-text"><a href="#">see all tasks for user</a></span>
                  </td>
        </template>

    </data-tablee>

</template>

<script>
    import DataTablee from 'vue-data-tablee'
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
                  { label: 'User', field: 'name' }
                ],
                rows: []
            }
            
        },
        created() {
          console.error('created!')
          this.getStatus();
        },
        methods: {
          getStatus(){
            ajax.getList('todolist','myData').then((data)=>{
              this.rows = data;
            })
          } 
          
        }
    };
</script>

<style scoped>
    @import 'listTasks.scss';
 
  </style>
  <style>
  span.data-tablee-icon {
      display:none;
  }
  </style>