<template>
    <div>
        <Header :name="name"/>
        <Leeds
            :list="list"
            :listbrands="listbrands"
            :loaded="loaded"
            :error="error"
            :params="params"
            @openDetail="OpenDetail"
            @brandfilter="BrandFilter"
            @search="Search"
            @orderby="Orderby"
        >
            <Table
                :loaded="loaded"
                :error="error"
                :nrow="nrow"
                :ntab="ntab"
            />
        </Leeds>
        <Pagination
            :totalPages="totalpages"
            :maxVisibleButtons="maxVisibleButtons"
            :page="page"
            @pageChanged="pageChange"
        />
    </div>
</template>

<script>
    import MainPagesUtils from '@/utilities/MainPagesUtils.js' 
    import ProductsLeedsUtils from '@/utilities/ProductsLeedsUtils.js' 
    import Header from '@/components/Header.vue'
    import Leeds from '@/components/Leeds.vue'
    import Pagination from '@/components/Pagination.vue'
    import Table from '@/components/Table.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')

    export default {
        mixins: [MainPagesUtils, ProductsLeedsUtils],
        
        components: {
            Leeds,
            Header,
            Pagination,
            Table
        },

        data(){
            return{
                name: 'Richieste info',
                nrow: 6,
                ntab: 4,
                orderDesc: false,
            }
        },

        methods: {
            async loadElements(){
                const {data} = await LeedsRepo.getallpagedsized(this.pageSize, this.page, this.params)
                this.params = {}
                this.loaded = true
                this.list = data
            },

            OpenDetail(val){
                this.$router.push('/leeds/'+val)
            },

            Search(filter){
                if(filter){
                    this.params.search = filter
                }else{
                    delete this.params.search
                }
                this.loadElements();
            },
            Orderby(){
                this.orderDesc = !this.orderDesc
                this.params.orderdesc = this.orderDesc
                this.loadElements();
            }
        },

        created() {
            if(this.$route.params){
                this.params.brand = this.$route.params.brand
                this.params.search = this.$route.params.search
                this.params.productId = this.$route.params.productId
            }
            this.loadElements();
            this.loadBrands();
        },
    }
</script>