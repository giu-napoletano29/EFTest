<template>
    <div>
        <b-container>
            <Header :name="name">
                <div class="btn-toolbar mb-2 mb-md-0">
                    <div class="btn-group me-2">
                        <button type="button" class="btn btn-sm btn-outline-secondary" @click="OpenNewProduct()">Aggiungi prodotto</button>
                    </div>
                </div>
            </Header>
            <Products
                :list="list"
                :listbrands="listbrands"
                :loaded="loaded"
                :error="error"
                @openDetail="OpenDetail"
                @brandfilter="BrandFilter"
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
    import Products from '@/components/Products.vue'
    import Pagination from '@/components/Pagination.vue'
    import {Factory} from './../wrappers/Factory'
    const ProductsRepo = Factory.get('products')
    const BrandsRepo = Factory.get('brands')

    export default {
        
        components: {
            Products,
            Header,
            Pagination
        },

        data(){
            return{
                name: 'Prodotti',
                list: {},
                listbrands: [],
                maxVisibleButtons: 7,
                page: 1,
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
                const {data} = await ProductsRepo.getallpaged(this.page, this.params)
                this.loaded = true
                this.list = data;
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
            OpenNewProduct(){
                this.$router.push('/products/new')
            },
            OpenDetail(val){
                this.$router.push('/products/'+val)
            },
            BrandFilter(filter){
                if(/^\d+$/.test(filter)){
                    this.params.brand = filter
                }else{
                    delete this.params.brand
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