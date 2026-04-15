<template>
  <div class="dashboard">
    <h2>{{ $t('admin.dashboard') }}</h2>
    <div class="stats-grid">
      <div class="stat-card">
        <h3>今日订单</h3>
        <p class="stat-value">{{ stats.todayOrders }}</p>
      </div>
      <div class="stat-card">
        <h3>今日销售额</h3>
        <p class="stat-value">{{ stats.todaySales }} 积分</p>
      </div>
      <div class="stat-card">
        <h3>商品总数</h3>
        <p class="stat-value">{{ stats.totalProducts }}</p>
      </div>
      <div class="stat-card">
        <h3>员工总数</h3>
        <p class="stat-value">{{ stats.totalEmployees }}</p>
      </div>
    </div>
    <div class="recent-orders">
      <h3>最近订单</h3>
      <table>
        <thead>
          <tr>
            <th>订单号</th>
            <th>员工</th>
            <th>金额</th>
            <th>状态</th>
            <th>时间</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="order in recentOrders" :key="order.id">
            <td>{{ order.id }}</td>
            <td>{{ order.employee }}</td>
            <td>{{ order.amount }} 积分</td>
            <td>{{ order.status }}</td>
            <td>{{ order.time }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const stats = ref({
  todayOrders: 0,
  todaySales: 0,
  totalProducts: 0,
  totalEmployees: 0
})

const recentOrders = ref([])
</script>

<style scoped>
.dashboard h2 {
  margin-bottom: 1.5rem;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1rem;
  margin-bottom: 2rem;
}

.stat-card {
  background: white;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.stat-card h3 {
  margin: 0 0 0.5rem 0;
  font-size: 0.9rem;
  color: #666;
}

.stat-value {
  margin: 0;
  font-size: 1.5rem;
  font-weight: bold;
  color: #42b883;
}

.recent-orders {
  background: white;
  padding: 1.5rem;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.recent-orders h3 {
  margin: 0 0 1rem 0;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  padding: 0.75rem;
  text-align: left;
  border-bottom: 1px solid #eee;
}

th {
  font-weight: 600;
  color: #666;
}
</style>