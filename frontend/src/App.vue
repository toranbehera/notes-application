<script setup lang="ts">
import { RouterView } from 'vue-router';
import { useThemeStore } from './store';

const theme = useThemeStore();
</script>

<template>
  <main :class="`flex flex-col min-h-screen dark:text-white ${theme.isDark ? 'dark' : ''} dark:bg-gray-800`">
    <RouterView />
  </main>
</template>
