<script setup lang="ts">
import { RouterView } from 'vue-router';
import { useThemeStore } from './store';

const theme = useThemeStore();
</script>

<template>
  <main :class="`bg-white flex flex-col mx-auto gap-3 p-5 dark:bg-gray-800 dark:text-white ${theme.isDark ? 'dark' : ''}`">
    <RouterView />
  </main>
  
</template>
