<template>
    <div>
        <b-container>
            <Header :name="name">
                <div class="btn-toolbar mb-2 mb-md-0">
                    <div class="btn-group me-2">
                        <button type="button" class="btn btn-sm btn-outline-secondary" @click="OpenNewBrand()">Aggiungi prodotto</button>
                    </div>
                </div>
            </Header>
            <Products
                :list="list"
                :loaded="loaded"
                :error="error"
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
                maxVisibleButtons: 7,
                page: 1,
                loaded: false,
                error: false,
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
                const {data} = await ProductsRepo.getallpaged(this.page)
                this.loaded = true
                this.list = data;
            },
            OpenModal(){
                this.open = true
            },
            CloseModal(){
                this.open = false
            },
            OpenNewBrand(){
                this.$router.push('/products/new')
            }
        },

        created() {
            this.loadElements();
        },
    }
</script>