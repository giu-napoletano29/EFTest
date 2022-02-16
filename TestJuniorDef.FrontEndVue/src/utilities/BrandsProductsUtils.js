//utility with the common methods and data between Brands and Products views

export default{
    data(){
        return{
            open: false,
            idEl: 0,
            successModalOpen: false,
            showToast: false,
            OpError: false,
            ErrMsg: {},
        }
    },

    methods:{
        OpenModal(id){
            this.idEl = id  //Element id
            this.open = true
        },

        CloseModal(){
            this.open = false
            this.successModalOpen = false //To close success modal
            this.SpecRedirect(); //Calling redirect specific for the component
        },

        OpenToast(){
            this.SpecRedirect();
            this.showToast = true;
        },

        CloseToast(){
            this.showToast = false;
        },

        //Handling of Delete request result
        DeleteSpecComponent(data){
            let self = this;
            data.then(function (response) {
                    if(response.status >= 200 && response.status <= 208){
                        self.OpError = false
                        self.successModalOpen = true
                    }else{
                        self.OpError = true
                        self.successModalOpen = true
                    }
                    self.loadElements();
                });
        },
    },
}