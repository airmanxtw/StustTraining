<template>
    <v-container>
      <div v-for="(d,index) in data" :key="index">
        {{d}}
      </div>
      <div class="sticky">
        <v-btn color="primary" class="mr-2">this is Btn</v-btn>
        <v-btn color="secondary">this is Btn</v-btn>
      </div>
    </v-container>
  </template>
  
  <script setup>
  import { ref, onMounted } from "vue";
  let data = ref([]);
  onMounted(() => {
    for (let i = 0; i < 100; i++) data.value.push(`this is line no ${i}`);
  });
  </script>
  <style scoped>
  .sticky {
    position: -webkit-sticky; /* Safari */
    position: sticky;
    bottom: 0;
    text-align: right;
    padding-bottom: 10px;
  }
  </style>
  