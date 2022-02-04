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
                                    <!-- <DeleteModal/> -->
                                    <button type="button" class="btn btn-danger" @click="OpenModal()">Elimina</button>
                                </div>
                            </td> 
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

        <DeleteModal
            :open="open"
            @closemodal="CloseModal"
        />

        <!-- <div class="modal fade" ref="exampleModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Sei sicuro?</h5>
                        <button type="button" class="btn-close" @click="modal.hide()" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        L'azione è irreversibile.
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="modal.hide()">Chiudi</button>
                        <button type="button" class="btn btn-danger">Elimina</button>
                    </div>
                </div>
            </div>
        </div> -->

        </b-overlay>
        <div v-if="error">
            <h2>An error has occourred!</h2>
        </div>
    </div>
</template>

<script>
//import { Modal } from 'bootstrap'
import DeleteModal from '@/components/DeleteModal.vue'

export default {
    components: {
        DeleteModal,
    },

    props: {
        page: Number,
        maxVisibleButtons: Number,
        list: Object,
        loaded: Boolean,
        error: Boolean,
    },

    data(){
        return{
            modal: null,
            open: false,
        }
    },

    methods:{
        OpenModal(){
            this.open = true
        },
        CloseModal(){
            this.open = false
        }
    },

    mounted() {
        //this.modal = new Modal(this.$refs.exampleModal)
    },
}
</script>
