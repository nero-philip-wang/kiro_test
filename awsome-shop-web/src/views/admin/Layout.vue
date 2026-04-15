<template>
  <div class="admin-layout">
    <aside class="sidebar">
      <h2>{{ $t('admin.title') }}</h2>
      <nav>
        <router-link to="/admin">{{ $t('admin.dashboard') }}</router-link>
        <router-link to="/admin/products">{{ $t('admin.products') }}</router-link>
        <router-link to="/admin/orders">{{ $t('admin.orders') }}</router-link>
        <router-link to="/admin/categories">{{ $t('admin.categories') }}</router-link>
        <router-link to="/admin/employees">{{ $t('admin.employees') }}</router-link>
      </nav>
      <div class="sidebar-footer">
        <select v-model="locale" @change="changeLocale">
          <option value="zh-CN">中文</option>
          <option value="en-US">EN</option>
        </select>
        <button @click="logout">{{ $t('common.logout') }}</button>
      </div>
    </aside>
    <main class="main-content">
      <router-view />
    </main>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

const { locale } = useI18n()

const changeLocale = (e) => {
  locale.value = e.target.value
}

const logout = () => {
  localStorage.removeItem('token')
  window.location.href = '/'
}
</script>

<style scoped>
.admin-layout {
  display: flex;
  min-height: 100vh;
}

.sidebar {
  width: 240px;
  background: #2c3e50;
  color: white;
  padding: 1rem;
  display: flex;
  flex-direction: column;
}

.sidebar h2 {
  margin: 0 0 1rem 0;
  font-size: 1.2rem;
}

.sidebar nav {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  flex: 1;
}

.sidebar nav a {
  color: #ecf0f1;
  text-decoration: none;
  padding: 0.75rem;
  border-radius: 4px;
  transition: background 0.2s;
}

.sidebar nav a:hover {
  background: #34495e;
}

.sidebar nav a.router-link-active {
  background: #42b883;
}

.sidebar-footer {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.sidebar-footer select,
.sidebar-footer button {
  padding: 0.5rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.sidebar-footer button {
  background: #e74c3c;
  color: white;
}

.main-content {
  flex: 1;
  padding: 2rem;
  background: #f5f5f5;
  overflow-y: auto;
}
</style>