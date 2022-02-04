//Qui chiamata ad API e poi passare contenuto ai componenti (template)
import Repository from "./Repository";

const resource = "/brands";

export default {
    get(){
        return Repository.get(`${resource}`);
    },

    getallpaged(size, page){
        return Repository.get(`${resource}/page/${size}/${page}`);
    },

    getById( id ){
        return Repository.get(`${resource}/${id}`);
    },

    create( data ){
        return Repository.post(`${resource}/new`, data);
    },

    update( id, data ){
        //return an Axios request (promise)
        //return axios.get()
        // data._method = 'PUT';
        // return axios.post( 'https://music.com/api/v1/songs/'+id, data );
        return Repository.put(`${resource}/${id}/edit`, data);
    },

    delete( id ){
        return Repository.delete(`${resource}/${id}`);
    }
};
