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
                    :brand="brand"
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
                    <button type="submit" class="btn btn-primary">Aggiungi brand</button>
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
//:product="product[prodIndex-1]" 
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
                loaded: false,
                error: false,
                response: '',

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

            InsertProduct(){
                this.brand.Products = this.product
                const resp = BrandsRepo.create(this.brand)
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

        computed: {
            temp(){
                return this.prod
            }
        },

        created() {
            this.loadElements();
            this.loadBrands();
        }

    }
</script>