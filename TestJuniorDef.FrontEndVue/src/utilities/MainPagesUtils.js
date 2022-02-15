//utility with the common methods and data between Brands, Leeds and Products views

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

    computed:{
        totalpages(){
            return Math.ceil(this.list.totalElements/this.list.pageSize)
        }
    },

    methods:{
        pageChange(page) {
            this.page = page
            this.loadElements();
        },
    },
}