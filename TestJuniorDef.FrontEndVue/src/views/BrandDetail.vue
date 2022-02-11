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
                pageSize: 5,
            }
        },

        methods: {
            pageChangeComponent(skip, take){
                this.listpaginated = this.list.products ? this.list.products.slice(skip, take):[]
            },
            
            async loadElements(){
                const {data} = await BrandsRepo.getById(this.$route.params.id)
                this.loadElementsComponent(data)
            },
        },

        computed:{
            name(){
                return 'Brand ' + this.list.brandName
            },
            totalpages(){
                return Math.ceil(this.list.products?.length/this.pageSize)
            }
        },
    }
</script>