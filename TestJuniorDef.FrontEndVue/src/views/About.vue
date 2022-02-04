<template>
<div>
    <b-overlay :show="!loaded && !error" rounded="sm">
    <div class="about">
        <h1>This is an about page</h1>
        <div v-if="loaded">
            <p>{{ list }}</p>
            <p v-for="el in list.elements" :key="el.brandName"> {{ el.brandName}}</p>
            <b-table striped hover :items="list.elements" :fields="fields"></b-table>
        </div>
    </div>
    </b-overlay>
    <div v-if="error">
        <h2>An error has occourred!</h2>
    </div>
</div>
</template>

<script>
    //import BrandsAPI from '../wrappers/BrandWrapper.js';
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')
    export default {
        
        data() {
            return {
                name: 'About',
                list: null,
                loaded: false,
                error: false,
                fields: ['brandName', 'description']
            }
        },

        async created() {
            const {data} = await BrandsRepo.getallpaged(5, 2)
            this.loaded = true
            this.list = data;
        }
    }

</script>
