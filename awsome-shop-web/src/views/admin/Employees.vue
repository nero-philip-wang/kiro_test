<template>
  <div class="admin-employees">
    <div class="page-header">
      <h2>{{ $t('admin.employees') }}</h2>
      <button class="add-btn" @click="showAddDialog = true">添加员工</button>
    </div>
    <div class="search-bar">
      <input v-model="searchQuery" type="text" placeholder="搜索员工..." />
    </div>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>姓名</th>
          <th>邮箱</th>
          <th>部门</th>
          <th>积分余额</th>
          <th>状态</th>
          <th>操作</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="employee in employees" :key="employee.id">
          <td>{{ employee.id }}</td>
          <td>{{ employee.name }}</td>
          <td>{{ employee.email }}</td>
          <td>{{ employee.department }}</td>
          <td>{{ employee.points }}</td>
          <td>{{ employee.status }}</td>
          <td>
            <button @click="editEmployee(employee)">编辑</button>
            <button @click="adjustPoints(employee)">调整积分</button>
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
const employees = ref([])

const editEmployee = (employee) => {
  console.log('Edit:', employee)
}

const adjustPoints = (employee) => {
  console.log('Adjust points:', employee)
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
</style>