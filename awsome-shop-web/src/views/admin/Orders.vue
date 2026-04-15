<template>
  <div class="admin-orders">
    <h2>{{ $t('admin.orders') }}</h2>
    <div class="filters">
      <select v-model="statusFilter">
        <option value="">全部状态</option>
        <option value="pending">待处理</option>
        <option value="processing">处理中</option>
        <option value="completed">已完成</option>
        <option value="cancelled">已取消</option>
      </select>
    </div>
    <table>
      <thead>
        <tr>
          <th>订单号</th>
          <th>员工</th>
          <th>商品</th>
          <th>金额</th>
          <th>状态</th>
          <th>时间</th>
          <th>操作</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="order in orders" :key="order.id">
          <td>{{ order.id }}</td>
          <td>{{ order.employee }}</td>
          <td>{{ order.itemsCount }} 件</td>
          <td>{{ order.amount }} 积分</td>
          <td>{{ order.status }}</td>
          <td>{{ order.time }}</td>
          <td>
            <button @click="viewOrder(order)">查看</button>
            <button @click="updateStatus(order)">更新状态</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const statusFilter = ref('')
const orders = ref([])

const viewOrder = (order) => {
  console.log('View:', order)
}

const updateStatus = (order) => {
  console.log('Update status:', order)
}
</script>

<style scoped>
.admin-orders h2 {
  margin-bottom: 1rem;
}

.filters {
  margin-bottom: 1rem;
}

.filters select {
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 4px;
}

table {
  width: 100%;
  background: white;
  border-collapse: collapse;
  border-radius: 8px;
  overflow: hidden;
}

th, td {
  padding: 1rem;
  text-align: left;
  border-bottom: 1px solid #eee;
}

th {
  background: #f9f9f9;
  font-weight: 600;
}

button {
  background: #42b883;
  color: white;
  border: none;
  padding: 0.25rem 0.5rem;
  border-radius: 4px;
  cursor: pointer;
  margin-right: 0.5rem;
}
</style>