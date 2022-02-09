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
                    :productbyid="productbyid"
                    :EditMode="EditMode"
                    
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
//:product="product"
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
                productbyid: {},
                prodid: this.$route.params.id,

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

            async loadProductById(){
                const {data} = await ProductRepo.getById(this.$route.params.id)
                this.loaded = true
                this.productbyid = data;
            },

            InsertProduct(){
                var resp = ""
                if(!this.prodid){
                    resp = ProductRepo.create(this.product)
                }else{
                    resp = ProductRepo.update(this.prodid, this.product)
                }

                this.response = resp
                
            }
        },

        created() {
            if(this.prodid){
                this.loadProductById();
                this.EditMode = true
                this.ButtonText = "Modifica"
                this.name= 'Modifica Prodotto'
            } 
            this.loadElements();
            this.loadBrands();
        }

    }
</script>