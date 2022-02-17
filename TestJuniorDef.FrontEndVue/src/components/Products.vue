<template>
    <div>
        <div class="products">
            <table class="table table-striped table-hover">
                <thead>
                    <tr>
                        <th scope="col" class="col-2 v-align s-pointer" @click="OrderByBrand()" > 
                            <div class="row">
                                <div class="col-2">
                                    Brand
                                </div> 
                                <div class="text-right col">
                                    <div v-show="!this.orderbrand">
                                        <i class="fa fa-fw fa-sort"></i>
                                    </div>                              
                                    <div v-show="this.orderbrand">
                                        <div v-show="!this.desc">
                                            <i class="fa fa-fw fa-sort-asc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-desc"></i>
                                        </div>
                                        <div v-show="this.desc">
                                            <i class="fa fa-fw fa-sort-desc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-asc"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </th>
                        <th scope="col" class="col-4 v-align s-pointer" @click="OrderByName()"> 
                            <div class="row">
                                <div class="col-2">
                                    Prodotto
                                </div> 
                                <div class="text-right col">
                                    <div v-show="!this.ordername">
                                        <i class="fa fa-fw fa-sort"></i>
                                    </div>                              
                                    <div v-show="this.ordername">
                                        <div v-show="!this.desc">
                                            <i class="fa fa-fw fa-sort-asc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-desc"></i>
                                        </div>
                                        <div v-show="this.desc">
                                            <i class="fa fa-fw fa-sort-desc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-asc"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </th>
                        <th scope="col" class="col-3 v-align"> Categorie </th>
                        <th scope="col" class="col-2 v-align s-pointer" @click="OrderByPrice()">
                            <div class="row">
                                <div class="col-2">
                                    Prezzo
                                </div> 
                                <div class="text-right col">
                                    <div v-show="!this.orderprice">
                                        <i class="fa fa-fw fa-sort"></i>
                                    </div>                              
                                    <div v-show="this.orderprice">
                                        <div v-show="!this.desc">
                                            <i class="fa fa-fw fa-sort-asc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-desc"></i>
                                        </div>
                                        <div v-show="this.desc">
                                            <i class="fa fa-fw fa-sort-desc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-asc"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </th>
                        <th/>
                    </tr>
                    <tr class="table-light">
                        <th> 
                            <select class="form-select" aria-label="Default select example" v-model="key" @change="onChange()">
                                <option selected>Tutti i brand</option>
                                <option v-for="(l,i) in listbrands" :key="i" :value="l.id">{{l.brandName}}</option>
                            </select>   
                        </th>
                        <th/><th/><th/><th/>
                    </tr>
                </thead>
                <slot></slot>
                <tbody v-if="loaded">
                    <tr class="s-pointer" v-for="(l,i) in list.elements" :key="i" v-on:click="openDetail(list.elements[i])"> 
                        <td>{{ l.brandName }}</td> 
                        <td><b>{{ l.productName }}</b> <div class="text-secondary" style="display:inline;">{{l.description}}</div></td>
                        <td>
                            <span v-for="(cat,y) in l.categories" :key="y" class="badge rounded-pill bg-primary" style="margin-right:3px;">{{ cat.categoryName }}</span>
                        </td>
                        <td class="text-center"><i class="bi bi-currency-euro"> </i> {{ CurrencyFormat(l.price) }}</td>
                        <td>
                            <div class="btn-group me-2">
                                <button type="button" class="btn btn-sm btn-outline-secondary" @click.stop="EditProduct(l.id)"><i class="bi bi-pencil-square"></i></button>
                                <button type="button" class="btn btn-sm btn-outline-danger" @click.stop="OpenModal(l.id)"><i class="bi bi-trash"></i></button>
                            </div>
                        </td> 
                    </tr>
                </tbody>
            </table>
        </div>
        <div v-if="error">
            <h2>An error has occourred!</h2>
        </div>
    </div>
</template>

<script>
    export default {

        props: {
            list: Object,
            listbrands: Array,
            loaded: Boolean,
            error: Boolean,
            desc: Boolean,
            orderbrand: Boolean,
            ordername: Boolean,
            orderprice: Boolean,
        },
        
        data() {
            return{
                key: "Tutti i brand"
            }  
        },

        methods:{
            CurrencyFormat(num){
                return num.toLocaleString("it-IT", { minimumFractionDigits: 2 });
            },
            OpenModal(id){
                this.$emit('openmodal', id)
            },
            CloseModal(){
                this.$emit('closemodal')
            },
            openDetail: function (element) {
                this.$emit('openDetail', element.id)
            },
            onChange(){
                this.$emit('brandfilter', this.key)
            },
            OrderByBrand(){
                this.$emit('orderbybrand')
            },
            OrderByName(){
                this.$emit('orderbyname')
            },
            OrderByPrice(){
                this.$emit('orderbyprice')
            },
            EditProduct(id){
                this.$emit('openedit', id)
            },
        },
    }
</script>