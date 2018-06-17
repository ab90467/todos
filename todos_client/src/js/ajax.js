import axios from 'axios';


const self = {

    _get: (url) => {
        console.error(url);
        return new Promise((resolve, reject) => {
            axios.get(url)
                .then(function(response) {
                    resolve(response.data);
                })
                .catch(function(error) {
                    console.error(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    },

    getList: (queryID, elmID) => {
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
                    resolve(response.data);
                })
                .catch(function(error) {
                    console.error(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    },

    getTasksOnSpesificUser: (userId) => {
        return self._get("https://localhost:5001/api/task/user/" + userId);
    },
    getTaskWithSpesificId: (taskId) => {
        return self._get("https://localhost:5001/api/task/details/" + taskId);
    },

    getUserDetails: (id) => {
        return self._get('https://localhost:5001/api/user/details/' + id);

    },
    _post: (url, data) => {
        console.error(url, data);
    },
    updateUser: (user) => {
        return new Promise((resolve, reject) => {
            axios.post('https://localhost:5001/api/update/user', user).then(function(response) {
                    console.log(response);
                    resolve(response);
                })
                .catch(function(error) {
                    console.log(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    },

    saveNewUser: (user) => {
        return new Promise((resolve, reject) => {
            axios.post('https://localhost:5001/api/save/user', user).then(function(response) {
                    console.log(response);
                    resolve(response);
                })
                .catch(function(error) {
                    console.log(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    }

    /*init: () => {
        window.addEventListener('hashchange', () => {
            //this.selectTab(window.location.hash);
            const pageID = window.location.hash.split('#')[1];
            console.error('ajax change tab :: ' + pageID);
            self.get('myData', pageID);
        });
    },*/
}
export default self;