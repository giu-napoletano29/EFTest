<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <InsertNewProduct
                :list="list"
                :brands="brands"
                :loaded="loaded"
                :error="error"
                @submitForm="InsertProduct"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import InsertNewProduct from '@/components/InsertNewProduct.vue'
    import {Factory} from './../wrappers/Factory'
    const CategoriesRepo = Factory.get('categories')
    const BrandsRepo = Factory.get('brands')
    const ProductRepo = Factory.get('products')

    export default {
        
        components: {
            InsertNewProduct,
            Header,
        },

        data(){
            return{
                name: 'Nuovo prodotto',
                list: [],
                brands: [],
                loaded: false,
                error: false,
                response: '',
            }
        },

        methods: {
            async loadElements(){
                const {data} = await CategoriesRepo.get()
                this.loaded = true
                this.list = data;
            },

            async loadBrands(){
                const {data} = await BrandsRepo.get()
                this.loaded = true
                this.brands = data;
            },

            InsertProduct(product){
                const resp = ProductRepo.create(product)
                this.response = resp
                console.log("Test: " + resp.response)
            }
        },

        created() {
            this.loadElements();
            this.loadBrands();
        }

    }
</script>