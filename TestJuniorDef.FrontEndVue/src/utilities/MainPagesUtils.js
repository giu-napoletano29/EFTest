export default{
    data(){
        return{
            list: {},
            page: 1,
            maxVisibleButtons: 7,
            pageSize: 10,
            loaded: false,
            error: false,
            params: {},
        }
    },

    methods:{
        pageChange(page) {
            this.page = page
            this.loadElements();
        },
    },
}