<template>
    <div>
        <div class="mb-3">
            <input type="text" class="form-control" placeholder="Nome brand" v-model="brand.BrandName" required>
        </div>
        <div class="input-group mb-3">
            <span class="input-group-text" id="basic-addon1">@</span>
            <input type="text" class="form-control" placeholder="Email" aria-label="Email" aria-describedby="basic-addon1" v-model="brand.Account.Email" required>
        </div>
        <div class="mb-3">
            <input type="password" class="form-control" placeholder="Password" v-model="brand.Account.Password" required>
        </div>
        <div class="mb-3">      
            <textarea class="form-control" rows="3" placeholder="Descrizione" v-model="brand.Description" required></textarea>
        </div>
        <div v-if="!EditMode" class="mb-3"> 
            <p><button type="button" class="btn btn-primary" @click="addProd">Aggiungi prodotto</button></p>
        </div>
    </div>
</template>

<script>

export default {

    props: {
        EditMode: Boolean,
        elementbyid: Object,
        loaded: Boolean,
    },

    data(){
        return{
            brand:{ 
                BrandName: "",
                Description: "",
                Account:{
                    Email: "",
                    Password: "",
                    AccountType: 1,
                }
            }
        }
    },

    watch: {
        brand: {
            deep:true,
            handler(){
                this.$emit('input', this.brand);
            }
        },
        loaded:{
            handler: function(newValue){
                if(this.EditMode && newValue){
                    this.AssingOldValue()
                }
            }
        }
    },

    methods:{
        addProd(){
            this.$emit('addprod');
        },
        AssingOldValue(){
            this.brand.BrandName = this.elementbyid.brandName;
            this.brand.Description = this.elementbyid.description;
            this.brand.Account.Email = this.elementbyid.email;
            this.brand.Account.Password =this.elementbyid.password;
        }
    },
}
</script>
