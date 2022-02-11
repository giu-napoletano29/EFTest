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
    import DetailPagesUtils from '@/utilities/DetailPagesUtils.js' 
    import Header from '@/components/Header.vue'
    import ProductDetail from '@/components/ProductDetail.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const ProductsRepo = Factory.get('products')

    export default {
        
        mixins: [DetailPagesUtils],

        components: {
            Header,
            ProductDetail,
            Pagination
        },

        data(){
            return{
                pageSize: 5,
            }
        },    

        methods: {
            pageChangeComponent(skip, take){
                this.listpaginated = this.list.infoRequest ? this.list.infoRequest.slice(skip, take):[]
            },
            
            async loadElements(){
                const {data} = await ProductsRepo.getById(this.$route.params.id)
                this.loadElementsComponent(data)
            },

            GoToleeds(){
                this.$router.push({name: 'Leeds', params: {search: this.list.name, brand: this.list.brandId}})
            },
        },

        computed:{
            name(){
                return this.list.name + " by " + this.list.brandName
            },
            totalpages(){
                return Math.ceil(this.list.infoRequest?.length/this.pageSize)
            }
        },
    }
</script>