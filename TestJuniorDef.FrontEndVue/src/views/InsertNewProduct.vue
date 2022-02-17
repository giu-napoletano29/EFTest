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
                    :disabledbrand="false"
                    :brands="brands"
                    :loadedEl="loadedEl"
                    :loadedBrand="loadedBrand"
                    :loadedEditElement="loadedEditElement"
                    :error="error"
                    :elementbyid="elementbyid"
                    :EditMode="EditMode"
                    
                    @input="(newprod) => {product = newprod}"
                />
                <div class="mb-3">      
                    <button type="submit" class="btn btn-primary" :disabled="ShowButton">Invia</button>
                </div>       
            </form>
        </b-container>
    </div>
</template>

<script>
    import InsertPageUtils from '@/utilities/InsertPageUtils.js' 
    import Header from '@/components/Header.vue'
    import InsertNewProduct from '@/components/InsertNewProduct.vue'
    import {Factory} from './../wrappers/Factory'
    const ProductRepo = Factory.get('products')

    export default {
        mixins: [InsertPageUtils],

        components: {
            InsertNewProduct,
            Header,
        },

        data(){
            return{
                name: 'Nuovo prodotto',

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
            async loadProductById(){
                this.loadedEditElement = false
                const {data} = await ProductRepo.getById(this.$route.params.id)
                this.loadedEditElement = true
                this.elementbyid = data;
            },

            RedirectSuccess(){
                this.$router.push('/products/success')
            },

            RedirectError(err){
                this.$router.push({name: 'ProductsError', params: {Error: err}})
            },

            InsertProduct(){
                var resp = null
                if(!this.EditMode){
                    resp = ProductRepo.create(this.product)
                }else{
                    resp = ProductRepo.update(this.elemid, this.product)
                }
                this.ResponseHandler(resp)  //Common in InsertPageUtils.js
            },

            checkForm: function (e) {
                this.errors = [];

                if (!(this.product.BrandId>0)) {
                    this.errors.push('Devi selezionare un brand.');
                }
                if (!this.product.Name || !/\S/.test(this.product.Name)) {
                    this.errors.push('Devi inserire un nome.');
                }
                if (!this.product.ShortDescription || !/\S/.test(this.product.ShortDescription)) {
                    this.errors.push('Devi inserire una piccola descrizione.');
                }
                if (!this.product.Description || !/\S/.test(this.product.Description)) {
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
            if(this.elemid){
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