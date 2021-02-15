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

function getAll() {    
    return fetchWrapper.get(baseUrl);
}

function getById(id) {
    return fetchWrapper.get(`${baseUrl}/${id}`);
}

function create(params) {
    return fetchWrapper.post(baseUrl, params);
}

function update(id, params) {
    console.log('update')
    console.log(params)
    return fetchWrapper.put(`${baseUrl}/${id}`, params);
}

// prefixed with underscored because delete is a reserved word in javascript
function _delete(id) {
    return fetchWrapper.delete(`${baseUrl}/${id}`);
}
