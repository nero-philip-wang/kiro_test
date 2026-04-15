<template>
  <div class="admin-products">
    <div class="page-header">
      <h2>{{ $t('admin.products') }}</h2>
      <button class="add-btn" @click="showAddDialog = true">添加商品</button>
    </div>
    <div class="search-bar">
      <input v-model="searchQuery" type="text" placeholder="搜索商品..." />
    </div>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>名称</th>
          <th>分类</th>
          <th>价格(积分)</th>
          <th>库存</th>
          <th>状态</th>
          <th>操作</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="product in products" :key="product.id">
          <td>{{ product.id }}</td>
          <td>{{ product.name }}</td>
          <td>{{ product.category }}</td>
          <td>{{ product.price }}</td>
          <td>{{ product.stock }}</td>
          <td>{{ product.status }}</td>
          <td>
            <button @click="editProduct(product)">编辑</button>
            <button class="delete-btn" @click="deleteProduct(product)">删除</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const searchQuery = ref('')
const showAddDialog = ref(false)
const products = ref([])

const editProduct = (product) => {
  console.log('Edit:', product)
}

const deleteProduct = (product) => {
  console.log('Delete:', product)
}
</script>

<style scoped>
.page-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.page-header h2 {
  margin: 0;
}

.add-btn {
  background: #42b883;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 4px;
  cursor: pointer;
}

.search-bar {
  margin-bottom: 1rem;
}

.search-bar input {
  padding: 0.5rem;
  border: 1px solid #ddd;
  border-radius: 4px;
  width: 300px;
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

.delete-btn {
  background: #e74c3c;
}
</style>