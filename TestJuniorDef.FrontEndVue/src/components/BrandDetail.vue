<template>
    <div>
        <div class="row">
            <h5>Categorie associate ai Prodotti di {{ list.brandName }}</h5>
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
            <h5>Richieste informazioni per prodotto</h5>
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
