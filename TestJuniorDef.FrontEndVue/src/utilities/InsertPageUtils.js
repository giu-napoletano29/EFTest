//utility with the common methods and data between insert views

import {Factory} from './../wrappers/Factory'
const CategoriesRepo = Factory.get('categories')
const BrandsRepo = Factory.get('brands')

export default{
    data(){
        return{
            list: [],
            brands: [],
            loadedEl: false,
            loadedBrand: false,
            loadedEditElement: false,
            error: false,
            errors: [],
            EditMode: false,
            elemid: this.$route.params.id,
            elementbyid: {},
            errmsg: [],
        }
    },
    
    computed:{
        ShowButton(){
            return this.EditMode ? false:!this.loadedEl || !this.loadedBrand
        }
    },

    methods:{
        async loadElements(){
            this.loadedEl = false
            const {data} = await CategoriesRepo.get()
            this.loadedEl = true
            this.list = data;
        },

        async loadBrands(){
            this.loadedBrand = false
            const {data} = await BrandsRepo.get()
            this.loadedBrand = true
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
                }).catch(function (response){
                    self.RedirectError(response.response.data.errors)
                });
        },
    },
}