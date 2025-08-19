<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getBooks, type BookDto } from '@/services/bookService'

const books = ref<BookDto[]>([])

onMounted(async () => {
  books.value = await getBooks()
})
</script>

<template>
  <div>
    <h1>Books</h1>
    <ul>
      <li v-for="b in books" :key="b.id">{{ b.title }} - {{ b.author }}</li>
    </ul>
  </div>
</template>
