import {Factory} from './../wrappers/Factory'
const BrandsRepo = Factory.get('brands')

export default{
    data(){
        return{
            listbrands: [],
        }
    },

    methods:{
        async loadBrands(){
            const {data} = await BrandsRepo.get()
            this.listbrands = data;
        },

        BrandFilter(filter){
            if(/^\d+$/.test(filter)){
                this.params.brand = filter
            }else{
                delete this.params.brand
            }
            this.loadElements();
        },
    },
}