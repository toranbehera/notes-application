<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router';
import { onMounted, ref } from 'vue';
import axios from 'axios';

const route = useRoute();
const router = useRouter();

function navigateHome(){
    router.push("/")
}

type Note = {
  id: number,
  title: string,
  content?: string,
  createdAt: string, 
  updatedAt?: string
}

const url = 'http://localhost:5184/notes';

const note = ref<Note>();

const loadingNote = ref(true);
const isSubmitting = ref(false);

const newTitle = ref('');
const newContent = ref('');

async function updateNote() {
    if(!newTitle.value && !newContent.value) return;

    isSubmitting.value = true;

    try {
        const payload = {
            title: newTitle.value.trim()
                ? newTitle.value
                : note.value?.title,

            content: newContent.value
        };

        await axios.put(`${url}/${route.params.id}`, payload);
        await getNote();
    } catch (err) {
        console.error(err);
    } finally{
        isSubmitting.value = false;
    }
}

async function getNote() {
    try{
        const res = await axios.get(`${url}/${route.params.id}`);
        note.value = res.data[0];
    } catch(err){
        console.error(err);
    } finally {
        loadingNote.value = false;
    }
}

onMounted(async () => {
    await getNote();

    if(note.value){
        newContent.value = note.value.content ?? "";
    }
});
</script>

<template>
    <div class="flex min-h-screen max-w-100 mx-auto">
        <p v-if="loadingNote" class="self-center">loading...</p>
        <div v-else class="space-y-5">
            <button @click="navigateHome">
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org">
                <path d="M19 12H5M5 12L12 19M5 12L12 5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            </button>
            
            <div class="flex justify-between">
                <h1 class="text-xl font-bold">
                    {{ note?.title }}
                </h1>
                <div class="text-sm space-y-2 text-right">
                    <p>Created: {{ new Date(note?.createdAt!).toLocaleDateString() }}</p>
                    <p v-if="note?.updatedAt">Last updated: {{ new Date(note?.updatedAt).toLocaleDateString() }}</p>
                </div>
            </div>

            <form class="space-y-5" @submit.prevent="updateNote">
                <input 
                    v-model="newTitle"
                    type="text" 
                    class="border-1 text-xl w-full"
                    placeholder="New title"
                />

                <textarea v-model="newContent" class="border-1 w-full field-sizing-content min-h-40 max-h-130"></textarea>

                <button type="submit" :disabled="isSubmitting" class="bg-green-700 disabled:brightness-70 text-white p-2 rounded-md">
                    <p v-if="isSubmitting">Submitting...</p>
                    <p v-else>Save changes</p>
                </button> 
            </form>
        </div>
        
    </div>
</template>
