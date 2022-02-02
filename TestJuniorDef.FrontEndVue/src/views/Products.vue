<template>
    <div>
        <b-container>
            <Header />
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
    }
</script>