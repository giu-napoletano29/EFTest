export default{
    methods:{
        RedirectIfNotFound(){
            this.$router.push({name: 'NotFound', params: { 0: "" } })     
        },
    }
}