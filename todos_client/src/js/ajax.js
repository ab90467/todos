import axios from 'axios';


const self = {

    _get: (url) => {
        console.error(url);
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

    getTasksOnSpesificUser: (id) => {

        return new Promise((resolve, reject) => {
            axios.get("https://localhost:5001/api/task/user/" + id)
                .then(function(response) {
                    resolve(response.data);
                })
                .catch(function(error) {
                    console.error(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    },

    getUserDetails: (id) => {
        return new Promise((resolve, reject) => {
            axios.get('https://localhost:5001/api/user/details/' + id)
                .then(function(response) {
                    resolve(response.data);
                })
                .catch(function(error) {
                    console.error(error);
                    reject(`ajax.get error :: ${error}`);
                });
        });
    },
    _user: (url, user) => {
        console.error(url, user);
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