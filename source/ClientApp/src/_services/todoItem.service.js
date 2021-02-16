import { Config } from '../_api';
import { fetchWrapper } from '@/_helpers';

const config = Config();
const baseUrl = `${config.apiUrl}/todoItem`;

export const todoItemService = {
    getAll,
    getById,
    create,
    update,
    delete: _delete
};

function getAll(id) {    
    console.log(id);
    return fetchWrapper.get(`${config.apiUrl}/todoList/todo_item/${id}`);
}

function getById(id) {
    return fetchWrapper.get(`${baseUrl}/${id}`);
}

function create(params) {
    console.log('create')
    console.log(baseUrl)
    console.log(params)
    return fetchWrapper.post(baseUrl, params);
}

function update(id, params) {
    return fetchWrapper.put(`${baseUrl}/${id}`, params);
}

// prefixed with underscored because delete is a reserved word in javascript
function _delete(id) {
    return fetchWrapper.delete(`${baseUrl}/${id}`);
}
