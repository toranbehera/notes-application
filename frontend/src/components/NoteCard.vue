<script setup lang="ts">
import { RouterLink } from 'vue-router';
import axios from 'axios';
import { ref } from 'vue';

const props = defineProps(['id', 'title', 'date']);

const url = 'http://localhost:5184/notes';

const isDeleting = ref(false);

const emit = defineEmits(['deleted']);

async function deleteNote(){
    isDeleting.value = true;

    try{
        await axios.delete(`${url}/${props.id}`);
        emit('deleted', props.id);
    } catch(err){
        console.error(err);
    } finally{
        isDeleting.value = false;
    }
}

</script>

<template>
    <div :class="`flex gap-3 ${isDeleting && 'brightness-70'} justify-between p-3 rounded-xl border-1 bg-yellow-100 w-100 mx-auto dark:text-gray-200 dark:bg-blue-950 dark:border-gray-600`">
        <RouterLink :to="`/notes/${id}`" class="flex flex-1 justify-between">
            <h2>{{ title }}</h2>
            <p>{{ date }}</p>   
        </RouterLink>
        <div class="">
            <button @click="deleteNote">x</button>
        </div> 
    </div>
</template>
