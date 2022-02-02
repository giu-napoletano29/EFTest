<template>
    <div>
        <b-container>
            <Header />
            <Leeds/>
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import Leeds from '@/components/Leeds.vue'
    export default {
        components: {
            Leeds,
            Header
        },
    }
</script>