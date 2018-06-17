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
          inputType: "text",
          model: "id",
          placeholder: "id"
        },{
          type: "select",
          values : [],
          label: "Status of task",
          model: "taskstatus",
          required: true
        },{
          type: "select",
          values : [],
          label: "Type of task",
          model: "tasktype",
          required: true
        },{
          type: "input",
          inputType: "text",
          label: "Description task",
          model: "descr",
          placeholder: "Description of new task",
          validator: VueFormGenerator.validators.string,
          required: true
        },{
          type: "select",
          values : [],
          label: "Assign user",
          model: "user",
          required: true        
        },{
          type : "submit",
          //onSubmit : this.validateAndSendContent,
          buttonText : "Save task",
          validateBeforeSubmit: true,
          onSubmit(model, schema) {
            // console.log("Form submitted! ");
            // native check -should be able to hook into form generator validation status...
            if(model.uname === "" || model.email === "" || model.skills === ""){
              return;
            }
            ajax[`${(model.id !== "0") ? 'updateUser' : 'saveNewUser'}`]((()=>{
                let obj = {
                    name : model.uname,
                    email : model.email,
                    skills : model.skills
                  }
                if(model.id !== "0"){
                  obj.id = model.id;
                }
                return obj;
              })()
            ).then((resp) =>{
                //console.error('done data: ajax.saveUser() :: '+JSON.stringify(resp));
                model.id = "";
                model.uname = "";
                model.email = "";
                model.skills = "";
                window.location.hash ="todolist";
            });
          },
          styleClasses: "half-width",
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
        fieldIdPrefix: 'user-'
      }
    }
  },
  created() {
    const data = this.schema.fields;
    console.error(data.find(field => field.model === 'user').label);
    ajax.getList('userlist').then((resp) =>{
      data.find(field => field.model === 'user').values = resp;
    });
    ajax.getList('tasktypelist').then((resp) =>{
      data.find(field => field.model === 'tasktype').values = resp;
    });
    ajax.getList('statuslist').then((resp) =>{
      data.find(field => field.model === 'taskstatus').values = resp;
    });
    ajax.getTaskWithSpesificId(3).then((resp)=>{
      console.error(JSON.stringify(resp,false,4));
      /*
      LAGE EDIT TASK!
      id : 0,           
        taskstatus: 1,
        tasktype: 0,
        descr: "",
        user: 0*/
    })
    const hashFunc = ()=>{
      const hash = window.location.hash.split('/')[0];
      if(hash !==  '#user'){
          return;
      }
      const userID = window.location.hash.split('/')[1];
      
      if(userID && userID  !== "0"){
        const that = this;
        ajax.getUserDetails(userID).then((resp) =>{
            
            that.model.id = resp[0].id;
            that.model.uname = resp[0].name;
            that.model.email = resp[0].email;
            that.model.skills = resp[0].skills;
        });
      }else{
        this.model.id = "0";
        this.model.descr = "";
      }
    }
    /*window.addEventListener('hashchange', () => {
      hashFunc()
    });
    hashFunc();*/
  }
}
/*
var fiel=app.$root.form_datos.fields.find(field => field.model === 'dp_provincia');
fiel.values=[{id:"0", name:"example"}];*/

</script>
