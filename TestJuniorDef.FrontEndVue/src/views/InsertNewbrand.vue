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
                <InsertNewbrand
                    :list="list"
                    :brands="brands"
                    :loadedEditElement="loadedEditElement"
                    :error="error"
                    :elementbyid="elementbyid"
                    :EditMode="EditMode"
                    @input="(newbrand) => {brand = newbrand}"
                    @addprod="addProd"
                />
                <component
                    v-for="(component, index) in newcomponents"
                    :key="index"
                    :is="component.c"

                    :index="index"
                    :arrayprod="product[index]"
                    :list="list"
                    :disabledbrand="true"
                    :collapseVal="true"
                    :brands="brands"
                    :loadedEl="loadedEl"
                    :loadedBrand="loadedBrand"
                    :error="error"
                    @input="(newprod) => {product[index] = newprod}"
                >
                    <div class="mb-3" role="group" aria-label="Basic example" style="display: inline-flex;  margin-top: 2rem !important;">
                        <h4 style="margin-top: 0.5rem !important;">Prodotto #{{index+1}}</h4>
                        <button type="button" class="btn" @click="RemoveComponent(index)"><i class="bi bi-x-circle-fill" style="color:#dc3545"></i></button>
                    </div>
                </component>
                <div class="mb-3 text-center">      
                    <button type="submit" class="btn btn-primary mt-3" :disabled="ShowButton">{{ButtonText}} brand</button>
                </div>       
            </form>
        </b-container>
    </div>
</template>

<script>
    import InsertPageUtils from '@/utilities/InsertPageUtils.js' 
    import Commonutils from '@/utilities/Commonutils.js'
    import Header from '@/components/Header.vue'
    import InsertNewbrand from '@/components/InsertNewbrand.vue'
    import InsertNewProduct from '@/components/InsertNewProduct.vue'
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')
    const Comp = InsertNewProduct

    export default {
        mixins: [InsertPageUtils, Commonutils],

        components: {
            InsertNewbrand,
            Header,
        },

        data(){
            return{
                name: 'Nuovo Brand',
                newcomponents: [],
                ButtonText: "Aggiungi",

                brand:{ 
                    BrandName: "",
                    Description: "",
                    Account:{
                        Email: "",
                        Password: "",
                        AccountType: 1,
                    },
                    Products:[]
                },

                product: [],
            }
        },

        methods: {
            async loadBrandById(){
                this.loadedEditElement = false
                const {data} = await BrandsRepo.getById(this.$route.params.id).catch(function (response){
                    return response.response.data.status
                });
                this.CheckEditData(data)
            },

            RedirectSuccess(){
                this.$router.push('/brands/success')
            },

            RedirectError(err){
                this.$router.push({name: 'BrandsError', params: {Error: err}})
            },

            RemoveComponent(val){
                this.product.splice(val, 1)
                this.newcomponents.splice(val, 1)
            },

            InsertBrand(){
                var resp = null
                if(!this.EditMode){ 
                    this.brand.Products = this.product
                    resp = BrandsRepo.create(this.brand)
                }else{
                    delete this.brand.Products
                    resp = BrandsRepo.update(this.elemid, this.brand)
                }
                this.ResponseHandler(resp) //Common in InsertPageUtils.js
            },

            addProd () {  
                let prod = { 
                    BrandId: 0,
                    Name: "",
                    ShortDescription: "",
                    Price: 0,
                    Description: "",
                    ProductCategory: [],
                } 
                this.newcomponents.push({c: Comp, index: this.newcomponents.length})
                this.product.push(prod);
                      
            },

            checkForm: function (e) {
                var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                this.errors = [];
                var temp = [];

                if (!this.brand.BrandName || !/\S/.test(this.brand.BrandName)) {
                    temp.push('Devi inserire un nome.');
                }
                if (!re.test(this.brand.Account.Email)) {
                    temp.push('Devi inserire una mail valida.');
                }
                if (!this.brand.Account.Password || !/\S/.test(this.brand.Account.Password)) {
                    temp.push('Devi inserire una password.');
                }
                if (!this.brand.Description || !/\S/.test(this.brand.Description)) {
                    temp.push('Devi inserire una descrizione.');
                }
                if(this.product.length > 0){
                    this.product.forEach(function (element, i) {
                        if (element.ProductCategory.length < 1) {
                            temp.push('Devi inserire una categoria al prodotto ' + (i+1) + ".");
                        }
                    });
                }
                
                e.preventDefault();
                this.errors = temp
                if(this.errors.length == 0){
                    this.InsertBrand()
                }

            }
        },

        created() {
            if(!this.elemid){
                this.loadElements();
                this.loadBrands();              
            }
            else{
                this.loadBrandById();
                this.EditMode = true
                this.ButtonText = "Modifica"
                this.name= 'Modifica Brand'
            }  
        }

    }
</script>