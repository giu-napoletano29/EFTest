<template>
    <div>
        <div class="mb-4">
            <b-avatar></b-avatar>
            <b-avatar variant="primary" text="BV"></b-avatar>
            <b-avatar variant="info" src="https://placekitten.com/300/300"></b-avatar>
            <b-avatar variant="success" icon="people-fill"></b-avatar>
        </div>
        <div>
            <b-button @click="tt">Button</b-button>
            <b-button @click="dec" variant="danger">Dec</b-button>
            <b-button @click="inc" variant="success">inc</b-button>
            <b-button @click="set" variant="outline-primary">Button</b-button>
        </div>
        <h2>Numero di prova: {{ test }}</h2>
        <div>
            <b-form-input v-model="text" placeholder="Enter your name"></b-form-input>
            <div class="mt-2">Value: {{ text }}</div>
        </div>
        <div class="overflow-auto">
            <!--<b-pagination-nav :link-gen="linkGen" :number-of-pages="3" use-router align="center"></b-pagination-nav>-->
        </div>
        <p> Test API: {{ axi.brandName }}</p>
    </div>
</template>

<script>
/*    import axios from 'axios';*/
    export default {
        props: {
            test: Number,
            inc: Function,
            dec: Function,
            axi: String
        },

        data() {
            return {
                text: '',
                brands: [],
            }
        },

        //created() {
        //    try {
        //        axios.get('https://localhost:5001/Brand/6').then(response => (this.brand = response.data))
        //    } catch (e) {
        //        /*Console.log(e)*/
        //    }
        //},

        methods: {
            set() {
                this.test = this.text
            },
            tt() {
                this.$emit('val', parseInt(this.text))
            }
        }
    }

</script>

