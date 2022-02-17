<template>
    <div :id="indexId">
        <slot></slot>
        <div>
            <div class="mb-3">
                <input type="text" class="form-control" placeholder="Nome prodotto" v-model="product.Name" required>
            </div>
            <div class="row">
                <div class="col-10">
                    <select v-show="loadedBrand && !disabledbrand" class="form-select mb-3" aria-label="Default select example" v-model="product.BrandId" :disabled="disabledbrand" :required="!disabledbrand">
                        <option value="">Seleziona brand</option>
                        <option v-for="(l,i) in brands" :key="i" :value="l.id">{{l.brandName}}</option>
                    </select>
                    <p v-show="!loadedBrand">Caricamento brand...</p>
                </div>
                <div class="col-2">
                    <div class="input-group mb-3"> 
                        <span class="input-group-text">€</span>
                        <input type="number" class="form-control currency" placeholder="Prezzo" aria-label="price" min="0" step="0.01" data-number-to-fixed="2" data-number-stepfactor="100" v-model="product.Price" required/>
                    </div>
                </div>
            </div>
            <div class="mb-3">
                <input type="text" class="form-control" placeholder="Piccola descrizione" v-model="product.ShortDescription" required>
            </div>
            <div class="mb-3">      
                <textarea class="form-control" rows="3" placeholder="Descrizione" v-model="product.Description" required></textarea>
            </div>
            <div class="row">
                <div v-show="loadedEl" class="mb-3 form-check form-check-inline col" v-for="(l,i) in list" :key="i">
                    <input type="checkbox" class="form-check-input" :id="l.id" v-model="product.ProductCategory" :value="getCatObj(l.id)">
                    <label class="form-check-label" for="exampleCheck1">{{l.name}}</label>
                </div>
            </div>
            <p v-show="!loadedEl">Caricamento categorie...</p>
        </div>
    </div>
</template>

<script>
import { Collapse } from 'bootstrap'
export default {

    props: {
        list: Array,
        brands: Array,
        loadedEl: Boolean,
        loadedBrand: Boolean,
        loadedEditElement: Boolean,
        error: Boolean,
        EditMode: Boolean,
        elementbyid: Object,
        disabledbrand: Boolean,
        arrayprod: Object,
        collapseVal: {
            default: false,
            type: Boolean
        },
        index: {
            default: 0,
            type: Number
        },
    },

    data(){
        return{
            collapsEl: {},
            product:{ 
                BrandId: this.disabledbrand ? 0:"",
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
                this.$emit('input', this.product);
            }
        },
        loadedEditElement:{
            handler: function(newValue){
                if(this.EditMode && newValue){
                    this.AssingOldValue()
                }
            }
        },
        arrayprod:{
            deep:true,
            handler(){
                this.product = {...this.arrayprod}
            }
        },
        
        collapseVal: function(val) {
            if(val){
                this.collapsEl.show()
            }else{
                this.collapsEl.hide()
            }
        }
    },

    computed:{
        indexId(){
            return this.collapseVal ? "collapse" + this.index : "insert"
        }
    },

    methods:{
        getCatObj(item){
            return { CategoryId: item,}
        },
        AssingOldValue(){
            this.product.BrandId = this.elementbyid.brandId
            this.product.Name = this.elementbyid.name
            this.product.ShortDescription = this.elementbyid.shortDescription
            this.product.Price = this.elementbyid.price
            this.product.Description = this.elementbyid.description
            this.product.ProductCategory = this.elementbyid.categories.map(a => this.getCatObj(a.id))
        },

        CollapsShow(){
            var collapseElementList = document.getElementById('collapse' + this.index)
            if(collapseElementList){
                this.collapsEl = new Collapse(collapseElementList)
            }
        }
    },

    mounted(){
        this.CollapsShow()
    },
}
</script>

<style scoped>
    input.currency {
        text-align: right;
        padding-right: 15px;
    }
</style>
