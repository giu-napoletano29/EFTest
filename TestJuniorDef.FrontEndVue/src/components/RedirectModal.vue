<template>
    <div class="modal fade" ref="redirectModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 v-if="!OpError" class="modal-title" id="exampleModalLabel">Operazione completata! <i class="bi bi-check-circle" style="color:green"></i></h5>
                    <h5 v-else class="modal-title" id="exampleModalLabel">Errore! <i class="bi bi-exclamation-circle" style="color:red"></i></h5>  
                    <button type="button" class="btn-close" @click="CloseModal()" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p v-if="!OpError" >L'azione è stata compiuta correttamente!</p>
                    <div v-else>
                        Si è verificato un errore nel completare l'operazione! 
                        <div v-for="(l,i) in err" :key="i">{{l}}</div>
                        </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn" v-bind:class="getClass()" @click="CloseModal()">Chiudi</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { Modal } from 'bootstrap'

export default {
    props: {
        success: Boolean,
        OpError: Boolean,
        ErrMsg: Object,
    },

    data(){
        return{
            modal: null,
            err: []
        }
    },
    mounted() {
        this.modal = new Modal(this.$refs.redirectModal)
    },

    methods:{
        CloseModal(){
            this.modal.hide()
            this.$emit('closemodal')
        },
        getClass(){
            return {
                'btn-success': !this.OpError,  
                'btn-danger': this.OpError
                }
        },
        printValues(obj) {
            for (var key in obj) {
                if (typeof obj[key] === "object") {
                    this.printValues(obj[key]);   
                } else {
                    this.err.push(obj[key])
                }
            }
        }
    },

    watch: { 
        success: function(val) {
            if(val){
                this.ErrMsg = []
                this.printValues(this.ErrMsg)
                this.modal.show()
            }         
        }
}
}
</script>