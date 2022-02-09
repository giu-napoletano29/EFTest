<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div class="products">
                <div v-if="loaded">
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th scope="col"> Brand </th>
                                <th scope="col"> Prodotto </th>
                                <th scope="col"> Categorie </th>
                                <th scope="col"> Prezzo </th>
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
                                        <button type="button" class="btn btn-sm btn-outline-secondary"><i class="bi bi-pencil-square"></i></button>
                                        <button type="button" class="btn btn-sm btn-outline-secondary" @click.stop="OpenModal()"><i class="bi bi-trash" style="color: red;"></i></button>
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
        },
        
        data() {
            return{
                key: "Tutti i brand"
            }  
        },

        methods:{
            OpenModal(){
                this.$emit('openmodal')
            },
            CloseModal(){
                this.$emit('closemodal')
            },
            openDetail: function (element) {
                this.$emit('openDetail', element.id)
            },
            onChange(){
                this.$emit('brandfilter', this.key)
            }
        },
    }
</script>