<template>
    <data-tablee :cols="cols" :rows="rows">
         <template slot="row" slot-scope="{ row, index }">
                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.id.substring(0,3) }}</span>
                  </td>
                  <td class="data-tablee-cell">
                    <span class="data-tablee-text">{{ row.name }}</span>
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
                  { label: 'Index', sort: false },
                  { label: 'Name', field: 'name' }
                ],
                rows: getUsers() //ajax.get('myData', 'todolist')
            }
            
        }
      
    };
 function getUsers() {
  return [
    {
      id: "daa3daeb-91c6-42f8-aaf4-4e80c297c000",
      name: "Darsie Tellett",
      email: "dtellett1@behance.net",
      gender: "Male",
      birth_date: "1990-07-12",
      address: {
        country: "United States",
        zip: "84130",
        province: "Utah",
        city: "Salt Lake City",
        street: "Schurz",
        number: "08529"
      }
    },
    {
      id: "37742e75-6839-461a-8727-d00b4333176a",
      name: "Hilarius Jeaffreson",
      email: "hjeaffreson2@ted.com",
      gender: "Male",
      birth_date: "1993-06-13",
      address: {
        country: "United States",
        zip: "34290",
        province: "Florida",
        city: "North Port",
        street: "Milwaukee",
        number: "0"
      }
    },
    {
      id: "a6ebf9ee-3cc5-4384-b73c-594a79207774",
      name: "Lyman Haxley",
      email: "lhaxley3@newyorker.com",
      gender: null,
      birth_date: "1972-12-29",
      address: {
        country: "United States",
        zip: "77260",
        province: "Texas",
        city: "Houston",
        street: "Scoville",
        number: "9514"
      }
    },
    {
      id: "77b39998-188a-4967-a960-12372308345c",
      name: "Lock Kearney",
      email: "lkearney4@ucsd.edu",
      gender: "Male",
      birth_date: "1983-01-17",
      address: {
        country: "United States",
        zip: "43215",
        province: "Ohio",
        city: "Columbus",
        street: "5th",
        number: "36"
      }
    },
    {
      id: "df88b137-fc34-4bd4-b32a-595f79867906",
      name: "Ame Blachford",
      email: "ablachford5@miitbeian.gov.cn",
      gender: "Female",
      birth_date: "1974-03-04",
      address: {
        country: "United States",
        zip: "32803",
        province: "Florida",
        city: "Orlando",
        street: "Hovde",
        number: "6251"
      }
    },
    {
      id: "d18a89e9-a707-4bed-9b75-73a1e712714b",
      name: "Bobbye Seldon",
      email: "bseldon6@artisteer.com",
      gender: "Male",
      birth_date: "1993-07-23",
      address: {
        country: "United States",
        zip: "83757",
        province: "Idaho",
        city: "Boise",
        street: "Delaware",
        number: "9754"
      }
    }
  ];
}
</script>

<style scoped>
    @import 'listTasks.scss';
 
  </style>
  <style>
  span.data-tablee-icon {
      display:none;
  }
  </style>