<script setup>
import { computed } from 'vue'

const props = defineProps({
  columns: {
    type: Array,
    required: true,
    default: () => []
  },
  data: {
    type: Array,
    required: true,
    default: () => []
  },
  striped: {
    type: Boolean,
    default: true
  },
  bordered: {
    type: Boolean,
    default: false
  },
  hoverable: {
    type: Boolean,
    default: true
  },
  compact: {
    type: Boolean,
    default: false
  },
  emptyText: {
    type: String,
    default: 'No data available'
  }
})

const emit = defineEmits(['row-click', 'sort'])

const getCellValue = (row, key) => {
  if (typeof key === 'function') {
    return key(row)
  }
  return row[key]
}

const handleRowClick = (row, index) => {
  emit('row-click', { row, index })
}
</script>

<template>
  <div class="table-wrapper">
    <table class="table" :class="{ 
      'table-striped': striped, 
      'table-bordered': bordered, 
      'table-hoverable': hoverable,
      'table-compact': compact
    }">
      <thead>
        <tr>
          <th 
            v-for="col in columns" 
            :key="col.key"
            :style="{ width: col.width, textAlign: col.align || 'left' }"
            :class="{ 'sortable': col.sortable }"
            @click="col.sortable && emit('sort', col.key)"
          >
            {{ col.label }}
            <span v-if="col.sortable" class="sort-icon">
              <svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
                <path d="M7 15l5 5 5-5M7 9l5-5 5 5"/>
              </svg>
            </span>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr 
          v-for="(row, index) in data" 
          :key="index"
          @click="handleRowClick(row, index)"
        >
          <td 
            v-for="col in columns" 
            :key="col.key"
            :style="{ textAlign: col.align || 'left' }"
          >
            <slot :name="`cell-${col.key}`" :row="row" :value="getCellValue(row, col.key)">
              {{ getCellValue(row, col.key) }}
            </slot>
          </td>
        </tr>
        <tr v-if="!data.length" class="empty-row">
          <td :colspan="columns.length" class="empty-cell">
            {{ emptyText }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.table-wrapper {
  width: 100%;
  overflow-x: auto;
}

.table {
  width: 100%;
  border-collapse: collapse;
  font-size: var(--font-size-base);
}

.table th,
.table td {
  padding: var(--spacing-md);
  text-align: left;
  border-bottom: 1px solid var(--border-color);
}

.table th {
  font-weight: var(--font-weight-semibold);
  color: var(--text-primary);
  background: var(--bg-secondary);
  white-space: nowrap;
}

.table th.sortable {
  cursor: pointer;
  user-select: none;
}

.table th.sortable:hover {
  background: var(--bg-tertiary);
}

.sort-icon {
  margin-left: var(--spacing-xs);
  opacity: 0.5;
}

.table td {
  color: var(--text-secondary);
}

/* Striped */
.table-striped tbody tr:nth-child(even) {
  background: var(--bg-secondary);
}

/* Bordered */
.table-bordered th,
.table-bordered td {
  border: 1px solid var(--border-color);
}

/* Hoverable */
.table-hoverable tbody tr {
  cursor: pointer;
  transition: background var(--transition-fast);
}

.table-hoverable tbody tr:hover {
  background: var(--bg-tertiary);
}

/* Compact */
.table-compact th,
.table-compact td {
  padding: var(--spacing-sm);
}

/* Empty */
.empty-row .empty-cell {
  text-align: center;
  color: var(--text-muted);
  padding: var(--spacing-2xl);
}

/* Responsive */
@media (max-width: 640px) {
  .table th,
  .table td {
    padding: var(--spacing-sm);
    font-size: var(--font-size-sm);
  }
}
</style>