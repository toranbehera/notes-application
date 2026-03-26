import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import { createWebHistory, createRouter } from 'vue-router'

import NotesListView from './components/NotesListView.vue'
import Note from './components/Note.vue'

const routes = [
    { path: '/', component: NotesListView },
    { path: '/notes/:id', component: Note},
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})

createApp(App).use(router).mount('#app')
