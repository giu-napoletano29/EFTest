<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div class="leeds">
                <div v-if="loaded">
                    <table class="table table-striped table-hover">
                        <thead>
                            <tr>
                                <th scope="col"> Id </th>
                                <th scope="col"> Nome </th>
                                <th scope="col"> Cognome </th>
                                <th scope="col"> Prodotto </th>
                                <th scope="col"> Brand </th>
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
                                <th> 
                                    <div class="input-group">
                                        <input type="text" class="form-control" placeholder="Nome prodotto" aria-label="Nome prodotto" aria-describedby="basic-addon2">
                                    </div>
                                </th>
                                <th/><th/><th/><th/>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(l,i) in list" :key="i"> 
                                <td>{{ l.id }}</td> 
                                <td>{{ l.name }}</td>
                                <td>{{ l.lastName }}</td>
                                <td>{{ l.product.name }}</td>
                                <td>{{ l.product.brandId }}</td>
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
            list: Array,
            loaded: Boolean,
            error: Boolean,
        },
    }
</script>