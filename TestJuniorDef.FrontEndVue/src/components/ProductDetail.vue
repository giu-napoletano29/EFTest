<template>
    <div>
        <div class="row mb-5">
            <h5 class="mb-3">Categorie associate al Prodotto</h5>
            <div class="row border">
                <div v-for="(l,i) in list.categories" :key="i"  class="col-sm-4">
                    <div class="card border-0">
                        <div class="card-body">
                            {{ l.categoryName }}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div style="height: 350px;">
                <h5 class="mb-3">Leeds per prodotto</h5>
                <p class="mb-4"><b>{{ list.totalInfoRequestGuest + list.totalInfoRequestLogged }}</b> richieste informazioni ricevute per questo prodotto fra cui <b>{{list.totalInfoRequestGuest}} </b> da utenti Guest e <b>{{list.totalInfoRequestLogged}} </b> da utenti registrati</p>
                <table class="table table-success table-striped mb-3">
                    <thead class="text-center">
                        <th class="col-1 v-align"> ID </th>
                        <th class="col-6 v-align"> Nome utente</th>
                        <th class="col-2 v-align"> Num. Risposte</th>
                        <th class="col-3 v-align"> Data ultima risposta</th>
                    </thead>
                    <tbody>
                        <tr v-for="(p,i) in infoRequestPaged" :key="i"> 
                            <td class="text-center">{{ p.id }}</td>
                            <td>{{ p.name }} {{ p.lastname }}</td> 
                            <td class="text-center">{{ p.totalReply }}</td>
                            <td class="text-center">{{ FormatDate(p.dateLastReply)}}</td> 
                        </tr>
                    </tbody>
                </table>
                <h4 v-show="emptyArray" class="text-center">Non ci sono richieste!</h4>
            </div>    
            <button v-show="!emptyArray" class="btn btn-primary mb-3" @click="GoToleeds()">Vedi tutte le richieste informazioni</button>      
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

    computed:{
        emptyArray(){
            return this.infoRequestPaged.length == 0
        }
    },

    methods:{
        GoToleeds(){
            this.$emit('gotoleeds')
        },
        FormatDate(date){
            return this.$parent.FormatDate(date)
        }
    }
}
</script>
