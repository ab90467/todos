<template>
  <form>
      <vue-form-generator :schema="schema" :model="model" :options="formOptions">
      </vue-form-generator>
    </form>
</template>



<script>
import VueFormGenerator from "vue-form-generator";
import ajax from '../js/ajax.js';

export default {
  
  components:{
    "vue-form-generator": VueFormGenerator.component
  },
  data() {
    return {
      model: {  
        id : 0,           
        taskstatus: 1,
        tasktype: 0,
        descr: "",
        user: 0
      },
      schema: {
        fields: [
          {
          type: "input",
          inputType: "hidden",
          model: "id",
          placeholder: "id"
        },{
          type: "select",
          values : [],
          label: "Status of task",
          model: "taskstatus",
          required: true,
          disabled() {
            return (this.model.id == 0)
          },
          styleClasses:'col-md-7'
        },{
          type: "select",
          values : [],
          label: "Type of task",
          model: "tasktype",
          required: true,
          styleClasses:'col-md-7'
        },{
          type: "input",
          inputType: "text",
          label: "Description task",
          model: "descr",
          placeholder: "Description of new task",
          validator: VueFormGenerator.validators.string,
          required: true,
          styleClasses:'col-md-7'
        },{
          type: "select",
          values : [],
          label: "Assign user",
          model: "user",
          required: true,
          styleClasses:'col-md-7',
          selectOptions: {
    hideNoneSelectedText: true,
  }       
        },{
          type : "submit",
          //onSubmit : this.validateAndSendContent,
          buttonText : "Save task",
          validateBeforeSubmit: true,
          onSubmit(model, schema) {
            console.log("Form submitted! ", this);
            // native check -should be able to hook into form generator validation status...
            if(model.descr === "" || model.taskstatus === 0 || model.tasktype === 0 || model.user === 0  ){
              return;
            }
            ajax[`${(model.id !== "0") ? 'updateTask' : 'saveNewTask'}`]((()=>{
                let obj = {
                  taskStatusID : model.taskstatus,
                  typeID : model.tasktype,
                  description : model.descr,
                  userID : model.user
                }
                if(model.id !== "0"){
                  obj.id = model.id;
                }
                return obj;
              })()
            ).then((resp) =>{
                model.id = "0";
                model.descr = "";          
                model.taskstatus = 1;
                model.tasktype = 0;
                model.user = 0;
                window.location.hash ="todolist";
            });
          },
          styleClasses:'col-md-8'
          /*disabled() {
            return false;
            console.log("Disabled: ", this.errors.length > 0);
            return this.errors.length > 0;
          }*/
        }]  
      },
      formOptions: {
        //validateAfterLoad: true,
        validateAfterChanged: true,
        fieldIdPrefix: 'task-'
      }
    }
  },
  created() {
    const data = this.schema.fields;
    ajax.getList('userlist').then((resp) =>{
      data.find(field => field.model === 'user').values = resp;
    });
    ajax.getList('tasktypelist').then((resp) =>{
      data.find(field => field.model === 'tasktype').values = resp;
    });
    ajax.getList('statuslist').then((resp) =>{
      data.find(field => field.model === 'taskstatus').values = resp;
    });
    const clearFields = () =>{
      const m = this.model;
      m.id = "0";
      m.descr = "";          
      m.taskstatus = 1;
      m.tasktype = 0;
      m.user = 0;
      data.find(field => field.type === 'submit').buttonText = "Save task";
    }
    const hashChangeAction = () => {
      const hash = window.location.hash.split('/')[0];
      if(hash !==  '#task'){
          return;
      }
      const taskId = window.location.hash.split('/')[1];
      
      if(taskId && taskId  !== "0"){
        const m = this.model;
        ajax.getTaskWithSpesificId(taskId).then((resp)=>{
          const d = (typeof resp[0] === "object") ?  resp[0] :false;
          if(!d){
            clearFields();
            return;
          }
          m.id = d.id;          
          m.taskstatus = d.taskStatusID;
          m.tasktype = d.typeID;
          m.descr = d.description;
          m.user = d.userID;
          data.find(field => field.type === 'submit').buttonText = "Change task";
        });
      }else{
        clearFields();
      }
    }
    hashChangeAction();
    window.addEventListener('hashchange', () => {
      hashChangeAction();
    });
  }
}
/*
var fiel=app.$root.form_datos.fields.find(field => field.model === 'dp_provincia');
fiel.values=[{id:"0", name:"example"}];*/

</script>
