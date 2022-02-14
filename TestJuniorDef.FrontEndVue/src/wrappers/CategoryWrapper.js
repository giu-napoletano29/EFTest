import Repository from "./Repository";

const resource = "/categories";

export default {
    get(){
        return Repository.get(`${resource}`);
    },
};
