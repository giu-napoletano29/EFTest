import Vue from 'vue'
import VueRouter from 'vue-router'
import Brands from '../views/Brands.vue'
import BrandDetail from '../views/BrandDetail.vue'
import Products from '../views/Products.vue'
import ProductDetail from '../views/ProductDetail.vue'
import Leeds from '../views/Leeds.vue'
import LeedDetail from '../views/LeedDetail.vue'
import InsertNewbrand from '../views/InsertNewbrand.vue'
import InsertNewProduct from '../views/InsertNewProduct.vue'
import NotFound from '../views/Service/NotFound.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    redirect: '/brands'
  },
  {
    path: '*',
    name: 'NotFound',
    component: NotFound
  },
  // {
  //   path: '/about',
  //   name: 'About',

  //   // is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // },
  {
    path: '/brands',
    name: 'Brands',
    component: Brands
  },
  {
    path: '/brands/success',
    name: 'BrandsSuccess',
    component: Brands
  },
  {
    path: '/brands/error',
    name: 'BrandsError',
    component: Brands
  },
  {
    path: '/brands/:id(\\d+)',
    name: 'BrandDetail',
    component: BrandDetail
  },
  {
    path: '/brands/new/',
    name: 'InsertNewbrand',
    component: InsertNewbrand
  },
  {
    path: '/brands/edit/:id(\\d+)',
    name: 'Editbrand',
    component: InsertNewbrand
  },
  {
    path: '/leeds/:id(\\d+)',
    name: 'LeedDetail',
    component: LeedDetail
  },
  {
    path: '/products',
    name: 'Products',
    component: Products
  },
  {
    path: '/products/success',
    name: 'ProductsSuccess',
    component: Products
  },
  {
    path: '/products/error',
    name: 'ProductsError',
    component: Products
  },
  {
    path: '/products/:id(\\d+)',
    name: 'ProductDetail',
    component: ProductDetail
  },
  {
    path: '/products/new',
    name: 'InsertNewProduct',
    component: InsertNewProduct
  },
  {
    path: '/products/edit/:id(\\d+)',
    name: 'EditProduct',
    component: InsertNewProduct
  },
  {
    path: '/leeds',
    name: 'Leeds',
    component: Leeds
  },
]

const router = new VueRouter({
  routes
})

export default router
