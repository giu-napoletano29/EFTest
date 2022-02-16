<template>
    <div>
        <div class="position-fixed p-3 top-0 start-50 translate-middle-x" style="z-index: 1070; display: block;" >
            <div id="liveToast" class="toast hide toast-success" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="toast-header">
                    <i class="bi bi-check-circle rounded me-2" style="color:green"></i>
                    <strong class="me-auto">Successo!</strong>
                    <small>Adesso</small>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body">
                    L'operazione è stata completata con successo!
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { Toast } from 'bootstrap'

export default {
    props: {
        open: Boolean,
    },

    data(){
        return{
            toast: null
        }
    },
    mounted() {
        var toastElList = [].slice.call(document.querySelectorAll('.toast'))
        this.toast = toastElList.map(function (toastEl) {
            return new Toast(toastEl)
        })
    },

    watch: { 
        open: function(val) {
            if(val){
                this.toast[0].show()
                this.$emit('closetoast')
            }         
        }
    }
}
</script>

<style scoped>
.toast-success{
    color: #0f5132;
    background-color: #d1e7dd !important;
    border-color: #badbcc !important;
}
</style>