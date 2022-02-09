<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <form v-on:submit.prevent="InsertProduct">
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
                <div class="mb-3">      
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
                response: '',
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

            InsertProduct(){
                var resp = ""
                if(!this.brandid){
                    this.brand.Products = this.product
                    resp = BrandsRepo.create(this.brand)
                }else{
                    delete this.brand.Products
                    resp = BrandsRepo.update(this.brandid, this.brand)
                }
                this.response = resp
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