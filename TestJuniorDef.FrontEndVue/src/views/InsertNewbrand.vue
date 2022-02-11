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
                    :loaded="loaded"
                    :error="error"
                    :brandbyid="brandbyid"
                    :EditMode="EditMode"
                    @input="(newbrand) => {brand = newbrand}"
                    @addprod="addProd"
                />
                <component
                    v-for="(component, index) in newcomponents"
                    :key="index"
                    :is="component"

                    :list="list"
                    :brands="brands"
                    :loaded="loaded"
                    :error="error"
                    @input="(newprod) => {product[index] = newprod}"
                />
                <div class="mb-3 text-center">      
                    <button type="submit" class="btn btn-primary">{{ButtonText}} brand</button>
                </div>       
            </form>
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import InsertNewbrand from '@/components/InsertNewbrand.vue'
    import InsertNewProduct from '@/components/InsertNewProduct.vue'
    import {Factory} from './../wrappers/Factory'
    const CategoriesRepo = Factory.get('categories')
    const BrandsRepo = Factory.get('brands')
    const Comp = InsertNewProduct

    export default {
        
        components: {
            InsertNewbrand,
            Header,
        },

        data(){
            return{
                name: 'Nuovo Brand',
                newcomponents: [],
                list: [],
                brands: [],
                brandbyid: {},
                loaded: false,
                error: false,
                errors: [],
                brandid: this.$route.params.id,
                EditMode: false,
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

            async loadBrandById(){
                const {data} = await BrandsRepo.getById(this.$route.params.id)
                this.loaded = true
                this.brandbyid = data;
            },

            RedirectSuccess(){
                this.$router.push('/brands/success')
            },

            RedirectError(){
                this.$router.push('/brands/error')
            },

            InsertBrand(){
                let self = this;
                var resp = null
                if(!this.brandid){  //change to EditMode check
                    this.brand.Products = this.product
                    resp = BrandsRepo.create(this.brand)
                }else{
                    delete this.brand.Products
                    resp = BrandsRepo.update(this.brandid, this.brand)
                }
                resp.then(function (response) {
                        if(response.status >= 200 && response.status <= 208){
                            self.RedirectSuccess()
                        }else{
                            self.RedirectError()
                        }
                    });
            },

            addProd () {  
                let prod = { 
                    BrandId: "Seleziona brand",
                    Name: "",
                    ShortDescription: "",
                    Price: 0,
                    Description: "",
                    ProductCategory: [],
                } 
                this.newcomponents.push(Comp)
                this.product.push(prod);
                      
            },

            checkForm: function (e) {
                var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                this.errors = [];
                var temp = [];

                if (!this.brand.BrandName) {
                    temp.push('Devi inserire un nome.');
                }
                if (!re.test(this.brand.Account.Email)) {
                    temp.push('Devi inserire una mail valida.');
                }
                if (!this.brand.Account.Password) {
                    temp.push('Devi inserire una password.');
                }
                if (!this.brand.Description) {
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
            if(!this.brandid){
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