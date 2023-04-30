import { createRouter, createWebHistory } from 'vue-router'
import UsersView from './views/UsersView.vue'
import RentsView from './views/RentsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'users',
      component: UsersView
    },
    {
      path: '/books',
      name: 'books',
      // route level code-splitting
      // this generates a separate chunk (<name>.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('./views/BooksView.vue')
    },
    {
        path: '/rents',
        name: 'rents',
        component: RentsView,
    }
  ]
})

export default router