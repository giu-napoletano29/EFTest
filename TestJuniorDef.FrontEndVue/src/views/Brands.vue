<template>
    <div>
        <b-container>
            <Header />
            <Brands/>
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Brands from '@/components/Brands.vue'
    export default {
        components: {
            Brands,
            Header
        },
    }
</script>