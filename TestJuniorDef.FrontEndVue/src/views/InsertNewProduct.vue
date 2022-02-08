<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <form v-on:submit.prevent="InsertProduct">
                <InsertNewProduct
                    :list="list"
                    :brands="brands"
                    :loaded="loaded"
                    :error="error"
                    :product="product"
                    @input="(newprod) => {product = newprod}"
                />
                <div class="mb-3">      
                    <button type="submit" class="btn btn-primary">Submit</button>
                </div>       
            </form>
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

                product:{ 
                    BrandId: "Seleziona brand",
                    Name: "",
                    ShortDescription: "",
                    Price: 0,
                    Description: "",
                    ProductCategory: [],
                }
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

            InsertProduct(){
                const resp = ProductRepo.create(this.product)
                this.response = resp
                
            }
        },

        created() {
            this.loadElements();
            this.loadBrands();
        }

    }
</script>