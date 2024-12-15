<template>
    <div> <h2>Crypto currencies average price </h2> </div>
    <div>
      <table>
      <thead>
        <tr>
          <th>Symbol</th>
          <th>Price</th>
          <th>Min</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.symbol">
          <td>{{ item.symbol }}</td>
          <td>{{ item.price }}</td>
          <td>{{ item.mins }}</td>
        </tr>
      </tbody>
    </table>
    </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'

let items = ref([])

onMounted(async () => {
  const response = await fetch('http://localhost:5102/api/crypto/')
  items.value = await response.json()
  console.log(items)
})


</script>