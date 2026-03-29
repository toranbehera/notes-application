<script setup lang="ts">
import { computed, ref, onMounted } from 'vue'
import NoteCard from './NoteCard.vue';
import { useThemeStore } from '../store';
import axios from 'axios';

type Note = {
  id: number,
  title: string,
  content?: string,
  createdAt: string, 
  updatedAt?: string
}

const url = 'https://notes-application-production.up.railway.app/notes';

const list = ref<Note[]>([]);

const loadingNotes = ref(true);
const isSubmitting = ref(false);

async function fetchNotes(){
  try{
    const res = await axios.get(url);
    list.value = res.data;
  } catch(err){
    console.error(err);
  } finally {
    loadingNotes.value = false;
  }
}

const theme = useThemeStore();

const noteTitle = ref('');
const noteContent = ref('');

const searchQuery = ref('');
const sortOrder = ref('asc');

const filteredList = computed(() => {
  const query = searchQuery.value.toLowerCase()

  return list.value
    .filter(note => note.title.toLowerCase().includes(query))
    .sort((a, b) => {
      const dateA = new Date(a.updatedAt || a.createdAt).getTime();
      const dateB = new Date(b.updatedAt || b.createdAt).getTime();

      return sortOrder.value === 'asc'? dateB - dateA : dateA - dateB;
    })
})

async function addNote(){
  if(!noteTitle.value.trim()) return;
  
  isSubmitting.value = true; 

  try{
  const note = {
    title: noteTitle.value,
    content: noteContent.value
  }

  await axios.post(url, note);

  noteTitle.value = '';
  noteContent.value = '';

  await fetchNotes();
  } catch(err) {
  console.error(err);
  } finally{
    isSubmitting.value = false; 
  }
}

function toggleSorting() {
  sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc'
}

onMounted(() => {
  fetchNotes();
})
</script>

<template>
  <!-- title and color theme icon -->
  <div class="mx-auto min-h-screen flex flex-col bg-white dark:bg-gray-800">
    <div class="bg-white flex flex-col gap-5 sticky top-0 w-full py-2 dark:bg-gray-800 dark:text-white">
      <div class="relative flex items-center justify-center">
        <h1 class="text-xl font-bold">Lotion</h1>
        <button class="absolute right-0" @click="theme.toggleTheme">
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-moon-fill" viewBox="0 0 16 16">
            <path v-if="theme.isDark" d="M6 .278a.77.77 0 0 1 .08.858 7.2 7.2 0 0 0-.878 3.46c0 4.021 3.278 7.277 7.318 7.277q.792-.001 1.533-.16a.79.79 0 0 1 .81.316.73.73 0 0 1-.031.893A8.35 8.35 0 0 1 8.344 16C3.734 16 0 12.286 0 7.71 0 4.266 2.114 1.312 5.124.06A.75.75 0 0 1 6 .278"/>
            <path v-else d="M8 11a3 3 0 1 1 0-6 3 3 0 0 1 0 6m0 1a4 4 0 1 0 0-8 4 4 0 0 0 0 8M8 0a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 0m0 13a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 13m8-5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2a.5.5 0 0 1 .5.5M3 8a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2A.5.5 0 0 1 3 8m10.657-5.657a.5.5 0 0 1 0 .707l-1.414 1.415a.5.5 0 1 1-.707-.708l1.414-1.414a.5.5 0 0 1 .707 0m-9.193 9.193a.5.5 0 0 1 0 .707L3.05 13.657a.5.5 0 0 1-.707-.707l1.414-1.414a.5.5 0 0 1 .707 0m9.193 2.121a.5.5 0 0 1-.707 0l-1.414-1.414a.5.5 0 0 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .707M4.464 4.465a.5.5 0 0 1-.707 0L2.343 3.05a.5.5 0 1 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .708"/>
          </svg>
        </button>
      </div>

      <p class="text-center italic brightness-70">Like Notion, but lighter</p>

      <!-- search bar -->
      <div class="flex gap-1">
        <input type="text" placeholder="search" class="border w-full p-1 rounded-xl" v-model="searchQuery">
        
        <button class="bg-black text-white p-2 rounded-xl dark:bg-gray-600"  @click="toggleSorting">
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
            <path v-if="sortOrder === 'asc'" fill-rule="evenodd" d="M7.646 4.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1-.708.708L8 5.707l-5.646 5.647a.5.5 0 0 1-.708-.708z"/>          
            <path v-else fill-rule="evenodd" d="M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708"/>
          </svg>
        </button>
      </div>
    </div>

    <!-- notes list -->
    <div class="flex-1 flex flex-col mt-5">
      <p v-if="loadingNotes" class="text-center">Loading...</p>
      <div v-else class="space-y-5">
        <NoteCard
          v-for="note in filteredList"
          :key="note.id"
          :id="note.id"
          :title="note.title"
          :date="new Date(note.createdAt).toLocaleDateString('en-GB')"
          @deleted="id => list = list.filter(n => n.id !== id)"
        /> 
      </div>
    </div>
   </div>
    
   <!-- notes submission form -->
    <form 
      class="flex justify-center bg-white p-3 bottom-0 sticky w-full xl:w-110 dark:bg-gray-800 mx-auto xl:top-0 xl:fixed xl:right-0 xl:h-max"
      @submit.prevent="addNote"
    >
      <div class="border p-3 w-80 flex flex-col gap-3">
        <label for="title">Title</label>
        <input v-model="noteTitle" type="text" id="title" class="border">
        <p v-if="!noteTitle" class="text-red-500 dark:text-red-200 text-sm">Title cannot be empty</p>

        <label for="content">Content</label>
        <textarea v-model="noteContent" name="content" id="content" class="border field-sizing-content min-h-20 max-h-30"></textarea>
        
        <button type="submit" :disabled="isSubmitting" class="bg-blue-400 disabled:brightness-70 p-1 w-30 mx-auto text-white rounded-md dark:bg-gray-600">
          <p v-if="isSubmitting">Submitting...</p>
          <p v-else>Add Note</p>
        </button>
      </div> 
    </form>
</template>
