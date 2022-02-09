<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <Leeds
                :list="list"
                :listbrands="listbrands"
                :loaded="loaded"
                :error="error"
                @openDetail="OpenDetail"
                @brandfilter="BrandFilter"
                @search="Search"
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
    import Leeds from '@/components/Leeds.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const LeedsRepo = Factory.get('leeds')
    const BrandsRepo = Factory.get('brands')

    export default {
        
        components: {
            Leeds,
            Header,
            Pagination
        },

        data(){
            return{
                name: 'Richieste info',
                list: {},
                listbrands: [],
                maxVisibleButtons: 7,
                page: 1,
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

        methods: {
            pageChange(page) {
                this.page = page
                this.loadElements();
            },

            async loadElements(){
                const {data} = await LeedsRepo.getallpagedsized(this.pageSize, this.page, this.params)
                this.loaded = true
                this.list = data
            },
            async loadBrands(){
                const {data} = await BrandsRepo.get()
                this.listbrands = data;
            },
            OpenModal(){
                this.open = true
            },
            CloseModal(){
                this.open = false
            },
            OpenDetail(val){
                this.$router.push('/leeds/'+val)
            },
            BrandFilter(filter){
                if(/^\d+$/.test(filter)){
                    this.params.brand = filter
                }else{
                    delete this.params.brand
                }
                this.loadElements();
            },
            Search(filter){
                if(filter){
                    this.params.search = filter
                }else{
                    delete this.params.search
                }
                this.loadElements();
            },
        },

        created() {
            this.loadElements();
            this.loadBrands();
        },
    }
</script>