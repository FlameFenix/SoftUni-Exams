import { jsonRequest } from "../helpers/jsonRequest.js";


let baseUrl = 'http://localhost:3030/data';

async function getAll(){
    let result = await jsonRequest(`${baseUrl}/cars?sortBy=_createdOn%20desc`);
    return result;
}

async function get(id){
    let result = await jsonRequest(`${baseUrl}/cars/${id}`);
    return result;
}

async function create(item){
    let result = await jsonRequest(`${baseUrl}/cars`, 'Post', item, true);
    return result;
}

async function update(item, id){
    let result = await jsonRequest(`${baseUrl}/cars/${id}`, 'Put', item, true);
    return result;
}

async function getAllListings(){
    let result = await jsonRequest(`${baseUrl}?sortBy=_createdOn%20desc`);
    return result;
}

async function getMyListings(userId){
    let result = await jsonRequest(`${baseUrl}/cars?where=_ownerId%3D%22${userId}%22&sortBy=_createdOn%20desc`);
    return result;
}

async function deleteItem(id){
    let result = await jsonRequest(`${baseUrl}/cars/${id}`, 'Delete', undefined, true);
    return result;
}


export default {
    getAll,
    get,
    create,
    update,
    deleteItem,
    getAllListings,
    getMyListings
}