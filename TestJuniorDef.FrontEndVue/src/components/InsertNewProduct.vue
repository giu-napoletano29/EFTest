<template>
    <div>
        <div class="mb-3">
            <input type="text" class="form-control" id="productname" placeholder="Nome prodotto" v-model="product.Name">
        </div>
        <div class="row">
            <div class="col-10">
                <select class="form-select mb-3" aria-label="Default select example" v-model="product.BrandId">
                    <option value="">Seleziona brand</option>
                    <option v-for="(l,i) in brands" :key="i" :value="l.id">{{l.brandName}}</option>
                </select>
            </div>
            <div class="col">
                <div class="input-group"> 
                    <span class="input-group-text">€</span>
                    <input type="number" class="form-control currency" placeholder="Prezzo" aria-label="price" min="0" step="0.10" data-number-to-fixed="2" data-number-stepfactor="100" v-model="product.Price" />
                </div>
            </div>
        </div>
        <div class="mb-3">
            <input type="text" class="form-control" id="shortdescriptio" placeholder="Piccola descrizione" v-model="product.ShortDescription">
        </div>
        <div class="mb-3">      
            <textarea class="form-control" id="descriptio" rows="3" placeholder="Descrizione" v-model="product.Description"></textarea>
        </div>
        <div class="mb-3 form-check form-check-inline" v-for="(l,i) in list" :key="i">
            <input type="checkbox" class="form-check-input" :id="l.id" v-model="product.ProductCategory" :value="getCatObj(l.id)">
            <label class="form-check-label" for="exampleCheck1">{{l.name}}</label>
        </div>
    </div>
</template>

<script>

export default {

    props: {
        list: Array,
        brands: Array,
        loaded: Boolean,
        error: Boolean,
        EditMode: Boolean,
        productbyid: Object,
    },

    data(){
        return{
            product:{ 
                BrandId: "",
                Name: "",
                ShortDescription: "",
                Price: 0,
                Description: "",
                ProductCategory: [],
            }
        }
    },

    watch: {
        product: {
            deep:true,
            handler(){
                console.log("Emit")
                this.$emit('input', this.product);
            }
        },
        loaded:{
            handler: function(newValue){
                if(this.EditMode && newValue){
                    this.AssingOldValue()
                }
            }
        }
    },

    methods:{
        getCatObj(item){
            return { CategoryId: item,}
        },
        AssingOldValue(){
            this.product.BrandId = this.productbyid.brandId
            this.product.Name = this.productbyid.name
            this.product.ShortDescription = this.productbyid.shortDescription
            this.product.Price = this.productbyid.price
            this.product.Description = this.productbyid.description
            this.product.ProductCategory = this.productbyid.categories.map(a => this.getCatObj(a.id))
        }
    },
}
</script>

<style scoped>
    input.currency {
        text-align: right;
        padding-right: 15px;
    }
</style>
