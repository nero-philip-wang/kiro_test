<template>
  <div class="profile-page">
    <h2>{{ $t('employee.profile') }}</h2>
    <div class="profile-card">
      <div class="avatar">
        <img src="../../assets/vue.svg" alt="Avatar" />
      </div>
      <div class="info">
        <p><strong>姓名:</strong> {{ user.name }}</p>
        <p><strong>部门:</strong> {{ user.department }}</p>
        <p><strong>积分:</strong> {{ user.points }}</p>
      </div>
    </div>
    <div class="menu-list">
      <div class="menu-item" @click="goToOrders">
        <span>{{ $t('employee.orders') }}</span>
        <span class="arrow">></span>
      </div>
      <div class="menu-item" @click="goToAddresses">
        <span>收货地址</span>
        <span class="arrow">></span>
      </div>
      <div class="menu-item" @click="changePassword">
        <span>修改密码</span>
        <span class="arrow">></span>
      </div>
      <div class="menu-item logout" @click="logout">
        <span>{{ $t('common.logout') }}</span>
        <span class="arrow">></span>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const user = ref({
  name: '员工用户',
  department: '技术部',
  points: 0
})

const goToOrders = () => {
  // Navigate to orders
}

const goToAddresses = () => {
  // Navigate to addresses
}

const changePassword = () => {
  // Show change password dialog
}

const logout = () => {
  localStorage.removeItem('token')
  window.location.href = '/'
}
</script>

<style scoped>
.profile-page {
  padding-bottom: 60px;
}

.profile-card {
  display: flex;
  align-items: center;
  padding: 1rem;
  background: #f9f9f9;
  border-radius: 8px;
  margin-bottom: 1rem;
}

.avatar img {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  margin-right: 1rem;
}

.info p {
  margin: 0.25rem 0;
}

.menu-list {
  border-top: 1px solid #eee;
}

.menu-item {
  display: flex;
  justify-content: space-between;
  padding: 1rem;
  border-bottom: 1px solid #eee;
  cursor: pointer;
}

.menu-item:hover {
  background: #f5f5f5;
}

.menu-item.logout {
  color: #e74c3c;
}

.arrow {
  color: #999;
}
</style>