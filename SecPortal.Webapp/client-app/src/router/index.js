import { createRouter, createWebHistory } from 'vue-router'
import BaseParent from '@/layouts/BaseParent.vue'
import Login from '../views/login/Main.vue'

const routes = [
  {
    path: '/',
    component: () => import('@/layouts/side-menu/Main.vue'),
    children: [
      {
        path: '',
        name: 'adminDashboard',
        component: () => import('@/views/dashboard/Main.vue')
      },
      {
        path: 'admins',
        name: 'baseAdmins',
        component: BaseParent,
        children: [
          {
            path: '',
            name: 'adminAdmins',
            component: () => import('@/views/.admin/Admins/Index.vue')
          }
        ]
      },
      {
        path: 'addresses',
        name: 'baseAdresses',
        component: BaseParent,
        children: [
          {
            path: '',
            name: 'adminAddresses',
            component: () => import('@/views/.admin/Addresses/Index.vue')
          },
          {
            path: 'crud/:id?',
            name: 'createAddresses',
            props: true,
            component: () => import('@/views/.admin/Addresses/Create.vue')
          }
        ]
      }
    ]
  },
  {
    name: 'adminLogin',
    path: '/admin/login',
    component: Login
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  scrollBehavior(to, from, savedPosition) {
    return savedPosition || { left: 0, top: 0 }
  }
})

export default router
