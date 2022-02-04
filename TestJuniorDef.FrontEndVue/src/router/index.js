import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Brands from '../views/Brands.vue'
import BrandDetail from '../views/BrandDetail.vue'
import Products from '../views/Products.vue'
import Leeds from '../views/Leeds.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/about',
    name: 'About',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  },
  {
    path: '/brands',
    name: 'Brands',
    component: Brands
  },
  {
    path: '/brands/:id',
    name: 'BrandDetail',
    component: BrandDetail
  },
  {
    path: '/products',
    name: 'Products',
    component: Products
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
