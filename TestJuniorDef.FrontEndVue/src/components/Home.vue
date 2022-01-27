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
    </div>
</template>

<script>
    export default {
        props: {
            test: Number,
            inc: Function,
            dec: Function,
        },

        data() {
            return {
                text: ''
            }
        },

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

