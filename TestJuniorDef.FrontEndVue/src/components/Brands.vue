<template>
    <div>
        <b-overlay :show="!loaded && !error" rounded="sm">
        <div class="brands">
            <div v-if="loaded">
                <table class="table table-striped table-hover">
                    <thead>
                        <th> ID </th>
                        <th> Nome brand </th>
                    </thead>
                    <tbody>
                        <tr v-for="(l,i) in list.elements" :key="i" v-on:click="openDetail(list.elements[i])"> 
                            <td>{{ l.id }}</td> 
                            <td>{{ l.brandName }}</td>
                            <td>
                                <div class="btn-group me-2">
                                    <button type="button" class="btn btn-sm btn-outline-secondary"><i class="bi bi-pencil-square"></i></button>
                                    <button type="button" class="btn btn-sm btn-outline-secondary" @click="OpenModal()"><i class="bi bi-trash" style="color: red;"></i></button>
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

    methods:{
        OpenModal(){
            this.$emit('openmodal')
        },
        CloseModal(){
            this.$emit('closemodal')
        },
        openDetail: function (element) {
            this.$emit('openDetail', element.id)
        },
    },
}
</script>
