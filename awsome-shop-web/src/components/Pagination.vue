<script setup>
import { computed } from 'vue'
import Button from './Button.vue'

const props = defineProps({
  currentPage: {
    type: Number,
    default: 1
  },
  totalItems: {
    type: Number,
    default: 0
  },
  pageSize: {
    type: Number,
    default: 10
  },
  pageSizeOptions: {
    type: Array,
    default: () => [10, 20, 50, 100]
  },
  showPageSize: {
    type: Boolean,
    default: true
  },
  showTotal: {
    type: Boolean,
    default: true
  },
  maxVisiblePages: {
    type: Number,
    default: 5
  }
})

const emit = defineEmits(['update:currentPage', 'update:pageSize', 'page-change'])

const totalPages = computed(() => Math.ceil(props.totalItems / props.pageSize))

const visiblePages = computed(() => {
  const pages = []
  const total = totalPages.value
  const current = props.currentPage
  const maxVisible = props.maxVisiblePages
  
  if (total <= maxVisible) {
    for (let i = 1; i <= total; i++) {
      pages.push(i)
    }
  } else {
    const half = Math.floor(maxVisible / 2)
    let start = current - half
    let end = current + half
    
    if (start < 1) {
      start = 1
      end = maxVisible
    }
    
    if (end > total) {
      end = total
      start = total - maxVisible + 1
    }
    
    for (let i = start; i <= end; i++) {
      pages.push(i)
    }
  }
  
  return pages
})

const showStartEllipsis = computed(() => {
  return visiblePages.value[0] > 2
})

const showEndEllipsis = computed(() => {
  return visiblePages.value[visiblePages.value.length - 1] < totalPages.value - 1
})

const startItem = computed(() => (props.currentPage - 1) * props.pageSize + 1)
const endItem = computed(() => Math.min(props.currentPage * props.pageSize, props.totalItems))

const goToPage = (page) => {
  if (page >= 1 && page <= totalPages.value && page !== props.currentPage) {
    emit('update:currentPage', page)
    emit('page-change', page)
  }
}

const handlePageSizeChange = (event) => {
  const newSize = parseInt(event.target.value, 10)
  emit('update:pageSize', newSize)
  emit('update:currentPage', 1)
  emit('page-change', 1)
}
</script>

<template>
  <div class="pagination">
    <div v-if="showTotal" class="pagination-info">
      Showing {{ startItem }} to {{ endItem }} of {{ totalItems }} entries
    </div>
    
    <div class="pagination-controls">
      <Button 
        size="sm" 
        :disabled="currentPage <= 1" 
        @click="goToPage(currentPage - 1)"
      >
        Previous
      </Button>
      
      <div class="pagination-pages">
        <button 
          v-if="showStartEllipsis"
          class="pagination-ellipsis"
          @click="goToPage(1)"
        >
          1
        </button>
        <span v-if="showStartEllipsis" class="pagination-dots">...</span>
        
        <button
          v-for="page in visiblePages"
          :key="page"
          class="pagination-page"
          :class="{ active: page === currentPage }"
          @click="goToPage(page)"
        >
          {{ page }}
        </button>
        
        <span v-if="showEndEllipsis" class="pagination-dots">...</span>
        <button 
          v-if="showEndEllipsis"
          class="pagination-ellipsis"
          @click="goToPage(totalPages)"
        >
          {{ totalPages }}
        </button>
      </div>
      
      <Button 
        size="sm" 
        :disabled="currentPage >= totalPages" 
        @click="goToPage(currentPage + 1)"
      >
        Next
      </Button>
    </div>
    
    <div v-if="showPageSize" class="pagination-size">
      <label>
        Show
        <select :value="pageSize" class="page-size-select" @change="handlePageSizeChange">
          <option v-for="size in pageSizeOptions" :key="size" :value="size">
            {{ size }}
          </option>
        </select>
        entries
      </label>
    </div>
  </div>
</template>

<style scoped>
.pagination {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
  gap: var(--spacing-md);
  padding: var(--spacing-md) 0;
}

.pagination-info {
  font-size: var(--font-size-sm);
  color: var(--text-muted);
}

.pagination-controls {
  display: flex;
  align-items: center;
  gap: var(--spacing-sm);
}

.pagination-pages {
  display: flex;
  align-items: center;
  gap: var(--spacing-xs);
}

.pagination-page,
.pagination-ellipsis {
  min-width: 32px;
  height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0 var(--spacing-sm);
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-medium);
  color: var(--text-secondary);
  background: transparent;
  border: 1px solid var(--border-color);
  border-radius: var(--radius-md);
  cursor: pointer;
  transition: all var(--transition-fast);
}

.pagination-page:hover:not(.active) {
  background: var(--bg-tertiary);
  border-color: var(--text-muted);
}

.pagination-page.active {
  background: var(--color-primary);
  border-color: var(--color-primary);
  color: var(--text-inverse);
}

.pagination-dots {
  padding: 0 var(--spacing-xs);
  color: var(--text-muted);
}

.pagination-size {
  font-size: var(--font-size-sm);
  color: var(--text-secondary);
}

.pagination-size label {
  display: flex;
  align-items: center;
  gap: var(--spacing-sm);
}

.page-size-select {
  padding: var(--spacing-xs) var(--spacing-sm);
  font-size: var(--font-size-sm);
  color: var(--text-primary);
  background: var(--bg-primary);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-md);
  cursor: pointer;
}

.page-size-select:focus {
  outline: none;
  border-color: var(--color-primary);
}

/* Responsive */
@media (max-width: 640px) {
  .pagination {
    flex-direction: column;
    align-items: stretch;
  }
  
  .pagination-controls {
    justify-content: center;
  }
  
  .pagination-info,
  .pagination-size {
    text-align: center;
  }
}
</style>