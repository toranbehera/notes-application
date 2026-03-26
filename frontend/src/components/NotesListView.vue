<script setup lang="ts">
import { computed, ref } from 'vue'
import NoteCard from './NoteCard.vue';

const noteTitle = ref('');
const noteDate = ref('');
const searchQuery = ref('');

const list = ref([
  {id: 1, title: 'Walk the dog', date: '02/12/26'},
  {id: 2, title: 'Water the plants', date: '01/07/26'},
  {id: 3, title: 'Get groceries', date: '02/15/26'}
]);

function addNote(){
  list.value.push({id: Math.floor(Math.random() * 10), title: noteTitle.value, date: noteDate.value})
}

const filteredList = computed(() => 
  list.value.filter(note => 
    note.title.toLowerCase().includes(searchQuery.value.toLowerCase())
  )
)

</script>

<template>
  <div class="mx-auto space-y-10 text-center">
    
    <div class="bg-white flex flex-col gap-5 fixed top-0 w-100 pt-1">
      <h1 class="text-xl font-bold">Lotion</h1>

      <div class="flex">
        <input type="text" placeholder="search" class="border-1 w-full p-1 rounded-xl" v-model="searchQuery">
      </div>
    </div>

    <div class="space-y-5 mt-20">
     <NoteCard
        v-for="note in filteredList"
        :key="note.id"
        :id="note.id"
        :title="note.title"
        :date="note.date"
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
