<template>
  <div class="cart-page">
    <h2>{{ $t('employee.cart') }}</h2>
    <div v-if="cartItems.length === 0" class="empty-cart">
      <p>购物车为空</p>
    </div>
    <div v-else>
      <div v-for="item in cartItems" :key="item.id" class="cart-item">
        <img :src="item.image" :alt="item.name" />
        <div class="item-info">
          <h3>{{ item.name }}</h3>
          <p class="price">{{ item.price }} 积分</p>
        </div>
        <div class="item-actions">
          <button @click="decreaseQty(item)">-</button>
          <span>{{ item.quantity }}</span>
          <button @click="increaseQty(item)">+</button>
        </div>
      </div>
      <div class="cart-summary">
        <p>总计: {{ totalPoints }} 积分</p>
        <button class="checkout-btn" @click="checkout">{{ $t('employee.checkout') }}</button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const cartItems = ref([
  { id: 1, name: '示例商品', price: 100, quantity: 1, image: '' }
])

const totalPoints = computed(() => {
  return cartItems.value.reduce((sum, item) => sum + item.price * item.quantity, 0)
})

const increaseQty = (item) => {
  item.quantity++
}

const decreaseQty = (item) => {
  if (item.quantity > 1) {
    item.quantity--
  }
}

const checkout = () => {
  console.log('Checkout:', cartItems.value)
}
</script>

<style scoped>
.cart-page {
  padding-bottom: 60px;
}

.empty-cart {
  text-align: center;
  padding: 2rem;
  color: #999;
}

.cart-item {
  display: flex;
  align-items: center;
  padding: 1rem;
  border-bottom: 1px solid #eee;
}

.cart-item img {
  width: 60px;
  height: 60px;
  object-fit: cover;
  border-radius: 4px;
  margin-right: 1rem;
}

.item-info {
  flex: 1;
}

.item-actions {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.item-actions button {
  width: 30px;
  height: 30px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.cart-summary {
  padding: 1rem;
  text-align: right;
}

.checkout-btn {
  background: #42b883;
  color: white;
  border: none;
  padding: 0.75rem 2rem;
  border-radius: 4px;
  cursor: pointer;
  font-size: 1rem;
  margin-top: 1rem;
}
</style>