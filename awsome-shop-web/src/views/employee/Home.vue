<template>
  <div class="employee-home">
    <h2>{{ $t('employee.welcome') }}</h2>
    <div class="hero-section">
      <img src="../../assets/hero.png" alt="Hero" />
    </div>
    <div class="quick-actions">
      <router-link to="/employee/products" class="action-btn">
        {{ $t('employee.products') }}
      </router-link>
      <router-link to="/employee/cart" class="action-btn">
        {{ $t('employee.cart') }}
      </router-link>
      <router-link to="/employee/orders" class="action-btn">
        {{ $t('employee.orders') }}
      </router-link>
    </div>
  </div>
</template>

<script setup>
</script>

<style scoped>
.employee-home {
  text-align: center;
}

.hero-section img {
  width: 100%;
  max-width: 400px;
  border-radius: 8px;
  margin: 1rem 0;
}

.quick-actions {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  max-width: 300px;
  margin: 2rem auto;
}

.action-btn {
  padding: 1rem;
  background: #42b883;
  color: white;
  text-decoration: none;
  border-radius: 8px;
  font-weight: bold;
}

.action-btn:hover {
  background: #359268;
}
</style>