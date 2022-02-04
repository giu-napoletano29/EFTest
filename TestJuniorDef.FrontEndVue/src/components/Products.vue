<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div class="products">
                <div v-if="loaded">
                    <table class="table table-striped table-hover">
                        <thead>
                            <th> Brand </th>
                            <th> Prodotto </th>
                            <th> Categorie </th>
                            <th> Prezzo </th>
                        </thead>
                        <tbody>
                            <tr v-for="(l,i) in list.elements" :key="i"> 
                                <td>{{ l.brandName }}</td> 
                                <td>{{ l.productName }}</td>
                                <td>
                                    <span v-for="(cat,y) in l.categories" :key="y" class="badge bg-primary">{{ cat.categoryName }}</span>
                                </td>
                                <td>{{ l.price }}</td>
                                <td>
                                    <div class="container">
                                        <button type="button" class="btn btn-primary">Modifica</button>
                                        <button type="button" class="btn btn-danger">Elimina</button>
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
// import Table from '@/components/Table.vue'
    export default {
        // components:{
        //     Table
        // },
        props: {
            list: Object,
            loaded: Boolean,
            error: Boolean,
        },
    }
</script>