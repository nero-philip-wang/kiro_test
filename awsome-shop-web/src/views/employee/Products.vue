<template>
  <div class="products-page">
    <h2>{{ $t('employee.products') }}</h2>
    <div class="search-bar">
      <input v-model="searchQuery" type="text" :placeholder="$t('common.search')" />
    </div>
    <div class="products-grid">
      <div v-for="product in products" :key="product.id" class="product-card">
        <img :src="product.image" :alt="product.name" />
        <h3>{{ product.name }}</h3>
        <p class="price">{{ product.price }} {{ $t('employee.points') }}</p>
        <button @click="addToCart(product)">{{ $t('employee.cart') }}</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import api from '@/api'

const searchQuery = ref('')
const products = ref([])

const loadProducts = async () => {
  try {
    products.value = await api.get('/products')
  } catch (error) {
    console.error('Failed to load products:', error)
  }
}

const addToCart = (product) => {
  console.log('Added to cart:', product)
}

loadProducts()
</script>

<style scoped>
.products-page {
  padding-bottom: 60px;
}

.search-bar input {
  width: 100%;
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  margin-bottom: 1rem;
}

.products-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(150px, 1fr));
  gap: 1rem;
}

.product-card {
  border: 1px solid #eee;
  border-radius: 8px;
  padding: 1rem;
  text-align: center;
}

.product-card img {
  width: 100%;
  height: 120px;
  object-fit: cover;
  border-radius: 4px;
}

.price {
  color: #42b883;
  font-weight: bold;
}

button {
  background: #42b883;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 4px;
  cursor: pointer;
}
</style>