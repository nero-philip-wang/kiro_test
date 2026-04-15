<template>
  <div class="admin-employees">
    <div class="page-header">
      <h2>{{ $t('admin.employees') }}</h2>
      <Button type="primary" @click="openCreateDialog">
        <template #icon>
          <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <line x1="12" y1="5" x2="12" y2="19"></line>
            <line x1="5" y1="12" x2="19" y2="12"></line>
          </svg>
        </template>
        {{ $t('common.add') || '添加员工' }}
      </Button>
    </div>

    <div class="search-bar">
      <Input 
        v-model="searchQuery" 
        :placeholder="$t('common.search') + '...'" 
        @update:modelValue="handleSearch"
      />
    </div>

    <div class="table-container">
      <Table :columns="columns" :data="filteredEmployees" :hoverable="true">
        <template #cell-role="{ value }">
          <span class="role-badge" :class="`role-${value}`">{{ getRoleName(value) }}</span>
        </template>
        <template #cell-isActive="{ value }">
          <span class="status-badge" :class="value ? 'active' : 'inactive'">
            {{ value ? '启用' : '禁用' }}
          </span>
        </template>
        <template #cell-actions="{ row }">
          <div class="action-buttons">
            <Button size="sm" variant="secondary" @click="openEditDialog(row)">
              {{ $t('common.edit') }}
            </Button>
            <Button size="sm" variant="danger" @click="openDeleteDialog(row)">
              {{ $t('common.delete') }}
            </Button>
          </div>
        </template>
      </Table>
    </div>

    <Pagination
      v-model:currentPage="currentPage"
      v-model:pageSize="pageSize"
      :totalItems="totalItems"
      :pageSizeOptions="[10, 20, 50]"
      @page-change="fetchEmployees"
    />

    <!-- Create/Edit Dialog -->
    <Modal
      v-model="showDialog"
      :title="isEditing ? ($t('common.edit') + '员工') : '添加员工'"
      size="md"
      :confirmText="$t('common.save')"
      :cancelText="$t('common.cancel')"
      :confirmLoading="saving"
      @confirm="saveEmployee"
      @close="closeDialog"
    >
      <form @submit.prevent="saveEmployee" class="employee-form">
        <Input
          v-model="form.name"
          :label="$t('employee.name') || '姓名'"
          :placeholder="$t('employee.name') || '请输入姓名'"
          :error="errors.name"
          required
        />
        <Input
          v-model="form.email"
          :label="$t('employee.email') || '邮箱'"
          type="email"
          :placeholder="$t('employee.email') || '请输入邮箱'"
          :error="errors.email"
          :disabled="isEditing"
          required
        />
        <Select
          v-model="form.role"
          :label="$t('employee.role') || '角色'"
          :options="roleOptions"
          :placeholder="$t('employee.role') || '选择角色'"
          :error="errors.role"
          required
        />
        <Checkbox
          v-if="isEditing"
          v-model="form.isActive"
          :label="$t('employee.status') || '启用状态'"
        />
      </form>
    </Modal>

    <!-- Delete Confirmation Dialog -->
    <Modal
      v-model="showDeleteDialog"
      title="确认删除"
      size="sm"
      confirmText="删除"
      cancelText="取消"
      confirmType="danger"
      :confirmLoading="deleting"
      @confirm="deleteEmployee"
    >
      <p>确定要删除员工 <strong>{{ employeeToDelete?.name }}</strong> 吗？</p>
      <p class="warning-text">此操作无法撤销。</p>
    </Modal>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import api from '@/api'
import Button from '@/components/Button.vue'
import Input from '@/components/Input.vue'
import Select from '@/components/Select.vue'
import Checkbox from '@/components/Checkbox.vue'
import Table from '@/components/Table.vue'
import Pagination from '@/components/Pagination.vue'
import Modal from '@/components/Modal.vue'

const columns = [
  { key: 'id', label: 'ID', width: '60px' },
  { key: 'name', label: '姓名' },
  { key: 'email', label: '邮箱' },
  { key: 'roleName', label: '角色' },
  { key: 'isActive', label: '状态' },
  { key: 'actions', label: '操作', width: '180px' }
]

const roleOptions = [
  { value: 0, label: '员工' },
  { value: 1, label: '管理员' },
  { value: 2, label: '超级管理员' }
]

const employees = ref([])
const searchQuery = ref('')
const currentPage = ref(1)
const pageSize = ref(10)
const totalItems = ref(0)

const showDialog = ref(false)
const showDeleteDialog = ref(false)
const isEditing = ref(false)
const saving = ref(false)
const deleting = ref(false)
const employeeToDelete = ref(null)

const form = ref({
  id: null,
  name: '',
  email: '',
  role: 0,
  isActive: true
})

