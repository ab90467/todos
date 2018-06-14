import axios from 'axios';


const self = {

    get: (elmID, queryID) => {
        const queryGetMapping = {
            todolist: 'https://localhost:5001/api/values/listtasks',
            tasklist: 'https://localhost:5001/api/values/listtasktype',
            userlist: 'https://localhost:5001/api/values/listusers',
        };
        // console.error(elmID, queryID, queryGetMapping[queryID], axios)
        if (!queryGetMapping[queryID]) {
            return;
        }
        axios.get(queryGetMapping[queryID])
            .then(function(response) {
                console.error(response);
                const elm = document.getElementById(elmID);
                elm.innerHTML = JSON.stringify(response.data, false, 4);
                return response.data;
            })
            .catch(function(error) {
                console.error(error);
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