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
    <button @click="navigateHome">Go back</button>
    
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
