<template>
  <div class="employee-layout">
    <header class="header">
      <h1>{{ $t('employee.title') }}</h1>
      <div class="header-actions">
        <span class="points">{{ $t('employee.points') }}: {{ points }}</span>
        <select v-model="locale" @change="changeLocale">
          <option value="zh-CN">中文</option>
          <option value="en-US">EN</option>
        </select>
      </div>
    </header>
    <main class="main">
      <router-view />
    </main>
    <nav class="bottom-nav">
      <router-link to="/employee">{{ $t('common.home') }}</router-link>
      <router-link to="/employee/products">{{ $t('employee.products') }}</router-link>
      <router-link to="/employee/cart">{{ $t('employee.cart') }}</router-link>
      <router-link to="/employee/profile">{{ $t('employee.profile') }}</router-link>
    </nav>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useI18n } from 'vue-i18n'

const { locale } = useI18n()
const points = ref(0)

const changeLocale = (e) => {
  locale.value = e.target.value
}
</script>

<style scoped>
.employee-layout {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  background: #42b883;
  color: white;
  padding: 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header h1 {
  margin: 0;
  font-size: 1.2rem;
}

.header-actions {
  display: flex;
  gap: 1rem;
  align-items: center;
}

.points {
  font-weight: bold;
}

.main {
  flex: 1;
  padding: 1rem;
}

.bottom-nav {
  display: flex;
  justify-content: space-around;
  padding: 1rem;
  background: #f5f5f5;
  position: fixed;
  bottom: 0;
  width: 100%;
}

.bottom-nav a {
  color: #42b883;
  text-decoration: none;
}

.bottom-nav a.router-link-active {
  font-weight: bold;
}
</style>