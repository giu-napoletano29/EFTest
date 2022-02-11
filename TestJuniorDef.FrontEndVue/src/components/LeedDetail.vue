<template>
    <div>
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-left pt-3 pb-2 mb-3">
            <h5 v-if="list.product" class="h5">Richiesta informazioni di {{ list.name }} {{ list.lastname }} per il prodotto {{ list.product.productName }} di {{ list.product.brandName }}</h5>
        </div>
        <p><b>Dati del richiedente: </b></p>
        <p>{{ list.name }} {{ list.lastName }}, <br> {{ list.address }}</p>
        <p><b>Richiesta inviata dall'utente: </b></p>
        <p>{{ list.text }}</p>
        <p><b>Rispsote/Commenti alla richiesta</b></p>
        <b-overlay :show="!loaded && !error" rounded="sm">
            <div style="min-height: 250px;">
                <div v-for="(l,i) in replies" :key="i" class="card border-success mb-3" style="width: 100%;">
                    <div class="card-header">
                        {{l.date.substring(0, 10)}} - {{l.user}}
                    </div>
                    <div class="card-body text-success">
                        <p class="card-text">{{l.text}}</p>
                    </div>
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
        replies: Array,
        loaded: Boolean,
        error: Boolean,
    },
}
</script>