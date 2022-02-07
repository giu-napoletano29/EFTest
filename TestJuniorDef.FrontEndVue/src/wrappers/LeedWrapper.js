import Repository from "./Repository";

const resource = "/leeds";

export default {
    get(){
        return Repository.get(`${resource}`);
    },

    getById( id ){
        return Repository.get(`${resource}/${id}`);
    }
};
