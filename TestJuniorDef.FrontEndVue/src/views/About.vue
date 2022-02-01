<template>
<div>
    <b-overlay :show="!loaded && !error" rounded="sm">
    <div class="about">
        <h1>This is an about page</h1>
        <div v-if="loaded">
            <p>{{ list }}</p>
            <p v-for="el in list.elements" :key="el.brandName"> {{ el.brandName}}</p>
        </div>
    </div>
    </b-overlay>
    <div v-if="error">
        <h2>An error has occourred!</h2>
    </div>
</div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                list: null,
                loaded: false,
                error: false,
            }
        },

        async created() {
            await axios.get('https://localhost:5001/Brand/page/5/2').then(response => (this.list = response.data))
            .catch((error) => { console.warn('Not good man :( ' + error); this.error = true })
            this.loaded = true

        }
    }

</script>
