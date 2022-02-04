<template>
    <div>
        <b-container>
            <Header :name="name"/>
            <BrandDetail
                :list="list"
                :error="error"
                :loaded="loaded"
            />
        </b-container>
    </div>
</template>

<script>
    import Header from '@/components/Header.vue'
    import BrandDetail from '@/components/BrandDetail.vue'
    import {Factory} from './../wrappers/Factory'
    const BrandsRepo = Factory.get('brands')

    export default {
        
        components: {
            Header,
            BrandDetail,
        },

        data(){
            return{
                //name: 'Brand ' + this.$route.params.id,
                list: {},
                loaded: false,
                error: false,
            }
        },

        methods: {
            async loadElements(){
                const {data} = await BrandsRepo.getById(this.$route.params.id)
                this.loaded = true
                this.list = data;
            },
        },

        computed:{
            name(){
                return 'Brand ' + this.list.brandName
            },
        },

        created() {
            this.loadElements();
        }
    }
</script>