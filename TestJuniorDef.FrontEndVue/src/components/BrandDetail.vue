<template>
    <div>
        <div class="row mb-5">
            <h5 class="mb-3">Categorie associate ai Prodotti di {{ list.brandName }}</h5>
            <div class="row">
                <div v-for="(l,i) in list.categories" :key="i"  class="col-sm-4">
                    <div class="card">
                        <div class="card-body">
                            {{ l.categoryName }} ({{ l.totalProducts }})
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <h5 class="mb-3">Richieste informazioni per prodotto</h5>
            <p class="mb-4"><b>{{ list.totalInfoRequest }}</b> richieste informazioni raccolte su totale di <b>{{ list.totalProducts }}</b> prodotti</p>
            <table class="table table-success table-striped">
                <thead>
                    <th> ID </th>
                    <th> Nome prodotto</th>
                    <th> Num. Richieste informazioni</th>
                </thead>
                <tbody>
                    <tr v-for="(p,i) in productsPaged" :key="i"> 
                        <td>{{ p.id }}</td>
                        <td>{{ p.productName }}</td> 
                        <td>{{ p.totalInfoRequest }}</td> 
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
export default {

    props: {
        list: Object,
        productsPaged: Array,
        loaded: Boolean,
        error: Boolean,
    },
}
</script>
