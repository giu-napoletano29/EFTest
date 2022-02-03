<template>
    <div>
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 class="h2">{{ name }}</h1>
            <slot></slot>
        </div>
        <span class="badge bg-primary">Primary</span>
    </div>
</template>

<script>
/*    import axios from 'axios';*/
    export default {
        props: {
            name: String
        },
    }
</script>