<template>
  <form>
      <vue-form-generator :schema="schema" :model="model" :options="formOptions">
      </vue-form-generator>
    </form>
</template>



<script>
import VueFormGenerator from "vue-form-generator";
export default {
  
  components:{
    "vue-form-generator": VueFormGenerator.component
  },
  data() {
  return {
    model: {             
      uname: "",
      email: "",
      skills: ""
    },
    schema: {
      fields: [
      {
        type: "input",
        inputType: "text",
        label: "Name",
        model: "uname",
        placeholder: "User name",
        validator: VueFormGenerator.validators.string,
        required: true
      },{
        type: "input",
        inputType: "email",
        label: "E-mail",
        model: "email",
        placeholder: "User's e-mail address",
        validator: VueFormGenerator.validators.email,
        required: true
      },{
        type: "input",
        inputType: "text",
        label: "Skils",
        model: "skills",
        placeholder: "Users speciality",
        validator: "string",
        validator: VueFormGenerator.validators.string,
        required: true
        
      },{
        type : "submit",
        onSubmit : this.validateAndSendContent,
        buttonText : "Save user",
        validateBeforeSubmit: true,
        onSubmit(model, schema) {
				console.log("Form submitted!", model.email);
				//alert("Form submitted!");
			},
			styleClasses: "half-width",
			disabled() {
				console.log("Disabled: ", this.errors.length > 0);
				return this.errors.length > 0;
			}
      }]  
  },
  formOptions: {
            validateAfterLoad: true,
            validateAfterChanged: true,
            fieldIdPrefix: 'user-'
          }
    }
  },
  created() {
          console.error('user schema created!', this)
          
    },
    methods : {
    validateAndSendContent : () => {
      // https://www.bountysource.com/issues/45902231-submit-form-on-keyup-keydown
      console.error('validate!' ,this.model, this.schema)

    }
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