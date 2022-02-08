<template>
    <div>
        <form v-on:submit.prevent="submitForm">
            <div class="mb-3">
                <input type="text" class="form-control" id="productname" placeholder="Nome brand">
            </div>
            <div class="mb-3">      
                <textarea class="form-control" id="descriptio" rows="3" placeholder="Descrizione"></textarea>
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

    },

    methods:{

    },
}
</script>
