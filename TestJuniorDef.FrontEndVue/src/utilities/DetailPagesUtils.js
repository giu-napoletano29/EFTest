//utility with the common methods and data between the detail views

export default{
    data(){
        return{
            list: {},
            listpaginated: [],
            loaded: false,
            error: false,
            maxVisibleButtons: 7,
            page: 1,
        }
    },

    methods:{
        pageChange(page) {
            this.page = page
            var skip = (this.page - 1 ) * this.pageSize
            var take = skip + this.pageSize
            this.pageChangeComponent(skip, take)
        },

        loadElementsComponent(data){
            if(!data){
                this.RedirectIfNotFound()
            }
            this.loaded = true
            this.list = data ? data:"";
            this.pageChange(1);
        },
    },

    created() {
        this.loadElements();
    }
}