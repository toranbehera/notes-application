<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

function navigateHome(){
    router.push("/")
}
</script>

<template>
    <div class="space-y-5">
    <button @click="navigateHome">
        <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org">
            <path d="M19 12H5M5 12L12 19M5 12L12 5" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
    </button>
    
    <h1 class="text-xl font-bold">
        Note {{ route.params.id }}
    </h1>

    <input 
        type="text" 
        class="border-1 text-black text-xl w-full"
        placeholder="New title"
    />

    <textarea class="border-1 w-full"></textarea>

    <button class="bg-green-600 text-white p-2 mx-auto rounded-md">
        Save changes
    </button>
</div>
</template>
