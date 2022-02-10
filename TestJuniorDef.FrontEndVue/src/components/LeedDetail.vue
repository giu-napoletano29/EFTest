<template>
    <div>
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-left pt-3 pb-2 mb-3">
            <h5 v-if="list.product" class="h5">Richiesta informazioni di {{ list.name }} {{ list.lastName }} per il prodotto {{ list.product.productName }} di {{ list.product.brandName }}</h5>
        </div>
        <b>Dati del richiedente: </b>
        <p>{{ list.name }} {{ list.lastName }},</p>
        <p>{{ list.address }}</p>
        <b>Richiesta inviata dall'utente: </b>
        <p>{{ list.text }}</p>
        <b>Rispsote/Commenti alla richiesta</b>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div v-for="(l,i) in replies" :key="i" class="card" style="width: 18rem;">
                <div class="card-header">
                    {{l.date}} - {{list.product.brandName}}
                </div>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">{{l.text}}</li>
                </ul>
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
        replies: Array,
        loaded: Boolean,
        error: Boolean,
    },
}
</script>