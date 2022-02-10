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
                :desc="desc"
                :orderbrand="orderbrand"
                :ordername="ordername"
                :orderprice="orderprice"
                @openmodal="OpenModal"
                @openDetail="OpenDetail"
                @brandfilter="BrandFilter"
                @orderbybrand="OrderByBrand"
                @orderbyname="OrderByName"
                @orderbyprice="OrderByPrice"
                @openedit="OpenEdit"
            />
            <Pagination
                :totalPages="totalpages"
                :maxVisibleButtons="maxVisibleButtons"
                :page="page"
                @pageChanged="pageChange"
            />
            <DeleteModal
                :open="open"
                @closemodal="CloseModal"
                @delete="Delete"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Products from '@/components/Products.vue'
    import Pagination from '@/components/Pagination.vue'
    import DeleteModal from '@/components/DeleteModal.vue'
    import {Factory} from './../wrappers/Factory'
    const ProductsRepo = Factory.get('products')
    const BrandsRepo = Factory.get('brands')

    export default {
        
        components: {
            Products,
            Header,
            Pagination,
            DeleteModal
        },

        data(){
            return{
                name: 'Prodotti',
                list: {},
                listbrands: [],
                maxVisibleButtons: 7,
                page: 1,
                pageSize: 10,
                loaded: false,
                error: false,
                params: {},
                orderbrand: false,
                ordername: false,
                orderprice: false,
                desc: false,
                idEl: 0,
                open: false,
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
                this.loaded = false
                const {data} = await ProductsRepo.getallpagedsized(this.pageSize, this.page, this.params)
                this.loaded = true
                this.list = data;
            },
            async loadBrands(){
                const {data} = await BrandsRepo.get()
                this.listbrands = data;
            },

            OpenModal(id){
                this.idEl = id
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

            ResetOrder(){
                this.orderbrand = false
                this.ordername = false
                this.orderprice = false
                delete this.params.orderbyBrand
                delete this.params.orderbyName
                delete this.params.orderbyPrice
            },

            OrderByBrand(){
                //let orderVal = !this.orderbrand
                this.ResetOrder()
                this.orderbrand = true
                this.params.orderbyBrand = this.orderbrand
                this.OrderByDesc()
                this.loadElements();
            },
            OrderByName(){
                //let orderVal = !this.ordername
                this.ResetOrder()
                this.ordername = true
                this.params.orderbyName = this.ordername
                this.OrderByDesc()
                this.loadElements();
            },
            OrderByPrice(){
                //let orderVal = !this.orderprice
                this.ResetOrder()
                this.orderprice = true
                this.params.orderbyPrice = this.orderprice
                this.OrderByDesc()
                this.loadElements();
            },
            OrderByDesc(){
                this.desc = !this.desc
                this.params.desc = this.desc
                this.loadElements();
            },
            OpenEdit(id){
                this.$router.push('/products/edit/' + id)
            },
            Delete(){
                const {data} = ProductsRepo.delete(this.idEl)
                this.open = false
                this.response = data
            },
        },

        created() {
            this.loadElements();
            this.loadBrands();
        },
    }
</script>