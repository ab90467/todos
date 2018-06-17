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
        id : "",           
        uname: "",
        email: "",
        skills: ""
      },
      schema: {
        fields: [
          {
          type: "input",
          inputType: "hidden",
          model: "id",
          placeholder: "id"
        },{
          type: "input",
          inputType: "text",
          label: "Name",
          model: "uname",
          placeholder: "User name",
          validator: VueFormGenerator.validators.string,
          required: true,
          styleClasses:'col-md-7'
        },{
          type: "input",
          inputType: "email",
          label: "E-mail",
          model: "email",
          placeholder: "User's e-mail address",
          validator: VueFormGenerator.validators.email,
          required: true,
          styleClasses:'col-md-7'
        },{
          type: "input",
          inputType: "text",
          label: "Skils",
          model: "skills",
          placeholder: "Users speciality",
          validator: "string",
          validator: VueFormGenerator.validators.string,
          required: true,
          styleClasses:'col-md-7'
        },{
          type : "submit",
          //onSubmit : this.validateAndSendContent,
          buttonText : "Save user",
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
                //schema.fields.find(field => field.type === 'submit').buttonText = "Save user";
                window.location.hash ="todolist";
            });
          },
          styleClasses:'col-md-7'
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
            that.schema.fields.find(field => field.type === 'submit').buttonText = "Change user";
        });
      }else{
        this.model.id = "0";
        this.model.uname = "";
        this.model.email = "";
        this.model.skills = "";
        this.errors = 0;
        this.schema.fields.find(field => field.type === 'submit').buttonText = "Save user";
      }
    }
    window.addEventListener('hashchange', () => {
      hashFunc()
    });
    hashFunc();
  }
}
/*

    // String validator with custom error messages
    validator: validators.string.locale({
        fieldIsRequired: "The password is required!",
        textTooSmall: "Password must be at least {1} characters"
    })
    */
</script>
<style>
input[type=submit] {
    padding:5px 15px; 
    background:#ccc; 
    border:0 none;
    cursor:pointer;
    -webkit-border-radius: 5px;
    border-radius: 5px; 
}
</style>