<script setup lang="ts">
import { RouterLink } from 'vue-router';

defineProps(['id', 'title', 'date']);


</script>

<template>
    <div class="rounded-xl border-1 bg-yellow-100 w-100 mx-auto dark:text-gray-200 dark:bg-blue-950 dark:border-gray-600">
        <RouterLink :to="`/notes/${id}`">
            <div class="flex justify-between p-3">
                <h2>{{ title }}</h2>
                <div class="flex gap-2">
                    <p>{{ date }}</p> 
                    <button>x</button>
                </div>  
            </div>
        </RouterLink>
    </div>
</template>
