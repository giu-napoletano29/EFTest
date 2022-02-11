<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <form v-on:submit.prevent="checkForm">
                <p v-if="errors.length">
                    <b>Correggere i seguenti errori:</b>
                    <ul>
                        <li v-for="error in errors" :key="error">{{ error }}</li> 
                    </ul>
                </p>
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
                errors: [],
                response: '',
                productbyid: {},
                prodid: this.$route.params.id,
                EditMode: false,

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

            RedirectSuccess(){
                this.$router.push('/products/success')
            },

            RedirectError(){
                this.$router.push('/products/error')
            },

            InsertProduct(){
                let self = this;
                var resp = null
                if(!this.prodid){
                    resp = ProductRepo.create(this.product)
                }else{
                    resp = ProductRepo.update(this.prodid, this.product)
                }

                resp.then(function (response) {
                        if(response.status >= 200 && response.status <= 208){
                            self.RedirectSuccess()
                        }else{
                            self.RedirectError()
                        }
                    });
                
            },

            checkForm: function (e) {
                this.errors = [];

                if (!(this.product.BrandId>0)) {
                    this.errors.push('Devi selezionare un brand.');
                }
                if (!this.product.Name) {
                    this.errors.push('Devi inserire un nome.');
                }
                if (!this.product.ShortDescription) {
                    this.errors.push('Devi inserire una piccola descrizione.');
                }
                if (!this.product.Description) {
                    this.errors.push('Devi inserire una descrizione.');
                }
                if (this.product.ProductCategory.length < 1) {
                    this.errors.push('Devi selezionare almeno una categoria.');
                }
                
                e.preventDefault();
                
                if(this.errors.length == 0){
                    this.InsertProduct()
                }
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