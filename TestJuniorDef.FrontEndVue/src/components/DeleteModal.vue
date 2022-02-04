<template>
    <div class="modal fade" ref="exampleModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Sei sicuro?</h5>
                    <button type="button" class="btn-close" @click="CloseModal()" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    L'azione è irreversibile.
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="CloseModal()">Chiudi</button>
                    <button type="button" class="btn btn-danger">Elimina</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { Modal } from 'bootstrap'

export default {
    props: {
        open: Boolean
    },

    data(){
        return{
            modal: null
        }
    },
    mounted() {
        this.modal = new Modal(this.$refs.exampleModal)
    },

    methods:{
        CloseModal(){
            this.modal.hide()
            this.$emit('closemodal')
        },
    },

    watch: { 
        open: function(val) {
            if(val){
                this.modal.show()
            }         
        }
}
}
</script>