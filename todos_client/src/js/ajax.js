import axios from 'axios';

(function(axios, todo) {
    //set this up right away
    window.addEventListener('hashchange', () => {
        //this.selectTab(window.location.hash);
        const pageID = window.location.hash.split('#')[1];
        console.error('ajax change tab :: ' + pageID);
        get('myData', pageID);
    });

    const queryGetMapping = {
        'todolist': ':5001/api/values/listtasks'
    }

    const get = (elmID, queryID) => {
        axios.get('/api/values')
            .then(function(response) {
                console.log(response);
                const elm = document.getElementById(id);
                elm.innerHTML = JSON.stringify(response.data, false, 4);
            })
            .catch(function(error) {
                console.log(error);
            });
    };

    todo.ajax = {
        get
    };
})(axios, window.todo = window.todo || {});