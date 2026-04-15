<template>
  <div class="admin-categories">
    <div class="page-header">
      <h2>{{ $t('admin.categories') }}</h2>
      <button class="add-btn" @click="showAddDialog = true">添加分类</button>
    </div>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>名称</th>
          <th>描述</th>
          <th>商品数</th>
          <th>操作</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="category in categories" :key="category.id">
          <td>{{ category.id }}</td>
          <td>{{ category.name }}</td>
          <td>{{ category.description }}</td>
          <td>{{ category.productCount }}</td>
          <td>
            <button @click="editCategory(category)">编辑</button>
            <button class="delete-btn" @click="deleteCategory(category)">删除</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const showAddDialog = ref(false)
const categories = ref([])

const editCategory = (category) => {
  console.log('Edit:', category)
}

const deleteCategory = (category) => {
  console.log('Delete:', category)
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