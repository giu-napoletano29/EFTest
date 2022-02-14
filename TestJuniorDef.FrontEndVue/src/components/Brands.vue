<template>
    <div>
        <div class="brands">
            <div>
                <table class="table table-striped table-hover">
                    <thead>
                        <tr>
                            <th class="col-1 v-align"> ID </th>
                            <th class="col-10 v-align"> Nome brand </th><th/>
                        </tr>
                        <tr class="table-light">
                            <th/>
                            <th> 
                                <div class="col-3">
                                    <input type="text" class="form-control" placeholder="Nome brand" aria-label="Nome brand" aria-describedby="basic-addon2" v-model="search" @change="SearchName()">
                                </div>
                            </th>
                            <th/>
                        </tr>
                    </thead>
                    <slot></slot>
                    <tbody v-if="loaded">
                        <tr class="s-pointer" v-for="(l,i) in list.elements" :key="i" v-on:click="openDetail(list.elements[i])">
                            <td>{{ l.id }}</td> 
                            <td>{{ l.brandName }}</td>
                            <td>
                                <div class="btn-group me-2">
                                    <button type="button" class="btn btn-sm btn-outline-secondary" @click.stop="OpenEdit(l.id)"><i class="bi bi-pencil-square"></i></button>
                                    <button type="button" class="btn btn-sm btn-outline-secondary" @click.stop="OpenModal(l.id)"><i class="bi bi-trash" style="color: red;"></i></button>
                                </div>
                            </td> 
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
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

    data(){
        return{
            search: ""
        }
    },

    methods:{
        OpenModal(id){
            this.$emit('openmodal', id)
        },
        OpenEdit(id){
            this.$emit('openedit', id)
        },
        CloseModal(){
            this.$emit('closemodal')
        },
        openDetail: function (element) {
            this.$emit('openDetail', element.id)
        },
        SearchName(){
            this.$emit('search', this.search)
        },
    },
}
</script>
