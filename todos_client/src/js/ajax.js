import axios from 'axios';


const self = {

    get: (queryID, elmID) => {
        return new Promise((resolve, reject) => {
            const queryGetMapping = {
                todolist: 'https://localhost:5001/api/list/tasks',
                userlist: 'https://localhost:5001/api/list/users',
                statuslist: 'https://localhost:5001/api/list/status',
                tasktypelist: 'https://localhost:5001/api/list/tasktypes',
            };
            // console.error(elmID, queryID, queryGetMapping[queryID], axios)
            if (!queryGetMapping[queryID]) {
                reject(`ajax.get error :: invalid query : ${queryID}`);
            }
            axios.get(queryGetMapping[queryID])
                .then(function(response) {
                    console.error(response);
                    if (elmID) {
                        const elm = document.getElementById(elmID);
                        elm.innerHTML = JSON.stringify(response.data, false, 4);
                    }
                    resolve(response.data);
                })
                .catch(function(error) {
                    console.error(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    },

    init: () => {
        window.addEventListener('hashchange', () => {
            //this.selectTab(window.location.hash);
            const pageID = window.location.hash.split('#')[1];
            console.error('ajax change tab :: ' + pageID);
            self.get('myData', pageID);
        });
    },
}
export default self;