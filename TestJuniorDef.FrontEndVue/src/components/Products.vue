<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div class="products">
                <div v-if="loaded" style="min-height: 780px;">
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th scope="col" class="col-3" @click="OrderByBrand()" > Brand 
                                    <div class="btn btn-sm">
                                        <div v-show="!this.orderbrand">
                                            <i class="fa fa-fw fa-sort"></i>
                                        </div>                              
                                        <div v-show="this.orderbrand">
                                            <i v-show="!this.desc" class="fa fa-fw fa-sort-asc"></i>
                                            <i v-show="this.desc" class="fa fa-fw fa-sort-desc"></i>
                                        </div>
                                    </div>
                                </th>
                                <th scope="col" class="col-3" @click="OrderByName()"> Prodotto 
                                    <div class="btn btn-sm">
                                        <div v-show="!this.ordername">
                                            <i class="fa fa-fw fa-sort"></i>
                                        </div>                              
                                        <div v-show="this.ordername">
                                            <i v-show="!this.desc" class="fa fa-fw fa-sort-asc"></i>
                                            <i v-show="this.desc" class="fa fa-fw fa-sort-desc"></i>
                                        </div>
                                    </div>
                                </th>
                                <th scope="col" class="col-4 v-align"> Categorie </th>
                                <th scope="col" class="col-1" @click="OrderByPrice()"> Prezzo 
                                    <div class="btn btn-sm">
                                        <div v-show="!this.orderprice">
                                            <i class="fa fa-fw fa-sort"></i>
                                        </div>                              
                                        <div v-show="this.orderprice">
                                            <i v-show="!this.desc" class="fa fa-fw fa-sort-asc"></i>
                                            <i v-show="this.desc" class="fa fa-fw fa-sort-desc"></i>
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
                        <tbody>
                            <tr v-for="(l,i) in list.elements" :key="i" v-on:click="openDetail(list.elements[i])"> 
                                <td>{{ l.brandName }}</td> 
                                <td>{{ l.productName }}</td>
                                <td>
                                    <span v-for="(cat,y) in l.categories" :key="y" class="badge bg-primary">{{ cat.categoryName }}</span>
                                </td>
                                <td><i class="bi bi-currency-euro"> </i> {{ l.price }}</td>
                                <td>
                                    <div class="btn-group me-2">
                                        <button type="button" class="btn btn-sm btn-outline-secondary" @click.stop="EditProduct(l.id)"><i class="bi bi-pencil-square"></i></button>
                                        <button type="button" class="btn btn-sm btn-outline-secondary" @click.stop="OpenModal(l.id)"><i class="bi bi-trash" style="color: red;"></i></button>
                                    </div>
                                </td> 
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </b-overlay>
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