<script setup lang="ts">
import { ref } from 'vue'

const count = ref(0)
</script>

<template>
  <section class="justify-self-center a">
    <h1>Note Taker</h1>

    <button class="text-white bg-blue-400 p-4" @click="count++">Count is {{ count }}</button>
  </section>
</template>
