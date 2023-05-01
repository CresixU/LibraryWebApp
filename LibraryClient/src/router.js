import { createRouter, createWebHistory } from 'vue-router'
import AdminUsersView from './views/AdminUsersView.vue'
import AdminRentsView from './views/AdminRentsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'users',
      component: AdminUsersView
    },
    {
      path: '/books',
      name: 'books',
      // route level code-splitting
      // this generates a separate chunk (<name>.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('./views/AdminBooksView.vue')
    },
    {
        path: '/rents',
        name: 'rents',
        component: AdminRentsView,
    }
  ]
})

export default router