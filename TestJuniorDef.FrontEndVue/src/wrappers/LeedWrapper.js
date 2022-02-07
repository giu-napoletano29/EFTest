import Repository from "./Repository";

const resource = "/leeds";

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
    }
};
