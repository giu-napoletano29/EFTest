<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <BrandDetail
                :list="list"
                :productsPaged="listpaginated"
                :error="error"
                :loaded="loaded"
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
    import utils from '@/utilities/utils.js' 
    import Header from '@/components/Header.vue'
    import BrandDetail from '@/components/BrandDetail.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')

    export default {
        mixins: [utils],
        
        components: {
            Header,
            BrandDetail,
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
                this.listpaginated = this.list.products ? this.list.products.slice(skip, take):[]
            },
            
            async loadElements(){
                const {data} = await BrandsRepo.getById(this.$route.params.id)
                if(!data){
                    this.RedirectIfNotFound()
                }
                this.loaded = true
                this.list = data ? data:"";
                this.pageChange(1);
            },

            // RedirectIfNotFound(){
            //     this.$router.push({name: 'NotFound', params: { 0: "" } })     
            // },
        },

        computed:{
            name(){
                return 'Brand ' + this.list.brandName
            },
            totalpages(){
                return Math.ceil(this.list.products?.length/this.pageSize)
            }
        },

        created() {
            this.loadElements();
        }
    }
</script>