<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
        <div class="brand">
            <div v-if="loaded">
                <table class="table table-striped table-hover">
                    <thead>
                        <th> ID </th>
                        <th> Nome brand </th>
                    </thead>
                    <tbody>
                        <tr v-for="(l,i) in list.elements" :key="i"> 
                            <td>{{ l.id }}</td> 
                            <td>{{ l.brandName }}</td>
                            <td>
                                <div class="container">
                                    <button type="button" class="btn btn-primary">Modifica</button>
                                    <button type="button" class="btn btn-danger" @click="OpenModal()">Elimina</button>
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
        page: Number,
        maxVisibleButtons: Number,
        list: Object,
        loaded: Boolean,
        error: Boolean,
    },

    methods:{
        OpenModal(){
            this.$emit('openmodal')
        },
        CloseModal(){
            this.$emit('closemodal')
        }
    },
}
</script>
