import { createRouter, createWebHistory } from 'vue-router'
import AdminWelcomeView from './views/AdminWelcomeView.vue'
import AdminUsersView from './views/AdminUsersView.vue'
import AdminBooksView from './views/AdminBooksView.vue'
import AdminRentsView from './views/AdminRentsView.vue'
import AdminUsersProfileView from './views/AdminUsersProfileView.vue'
import AdminPanelView from './views/AdminPanelView.vue'
import AdminPanelCategoriesView from './views/AdminPanelCategoriesView.vue'
import AdminPanelBooksView from './views/AdminPanelBooksView.vue'
import AdminPanelUsersView from './views/AdminPanelUsersView.vue'
import AdminPanelRolesView from './views/AdminPanelRolesView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: 'login',
      component: () => import('./views/LoginView.vue')
    },
    {
      path: '/panel',
      name: 'main',
      component: () => import('./views/AdminMainView.vue'),
      children: [
        {
          path: '',
          name: 'welcome',
          component: AdminWelcomeView
        
        },
        {
          path: 'users',
          name: 'users',
          component: AdminUsersView,
        },
        {
          path: 'user/:id',
          name: 'userProfile',
          component: AdminUsersProfileView
        },
        {
          path: 'books',
          name: 'books',
          component: AdminBooksView
        },
        {
            path: 'rents',
            name: 'rents',
            component: AdminRentsView
        },
        {
          path: '/admin',
          name: 'panel',
          component: AdminPanelView,
          children: [
            {
              path: '/admin/categories',
              component: AdminPanelCategoriesView
            },
            {
              path: 'books',
              component: AdminPanelBooksView
            },
            {
              path: 'users',
              component: AdminPanelUsersView
            },
            {
              path: 'roles',
              component: AdminPanelRolesView
            }
          ]
        }
      ]
    },
  ]
})

export default router