<template>
    <div>
        <form v-on:submit.prevent="submitForm">
            <div class="mb-3">
                <input type="text" class="form-control" id="productname" placeholder="Nome prodotto">
            </div>
            <select class="form-select mb-3" aria-label="Default select example">
                <option selected>Seleziona brand</option>
                <option v-for="(l,i) in brands" :key="i" :value="l.id">{{l.brandName}}</option>
            </select>
            <div class="mb-3">
                <input type="text" class="form-control" id="shortdescriptio" placeholder="Piccola descrizione">
            </div>
            <div class="mb-3">      
                <textarea class="form-control" id="descriptio" rows="3" placeholder="Descrizione"></textarea>
            </div>
            <div class="mb-3 form-check form-check-inline" v-for="(l,i) in list" :key="i">
                <input type="checkbox" class="form-check-input" :id="l.id">
                <label class="form-check-label" for="exampleCheck1">{{l.name}}</label>
            </div>
            <div class="mb-3">      
                <button type="submit" class="btn btn-primary">Submit</button>
            </div>
            
        </form>
    </div>
</template>

<script>

export default {

    props: {
        list: Array,
        brands: Array,
        loaded: Boolean,
        error: Boolean,

        productName: String,
        brandId: Number,
        shortDesc: String,
        longDesc: String,
        productCats: [],
    },

    methods:{
        submitForm(){
            // const product = { 
            //     title: "Vue POST Request Example" 
            // };
        }  
    },
}
</script>