const errors = ref({
  name: '',
  email: '',
  role: ''
})

const filteredEmployees = computed(() => {
  if (!searchQuery.value) return employees.value
  const query = searchQuery.value.toLowerCase()
  return employees.value.filter(emp => 
    emp.name?.toLowerCase().includes(query) ||
    emp.email?.toLowerCase().includes(query)
  )
})

const getRoleName = (role) => {
  const roleMap = {
    0: '员工',
    1: '管理员',
    2: '超级管理员'
  }
  return roleMap[role] || '员工'
}

const fetchEmployees = async () => {
  try {
    const data = await api.get('/users')
    employees.value = data
    totalItems.value = data.length
  } catch (error) {
    console.error('Failed to fetch employees:', error)
  }
}

const handleSearch = () => {
  currentPage.value = 1
}

const openCreateDialog = () => {
  isEditing.value = false
  form.value = {
    id: null,
    name: '',
    email: '',
    role: 0,
    isActive: true
  }
  errors.value = { name: '', email: '', role: '' }
  showDialog.value = true
}

const openEditDialog = (employee) => {
  isEditing.value = true
  form.value = {
    id: employee.id,
    name: employee.name,
    email: employee.email,
    role: employee.role,
    isActive: employee.isActive
  }
  errors.value = { name: '', email: '', role: '' }
  showDialog.value = true
}

const openDeleteDialog = (employee) => {
  employeeToDelete.value = employee
  showDeleteDialog.value = true
}

const closeDialog = () => {
  showDialog.value = false
}

const validateForm = () => {
  let valid = true
  errors.value = { name: '', email: '', role: '' }

  if (!form.value.name?.trim()) {
    errors.value.name = '请输入姓名'
    valid = false
  }

  if (!form.value.email?.trim()) {
    errors.value.email = '请输入邮箱'
    valid = false
  } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.value.email)) {
    errors.value.email = '请输入有效的邮箱地址'
    valid = false
  }

  return valid
}

const saveEmployee = async () => {
  if (!validateForm()) return

  saving.value = true
  try {
    if (isEditing.value) {
      await api.put(`/users/${form.value.id}`, {
        name: form.value.name,
        role: form.value.role,
        isActive: form.value.isActive
      })
    } else {
      await api.post('/users', {
        name: form.value.name,
        email: form.value.email,
        role: form.value.role
      })
    }
    showDialog.value = false
    fetchEmployees()
  } catch (error) {
    console.error('Failed to save employee:', error)
    if (error.response?.data?.error) {
      const err = error.response.data.error
      if (err.code === 'EMAIL_EXISTS') {
        errors.value.email = err.message
      }
    }
  } finally {
    saving.value = false
  }
}

const deleteEmployee = async () => {
  if (!employeeToDelete.value) return

  deleting.value = true
  try {
    await api.delete(`/users/${employeeToDelete.value.id}`)
    showDeleteDialog.value = false
    employeeToDelete.value = null
    fetchEmployees()
  } catch (error) {
    console.error('Failed to delete employee:', error)
  } finally {
    deleting.value = false
  }
}

onMounted(() => {
  fetchEmployees()
})
</script>

<style scoped>
.admin-employees {
  padding: var(--spacing-lg);
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: var(--spacing-lg);
}

.page-header h2 {
  margin: 0;
  font-size: var(--font-size-2xl);
  font-weight: var(--font-weight-semibold);
}

.search-bar {
  margin-bottom: var(--spacing-lg);
  max-width: 300px;
}

.table-container {
  background: var(--bg-primary);
  border-radius: var(--radius-lg);
  overflow: hidden;
  margin-bottom: var(--spacing-lg);
}

.role-badge {
  display: inline-block;
  padding: var(--spacing-xs) var(--spacing-sm);
  border-radius: var(--radius-md);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-medium);
}

.role-badge.role-0 {
  background: var(--color-primary-light);
  color: var(--color-primary);
}

.role-badge.role-1 {
  background: #fef3c7;
  color: #d97706;
}

.role-badge.role-2 {
  background: #dbeafe;
  color: #2563eb;
}

.status-badge {
  display: inline-block;
  padding: var(--spacing-xs) var(--spacing-sm);
  border-radius: var(--radius-md);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-medium);
}

.status-badge.active {
  background: #dcfce7;
  color: #16a34a;
}

.status-badge.inactive {
  background: #fee2e2;
  color: #dc2626;
}

.action-buttons {
  display: flex;
  gap: var(--spacing-sm);
}

.employee-form {
  display: flex;
  flex-direction: column;
  gap: var(--spacing-md);
}

.warning-text {
  color: var(--color-danger);
  font-size: var(--font-size-sm);
  margin-top: var(--spacing-sm);
}
</style>