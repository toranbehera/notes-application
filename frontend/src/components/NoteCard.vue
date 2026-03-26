<script setup lang="ts">
import { RouterLink } from 'vue-router';

defineProps(['id', 'title', 'date']);

</script>

<template>
    <div class="rounded-xl border-2 border-gray-400 bg-yellow-100 w-100 mx-auto">
        <RouterLink :to="`/notes/${id}`">
            <div class="flex justify-between p-3">
                <h2>{{ title }}</h2>
                <div class="flex gap-2">
                    <p>{{ date }}</p> 
                    <button class="text-red-600">x</button>
                </div>  
            </div>
        </RouterLink>
    </div>
</template>
