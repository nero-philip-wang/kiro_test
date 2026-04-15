<template>
  <div class="orders-page">
    <h2>{{ $t('employee.orders') }}</h2>
    <div v-if="orders.length === 0" class="empty-orders">
      <p>暂无订单</p>
    </div>
    <div v-else class="orders-list">
      <div v-for="order in orders" :key="order.id" class="order-card">
        <div class="order-header">
          <span class="order-id">订单号: {{ order.id }}</span>
          <span class="order-status">{{ order.status }}</span>
        </div>
        <div class="order-items">
          <div v-for="item in order.items" :key="item.id" class="order-item">
            <span>{{ item.name }}</span>
            <span>x{{ item.quantity }}</span>
          </div>
        </div>
        <div class="order-footer">
          <span class="total">总计: {{ order.total }} 积分</span>
          <span class="date">{{ order.date }}</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const orders = ref([])

const loadOrders = async () => {
  // Placeholder for API call
}
</script>

<style scoped>
.orders-page {
  padding-bottom: 60px;
}

.empty-orders {
  text-align: center;
  padding: 2rem;
  color: #999;
}

.orders-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.order-card {
  border: 1px solid #eee;
  border-radius: 8px;
  padding: 1rem;
}

.order-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.5rem;
  font-size: 0.9rem;
  color: #666;
}

.order-status {
  color: #42b883;
  font-weight: bold;
}

.order-items {
  padding: 0.5rem 0;
  border-top: 1px solid #eee;
}

.order-item {
  display: flex;
  justify-content: space-between;
  padding: 0.25rem 0;
}

.order-footer {
  display: flex;
  justify-content: space-between;
  padding-top: 0.5rem;
  border-top: 1px solid #eee;
  font-weight: bold;
}
</style>