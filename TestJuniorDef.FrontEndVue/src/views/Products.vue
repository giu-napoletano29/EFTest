<template>
    <div>
        <b-container>
            <Header :name="name">
                <div class="btn-toolbar mb-2 mb-md-0">
                    <div class="btn-group me-2">
                        <button type="button" class="btn btn-sm btn-outline-secondary">Aggiungi prodotto</button>
                    </div>
                </div>
            </Header>
            <Products/>
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Products from '@/components/Products.vue'
    export default {
        
        components: {
            Products,
            Header
        },

        data(){
            return{
                name: 'Prodotti',
            }
        }
    }
</script>