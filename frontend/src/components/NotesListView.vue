<script setup lang="ts">
import { computed, ref } from 'vue'
import NoteCard from './NoteCard.vue';

const noteTitle = ref('');
const noteDate = ref();

const searchQuery = ref('');
const sortOrder = ref('asc');

const list = ref([
  {id: 1, title: 'Walk the dog', date: new Date('2026-02-12')},
  {id: 2, title: 'Water the plants', date: new Date('2026-01-07')},
  {id: 3, title: 'Get groceries', date: new Date('2026-02-15')}
]);

const filteredList = computed(() => {
  const query = searchQuery.value.toLowerCase()

  return list.value
    .filter(note => note.title.toLowerCase().includes(query))
    .sort((a, b) =>
      sortOrder.value === 'asc'
        ? a.date.getTime() - b.date.getTime()
        : b.date.getTime() - a.date.getTime()
    )
})

function addNote(){
  list.value.push({id: Math.floor(Math.random() * 10), title: noteTitle.value, date: new Date()})
}

function toggleSorting() {
  sortOrder.value = sortOrder.value === 'asc' ? 'desc' : 'asc'
}

</script>

<template>
  <div class="mx-auto space-y-10 text-center">
    
    <div class="bg-white flex flex-col gap-5 fixed top-0 w-100 py-2">
      <h1 class="text-xl font-bold">Lotion</h1>

      <div class="flex gap-1">
        <input type="text" placeholder="search" class="border-1 w-full p-1 rounded-xl" v-model="searchQuery">
        
        <button class="bg-black text-white p-2 rounded-xl"  @click="toggleSorting">
          <svg v-if="sortOrder === 'asc'" xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
            <path fill-rule="evenodd" d="M7.646 4.646a.5.5 0 0 1 .708 0l6 6a.5.5 0 0 1-.708.708L8 5.707l-5.646 5.647a.5.5 0 0 1-.708-.708z"/>
          </svg>
          
          <svg v-else xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
            <path fill-rule="evenodd" d="M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708"/>
          </svg>
        </button>
      </div>
    </div>

    <div class="space-y-5 mt-20">
     <NoteCard
        v-for="note in filteredList"
        :key="note.date.getTime()"
        :id="note.id"
        :title="note.title"
        :date="note.date.toLocaleDateString('en-gb')"
      /> 
    </div>
    
    <div class="bg-white p-3 sticky bottom-5 w-100">
      <div class="border-1 p-3 flex flex-col gap-3">
        <label for="title">Title</label>
        <input v-model="noteTitle" type="text" id="title" class="border">

        <label for="date">Date</label>
        <input v-model="noteDate" type="text" id="date" class="border">

        <label for="content">Content</label>
        <textarea name="content" id="content" class="border"></textarea>
        
        <button @click="addNote" class="bg-blue-400 p-1 w-30 mx-auto text-white rounded-md">Add to list</button>
      </div> 
    </div>
    
  </div>
</template>
