<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <ProductDetail
                :list="list"
                :infoRequestPaged="listpaginated"
                :error="error"
                :loaded="loaded"
                @gotoleeds="GoToleeds"
            />
            <Pagination
                :totalPages="totalpages"
                :maxVisibleButtons="maxVisibleButtons"
                :page="page"
                @pageChanged="pageChange"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import ProductDetail from '@/components/ProductDetail.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const ProductsRepo = Factory.get('products')

    export default {
        
        components: {
            Header,
            ProductDetail,
            Pagination
        },

        data(){
            return{
                list: {},
                listpaginated: [],
                loaded: false,
                error: false,
                maxVisibleButtons: 7,
                page: 1,
                pageSize: 5,
            }
        },    

        methods: {
            pageChange(page) {
                this.page = page
                var skip = (this.page - 1 ) * this.pageSize
                var take = skip + this.pageSize
                this.listpaginated = this.list.infoRequest ? this.list.infoRequest.slice(skip, take):[]
            },
            
            async loadElements(){
                let self = this
                const {data} = await ProductsRepo.getById(this.$route.params.id)
                .then(function (response) {
                        if(response.status == 204){
                            self.RedirectIfNotFound()
                            return response
                        }else{
                            return response
                        }
                    })
                .catch(function (error) {
                        if(error.response.status == 400){
                            self.RedirectIfNotFound()
                            return error.response
                        }
                    });
                this.loaded = true
                this.list = data ? data:"";
                this.pageChange(1);
            },
            GoToleeds(){
                this.$router.push({name: 'Leeds', params: {search: this.list.name, brand: this.list.brandId}})
            },

            RedirectIfNotFound(){
                this.$router.push({name: 'NotFound', params: { 0: "" } })     
            }
        },

        computed:{
            name(){
                return this.list.name + " by " + this.list.brandName
            },
            totalpages(){
                return Math.ceil(this.list.infoRequest?.length/this.pageSize)
            }
        },

        created() {
            this.loadElements();
        }
    }
</script>