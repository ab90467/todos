<template>
    <div class="tab-bottom--v-shortlinks">
        <span v-show="display.todo"><a title="go back to overview page" href="#todolist">Dismiss and go back to List of tasks</a> </span>
        <span v-show="display.notWhentaskIsGone"> | </span>
        <span v-show="display.task">  <a title="go to the new task page" href="#task">Create new task</a></span>
        <span v-show="display.user"> | <a title="go to the new user page" href="#user">Create new user</a></span>
    </div>

</template>

<script>
    //v-show="showMenu"
    export default {
        data() {
            return {
                display : {
                    todo : true,
                    notWhentaskIsGone : true,
                    task : true,
                    user : true,
                    
                }
            }
        },
        created() {
            const setDisplayProp = ()=>{
                const h = document.location.hash;
                this.display = {
                    todo : !(h.match(/todolist/)),
                    task : !(h.match(/task/)),
                    user : !(h.match(/user/))
                }
                this.display.notWhentaskIsGone = (this.display.task && this.display.todo);
            }
            
            window.addEventListener('hashchange', () => {
                setDisplayProp();   
            });
            setDisplayProp(); 
        }
    }

</script>


<style>
    .tab-bottom--v-shortlinks {
        padding:10px 0 0 20px;
    }
</style>