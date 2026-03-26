<script setup lang="ts">
import { ref } from 'vue'
import NoteCard from './NoteCard.vue';

const noteTitle = ref('');
const noteDate = ref('');

const list = ref([
  {id: 1, title: 'Walk the dog', date: '02/12/26'},
  {id: 2, title: 'Water the plants', date: '01/07/26'},
  {id: 3, title: 'Get groceries', date: '02/15/26'}
]);

function addNote(){
  list.value.push({id: Math.floor(Math.random() * 10), title: noteTitle.value, date: noteDate.value})
}

// function removeNote(){
// }

</script>

<template>
  <div class="mx-auto space-y-10 text-center">
    <h1 class="text-xl font-bold">Lotion</h1>

    <NoteCard
      v-for="note in list"
      :key="note.id"
      :id="note.id"
      :title="note.title"
      :date="note.date"
    />

    <div class="border-1 p-3 flex flex-col gap-3 w-70 mx-auto">
      <label for="title">Title</label>
      <input v-model="noteTitle" type="text" id="title" class="border">

      <label for="date">Date</label>
      <input v-model="noteDate" type="text" id="date" class="border">

      <label for="content">Content</label>
      <textarea name="content" id="content" class="border"></textarea>
      
      <button @click="addNote" class="bg-blue-400 p-1 w-30 mx-auto text-white rounded-md">Add to list</button>
    </div>
  </div>
</template>
