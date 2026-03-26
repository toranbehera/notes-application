import { createApp } from 'vue'
import { createPinia } from 'pinia'
import './style.css'
import App from './App.vue'
import { createWebHistory, createRouter } from 'vue-router'

import NotesListView from './components/NotesListView.vue'
import Note from './components/Note.vue'

const pinia = createPinia();

const routes = [
    { path: '/', component: NotesListView },
    { path: '/notes/:id', component: Note},
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})

createApp(App).use(pinia).use(router).mount('#app')
