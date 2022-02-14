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

        RedirectIfNotFound(){
            this.$router.push({name: 'NotFound', params: { 0: "" } })     
        },
    },

    created() {
        this.loadElements();
    }
}