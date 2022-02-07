<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div class="leeds">
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
                                    <select class="form-select" aria-label="Default select example">
                                        <option selected>Tutti i brand</option>
                                        <option value="1">One</option>
                                        <option value="2">Two</option>
                                        <option value="3">Three</option>
                                    </select>   
                                </th>
                                <th/><th/><th/><th/>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(l,i) in list.elements" :key="i"> 
                                <td>{{ l.brandName }}</td> 
                                <td>{{ l.productName }}</td>
                                <td>
                                    <span v-for="(cat,y) in l.categories" :key="y" class="badge bg-primary">{{ cat.categoryName }}</span>
                                </td>
                                <td><i class="bi bi-currency-euro"> </i> {{ l.price }}</td>
                                <td>
                                    <div class="btn-group me-2">
                                        <button type="button" class="btn btn-sm btn-outline-secondary"><i class="bi bi-pencil-square"></i></button>
                                        <button type="button" class="btn btn-sm btn-outline-secondary"><i class="bi bi-trash" style="color: red;"></i></button>
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
            loaded: Boolean,
            error: Boolean,
        },
    }
</script>