<template>
    <div>
        <div class="mb-3">
            <input type="text" class="form-control" placeholder="Nome brand" v-model="brand.BrandName">
        </div>
        <div class="input-group mb-3">
            <span class="input-group-text" id="basic-addon1">@</span>
            <input type="text" class="form-control" placeholder="Email" aria-label="Email" aria-describedby="basic-addon1" v-model="brand.Account.Email">
        </div>
        <div class="mb-3">
            <input type="password" class="form-control" placeholder="Password" v-model="brand.Account.Password">
        </div>
        <div class="mb-3">      
            <textarea class="form-control" rows="3" placeholder="Descrizione" v-model="brand.Description"></textarea>
        </div>
        <div class="mb-3"> 
            <button type="button" class="btn btn-primary" @click="addProd">Aggiungi prodotto</button>
        </div>
    </div>
</template>

<script>

export default {

    props: {
        brand:{ 
            BrandName: String,
            Description: String,
            Account:{
                Email: String,
                Password: String,
            }
        }

    },

    watch: {
        brand: {
            deep:true,
            handler(){
                console.log("Emit")
                this.$emit('input', this.brand);
            }
        }
    },

    methods:{
        addProd(){
            this.$emit('addprod');
        }
    },
}
</script>
