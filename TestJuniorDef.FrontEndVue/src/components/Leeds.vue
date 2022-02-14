<template>
    <div>
        <div class="leeds">
            <div>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th scope="col" class="col-1 v-align"> Id </th>
                            <th scope="col" class="col-1 v-align s-pointer" @click="OrderBy()"> 
                                <div class="row">
                                    <div class="col-2">
                                        Data
                                    </div> 
                                    <div class="text-right col">
                                        <div v-show="!this.orderDesc">
                                            <!-- <i class="fa fa-fw fa-sort-desc"></i> -->
                                                <i class="fa fa-fw fa-sort-desc icon-stack"></i>
                                                <i class="fa fa-fw fa-sort-asc"></i>
                                        </div>                              
                                        <div v-show="this.orderDesc">
                                            <i class="fa fa-fw fa-sort-asc icon-stack"></i>
                                            <i class="fa fa-fw fa-sort-desc"></i>
                                        </div>
                                    </div>
                                </div>
                            </th>
                            <th scope="col" class="col-2 v-align"> Nome </th>
                            <th scope="col" class="col-2 v-align"> Cognome </th>
                            <th scope="col" class="col-3 v-align"> Prodotto </th>
                            <th scope="col" class="col-3 v-align"> Brand </th>
                        </tr>
                        <tr class="table-light">
                            <th/><th/><th/><th/>
                            <th> 
                                <div class="input-group">
                                    <input type="text" class="form-control" placeholder="Nome prodotto" aria-label="Nome prodotto" aria-describedby="basic-addon2" v-model="search" @change="SearchName()">
                                </div>
                            </th>
                            <th> 
                                <select class="form-select" aria-label="Default select example" v-model="key" @change="onChange()">
                                    <option value="">Tutti i brand</option>
                                    <option v-for="(l,i) in listbrands" :key="i" :value="l.id">{{l.brandName}}</option>
                                </select>    
                            </th>
                        </tr>
                    </thead>
                    <slot></slot>
                    <tbody v-if="loaded">
                        <tr class="s-pointer" v-for="(l,i) in list.elements" :key="i" v-on:click="openDetail(list.elements[i])"> 
                            <td>{{ l.id }}</td> 
                            <td>{{ l.insertDate.substring(0, 10) }} </td>
                            <td>{{ l.name }}</td>
                            <td>{{ l.lastname }}</td>
                            <td>{{ l.productName }}</td>
                            <td>{{ l.brandName }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
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
            params: Object,
            orderDesc: Boolean,
        },

        data() {
            return{
                key: this.params.brand ? this.params.brand:"",
                search: this.params.search
            }  
        },

        methods:{
            openDetail: function (element) {
                this.$emit('openDetail', element.id)
            },
            onChange(){
                this.$emit('brandfilter', this.key)
            },
            SearchName(){
                this.$emit('search', this.search)
            },
            OrderBy(){
                this.$emit('orderby')
            },
        }

    }
</script>