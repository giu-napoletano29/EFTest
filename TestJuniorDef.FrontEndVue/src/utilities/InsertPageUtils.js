import {Factory} from './../wrappers/Factory'
const CategoriesRepo = Factory.get('categories')
const BrandsRepo = Factory.get('brands')

export default{
    data(){
        return{
            list: [],
            brands: [],
            loaded: false,
            error: false,
            errors: [],
            EditMode: false,
            elemid: this.$route.params.id,
            elementbyid: {},
        }
    },

    methods:{
        async loadElements(){
            const {data} = await CategoriesRepo.get()
            this.loaded = true
            this.list = data;
        },

        async loadBrands(){
            const {data} = await BrandsRepo.get()
            this.loaded = true
            this.brands = data;
        },

        ResponseHandler(resp){
            let self = this;
            resp.then(function (response) {
                    if(response.status >= 200 && response.status <= 208){
                        self.RedirectSuccess()
                    }else{
                        self.RedirectError()
                    }
                });
        },
    },
}