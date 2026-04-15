<script setup>
import { computed, ref } from 'vue'

const props = defineProps({
  modelValue: {
    type: [String, Number, null],
    default: null
  },
  options: {
    type: Array,
    default: () => []
  },
  label: {
    type: String,
    default: ''
  },
  placeholder: {
    type: String,
    default: 'Select an option'
  },
  error: {
    type: String,
    default: ''
  },
  disabled: {
    type: Boolean,
    default: false
  },
  required: {
    type: Boolean,
    default: false
  },
  id: {
    type: String,
    default: ''
  },
  name: {
    type: String,
    default: ''
  },
  size: {
    type: String,
    default: 'md',
    validator: (value) => ['sm', 'md', 'lg'].includes(value)
  },
  labelKey: {
    type: String,
    default: 'label'
  },
  valueKey: {
    type: String,
    default: 'value'
  }
})

const emit = defineEmits(['update:modelValue', 'change'])

const selectId = computed(() => props.id || `select-${Math.random().toString(36).substr(2, 9)}`)
const isOpen = ref(false)

const selectedLabel = computed(() => {
  const selected = props.options.find(opt => opt[props.valueKey] === props.modelValue)
  return selected ? selected[props.labelKey] : ''
})

const handleChange = (event) => {
  const value = event.target.value
  const selected = props.options.find(opt => opt[props.valueKey] == value)
  emit('update:modelValue', selected ? selected[props.valueKey] : null)
  emit('change', selected)
}

const toggleDropdown = () => {
  if (!props.disabled) {
    isOpen.value = !isOpen.value
  }
}

const selectOption = (option) => {
  emit('update:modelValue', option[props.valueKey])
  emit('change', option)
  isOpen.value = false
}
</script>

<template>
  <div class="select-wrapper" :class="{ 'has-error': error, 'is-disabled': disabled, 'is-open': isOpen }">
    <label v-if="label" :for="selectId" class="select-label">
      {{ label }}
      <span v-if="required" class="required-mark">*</span>
    </label>
    
    <div class="select-container">
      <select
        :id="selectId"
        :value="modelValue"
        :disabled="disabled"
        :required="required"
        :name="name"
        :class="['select', `select-${size}`]"
        @change="handleChange"
        @blur="isOpen = false"
      >
        <option v-if="placeholder" value="" disabled>{{ placeholder }}</option>
        <option 
          v-for="option in options" 
          :key="option[valueKey]" 
          :value="option[valueKey]"
        >
          {{ option[labelKey] }}
        </option>
      </select>
      
      <div class="select-arrow">
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <polyline points="6 9 12 15 18 9"></polyline>
        </svg>
      </div>
    </div>
    
    <span v-if="error" class="select-error">{{ error }}</span>
  </div>
</template>

<style scoped>
.select-wrapper {
  display: flex;
  flex-direction: column;
  gap: var(--spacing-xs);
}

.select-label {
  font-size: var(--font-size-sm);
  font-weight: var(--font-weight-medium);
  color: var(--text-primary);
}

.required-mark {
  color: var(--color-danger);
  margin-left: 2px;
}

.select-container {
  position: relative;
}

.select {
  width: 100%;
  padding: var(--spacing-sm) var(--spacing-xl) var(--spacing-sm) var(--spacing-md);
  font-size: var(--font-size-base);
  color: var(--text-primary);
  background: var(--bg-primary);
  border: 1px solid var(--border-color);
  border-radius: var(--radius-md);
  cursor: pointer;
  appearance: none;
  transition: border-color var(--transition-fast), box-shadow var(--transition-fast);
}

.select:focus {
  outline: none;
  border-color: var(--color-primary);
  box-shadow: 0 0 0 3px var(--color-primary-light);
}

.select:disabled {
  background: var(--bg-tertiary);
  cursor: not-allowed;
  opacity: 0.7;
}

.select-arrow {
  position: absolute;
  right: var(--spacing-md);
  top: 50%;
  transform: translateY(-50%);
  pointer-events: none;
  color: var(--text-muted);
  transition: transform var(--transition-fast);
}

.is-open .select-arrow {
  transform: translateY(-50%) rotate(180deg);
}

/* Sizes */
.select-sm {
  padding: var(--spacing-xs) var(--spacing-xl) var(--spacing-xs) var(--spacing-sm);
  font-size: var(--font-size-sm);
  min-height: 32px;
}

.select-md {
  min-height: 40px;
}

.select-lg {
  padding: var(--spacing-md) var(--spacing-xl) var(--spacing-md) var(--spacing-lg);
  font-size: var(--font-size-lg);
  min-height: 48px;
}

/* Error state */
.has-error .select {
  border-color: var(--color-danger);
}

.has-error .select:focus {
  box-shadow: 0 0 0 3px rgba(239, 68, 68, 0.2);
}

.select-error {
  font-size: var(--font-size-sm);
  color: var(--color-danger);
}

/* Disabled state */
.is-disabled .select-label {
  opacity: 0.7;
}
</style>