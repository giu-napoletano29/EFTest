<template>
    <div>
        <div class="row mb-5">
            <h5 class="mb-3">Categorie associate al Prodotto</h5>
            <div class="row">
                <div v-for="(l,i) in list.categories" :key="i"  class="col-sm-4">
                    <div class="card">
                        <div class="card-body">
                            {{ l.categoryName }}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <h5 class="mb-3">Leeds per prodotto</h5>
            <p class="mb-4"><b>{{ list.totalInfoRequest }}</b> richieste informazioni ricevute per questo prodotto fra cui <b>{{list.totalInfoRequestGuest}} </b> da utenti Guest e <b>{{list.totalInfoRequestLogged}} </b> da utenti registrati</p>
            <table class="table table-success table-striped mb-3">
                <thead>
                    <th> ID </th>
                    <th> Nome utente</th>
                    <th> Num. Risposte</th>
                    <th> Data ultima risposta</th>
                </thead>
                <tbody>
                    <tr v-for="(p,i) in infoRequestPaged" :key="i"> 
                        <td>{{ p.id }}</td>
                        <td>{{ p.name }} {{ p.lastname }}</td> 
                        <td>{{ p.totalReply }}</td>
                        <td>{{ p.dateLastReply }}</td> 
                    </tr>
                </tbody>
            </table> 
            <button class="btn btn-primary mb-3" @click="GoToleeds()">Vedi tutte le richieste informazioni</button>      
        </div>
    </div>
</template>

<script>
export default {

    props: {
        list: Object,
        infoRequestPaged: Array,
        loaded: Boolean,
        error: Boolean,
    },

    methods:{
        GoToleeds(){
            this.$emit('gotoleeds')
        }
    }
}
</script>
