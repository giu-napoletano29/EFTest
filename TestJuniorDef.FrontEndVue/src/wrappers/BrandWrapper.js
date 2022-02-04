import Repository from "./Repository";

const resource = "/brands";

export default {
    get(){
        return Repository.get(`${resource}`);
    },

    getallpagedsized(size, page){
        return Repository.get(`${resource}/page/${size}/${page}`);
    },

    getallpaged(page){
        return Repository.get(`${resource}/page/${page}`);
    },

    getById( id ){
        return Repository.get(`${resource}/${id}`);
    },

    create( data ){
        return Repository.post(`${resource}/new`, data);
    },

    update( id, data ){
        return Repository.put(`${resource}/${id}/edit`, data);
    },

    delete( id ){
        return Repository.delete(`${resource}/${id}`);
    }
};
