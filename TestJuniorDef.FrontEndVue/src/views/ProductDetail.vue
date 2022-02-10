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
                this.listpaginated = this.list.infoRequest.slice(skip, take)
            },
            
            async loadElements(){
                const {data} = await ProductsRepo.getById(this.$route.params.id)
                this.loaded = true
                this.list = data;
                this.pageChange(1);
            },
            GoToleeds(){
                this.$router.push({name: 'Leeds', params: {search: this.list.name, brand: this.list.brandId}})
            },
        },

        computed:{
            name(){
                return 'Brand ' + this.list.brandName
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