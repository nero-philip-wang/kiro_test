import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/',
    redirect: '/employee'
  },
  {
    path: '/employee',
    name: 'Employee',
    component: () => import('../views/employee/Layout.vue'),
    children: [
      {
        path: '',
        name: 'EmployeeHome',
        component: () => import('../views/employee/Home.vue')
      },
      {
        path: 'products',
        name: 'EmployeeProducts',
        component: () => import('../views/employee/Products.vue')
      },
      {
        path: 'cart',
        name: 'EmployeeCart',
        component: () => import('../views/employee/Cart.vue')
      },
      {
        path: 'orders',
        name: 'EmployeeOrders',
        component: () => import('../views/employee/Orders.vue')
      },
      {
        path: 'profile',
        name: 'EmployeeProfile',
        component: () => import('../views/employee/Profile.vue')
      }
    ]
  },
  {
    path: '/admin',
    name: 'Admin',
    component: () => import('../views/admin/Layout.vue'),
    children: [
      {
        path: '',
        name: 'AdminDashboard',
        component: () => import('../views/admin/Dashboard.vue')
      },
      {
        path: 'products',
        name: 'AdminProducts',
        component: () => import('../views/admin/Products.vue')
      },
      {
        path: 'orders',
        name: 'AdminOrders',
        component: () => import('../views/admin/Orders.vue')
      },
      {
        path: 'categories',
        name: 'AdminCategories',
        component: () => import('../views/admin/Categories.vue')
      },
      {
        path: 'employees',
        name: 'AdminEmployees',
        component: () => import('../views/admin/Employees.vue')
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router