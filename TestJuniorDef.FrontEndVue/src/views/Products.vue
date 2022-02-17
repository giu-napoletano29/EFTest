<template>
    <div>
        <Header :name="name">
            <div class="btn-toolbar mb-2 mb-md-0">
                <div class="btn-group me-2">
                    <button type="button" class="btn btn-sm btn-outline-primary" @click="OpenNewProduct()">Aggiungi prodotto</button>
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
        >
            <Table
                :loaded="loaded"
                :error="error"
                :nrow="nrow"
                :ntab="ntab"
            />
        </Products>

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
        <RedirectModal
            :success="successModalOpen"
            :OpError="OpError"
            :ErrMsg="ErrMsg"
            @closemodal="CloseModal"
        />  
        <ToastMessage
            :open="showToast"
            @closetoast="CloseToast"
        />      
    </div>
</template>

<script>
    import MainPagesUtils from '@/utilities/MainPagesUtils.js' 
    import BrandsProductsUtils from '@/utilities/BrandsProductsUtils.js'
    import ProductsLeedsUtils from '@/utilities/ProductsLeedsUtils.js' 
    import Header from '@/components/Header.vue'
    import Products from '@/components/Products.vue'
    import Table from '@/components/Table.vue'
    import Pagination from '@/components/Pagination.vue'
    import DeleteModal from '@/components/DeleteModal.vue'
    import RedirectModal from '@/components/RedirectModal.vue'
    import ToastMessage from '@/components/ToastMessage.vue'
    import {Factory} from './../wrappers/Factory'
    const ProductsRepo = Factory.get('products')

    export default {
        mixins: [MainPagesUtils, BrandsProductsUtils, ProductsLeedsUtils],
        
        components: {
            Products,
            Header,
            Pagination,
            DeleteModal,
            Table,
            RedirectModal,
            ToastMessage
        },

        data(){
            return{
                name: 'Prodotti',
                nrow: 7,
                ntab: 3,
                orderbrand: false,
                ordername: false,
                orderprice: false,
                desc: false,
            }
        },

        methods: {
            async loadElements(){
                this.loaded = false
                const {data} = await ProductsRepo.getallpagedsized(this.pageSize, this.page, this.params)
                this.loaded = true
                this.list = data;
            },

            Delete(){
                const data = ProductsRepo.delete(this.idEl)
                this.open = false
                this.DeleteSpecComponent(data) //delete handling generic for the components (BrandsProductsUtils.js)
            },

            OpenNewProduct(){
                this.$router.push('/products/new')
            },

            OpenDetail(val){
                this.$router.push('/products/'+val)
            },

            OpenEdit(id){
                this.$router.push('/products/edit/' + id)
            },

            SpecRedirect(){
                if(this.$route.name!='Products'){
                    this.$router.push({name: 'Products'})
                }
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
                this.ResetOrder()
                this.orderbrand = true
                this.params.orderbyBrand = this.orderbrand
                this.OrderByDesc()
            },
            OrderByName(){
                this.ResetOrder()
                this.ordername = true
                this.params.orderbyName = this.ordername
                this.OrderByDesc()
            },
            OrderByPrice(){
                this.ResetOrder()
                this.orderprice = true
                this.params.orderbyPrice = this.orderprice
                this.OrderByDesc()
            },
            OrderByDesc(){
                this.desc = !this.desc
                this.params.desc = this.desc
                this.loadElements();
            },
        },

        created() {
            this.loadElements();
            this.loadBrands();  //common method in ProductsLeedsUtils.js
        },

        mounted(){
            if(this.$route.name==='ProductsSuccess'){
                //this.successModalOpen = true
                this.OpenToast()
            }else if(this.$route.name==='ProductsError'){
                this.successModalOpen = true
                this.OpError = true
                this.ErrMsg = this.$route.params.Error
            }
        }
    }
</script>