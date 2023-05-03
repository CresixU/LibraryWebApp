import { createRouter, createWebHistory } from 'vue-router'
import AdminUsersView from './views/AdminUsersView.vue'
import AdminBooksView from './views/AdminBooksView.vue'
import AdminRentsView from './views/AdminRentsView.vue'
import AdminUsersProfileView from './views/AdminUsersProfileView.vue'
import AdminPanelView from './views/AdminPanelView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'main',
      // route level code-splitting
      // this generates a separate chunk (<name>.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('./views/AdminMainView.vue')
    },
    {
      path: '/users',
      name: 'users',
      component: AdminUsersView,
    },
    {
      path: '/user/:id',
      name: 'userProfile',
      component: AdminUsersProfileView
    },
    {
      path: '/books',
      name: 'books',
      component: AdminBooksView
    },
    {
        path: '/rents',
        name: 'rents',
        component: AdminRentsView
    },
    {
      path: '/panel',
      name: 'panel',
      component: AdminPanelView
    }
  ]
})

export default router