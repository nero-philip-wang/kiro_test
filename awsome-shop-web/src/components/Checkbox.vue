<script setup>
import { computed } from 'vue'

const props = defineProps({
  modelValue: {
    type: [Boolean, Array],
    default: false
  },
  value: {
    type: [String, Number, Boolean],
    default: null
  },
  label: {
    type: String,
    default: ''
  },
  disabled: {
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
  indeterminate: {
    type: Boolean,
    default: false
  }
})

const emit = defineEmits(['update:modelValue', 'change'])

const checkboxId = computed(() => props.id || `checkbox-${Math.random().toString(36).substr(2, 9)}`)

const isChecked = computed(() => {
  if (Array.isArray(props.modelValue)) {
    return props.modelValue.includes(props.value)
  }
  return props.modelValue === true
})

const handleChange = (event) => {
  const checked = event.target.checked
  
  if (Array.isArray(props.modelValue)) {
    const newValue = [...props.modelValue]
    if (checked) {
      newValue.push(props.value)
    } else {
      const index = newValue.indexOf(props.value)
      if (index > -1) {
        newValue.splice(index, 1)
      }
    }
    emit('update:modelValue', newValue)
    emit('change', newValue)
  } else {
    emit('update:modelValue', checked)
    emit('change', checked)
  }
}
</script>

<template>
  <label 
    class="checkbox-wrapper" 
    :class="{ 
      'is-disabled': disabled,
      'is-checked': isChecked,
      'is-indeterminate': indeterminate
    }"
  >
    <input
      :id="checkboxId"
      type="checkbox"
      :checked="isChecked"
      :disabled="disabled"
      :indeterminate="indeterminate"
      :name="name"
      :value="value"
      class="checkbox-input"
      @change="handleChange"
    />
    <span class="checkbox-box" :class="`checkbox-${size}`">
      <svg v-if="indeterminate" class="checkbox-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3">
        <line x1="5" y1="12" x2="19" y2="12"></line>
      </svg>
      <svg v-else-if="isChecked" class="checkbox-icon" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="3">
        <polyline points="20 6 9 17 4 12"></polyline>
      </svg>
    </span>
    <span v-if="label" class="checkbox-label">{{ label }}</span>
  </label>
</template>

<style scoped>
.checkbox-wrapper {
  display: inline-flex;
  align-items: center;
  gap: var(--spacing-sm);
  cursor: pointer;
  user-select: none;
}

.checkbox-wrapper.is-disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.checkbox-input {
  position: absolute;
  opacity: 0;
  width: 0;
  height: 0;
}

.checkbox-box {
  display: flex;
  align-items: center;
  justify-content: center;
  border: 2px solid var(--border-color);
  border-radius: var(--radius-sm);
  background: var(--bg-primary);
  transition: all var(--transition-fast);
  flex-shrink: 0;
}

.checkbox-sm .checkbox-box {
  width: 16px;
  height: 16px;
}

.checkbox-md .checkbox-box {
  width: 20px;
  height: 20px;
}

.checkbox-lg .checkbox-box {
  width: 24px;
  height: 24px;
}

.checkbox-input:focus-visible + .checkbox-box {
  outline: 2px solid var(--color-primary);
  outline-offset: 2px;
}

.checkbox-wrapper.is-checked .checkbox-box {
  background: var(--color-primary);
  border-color: var(--color-primary);
}

.checkbox-wrapper.is-indeterminate .checkbox-box {
  background: var(--color-primary);
  border-color: var(--color-primary);
}

.checkbox-icon {
  color: var(--text-inverse);
  width: 14px;
  height: 14px;
}

.checkbox-sm .checkbox-icon {
  width: 12px;
  height: 12px;
}

.checkbox-lg .checkbox-icon {
  width: 16px;
  height: 16px;
}

.checkbox-label {
  font-size: var(--font-size-base);
  color: var(--text-primary);
}

.checkbox-sm .checkbox-label {
  font-size: var(--font-size-sm);
}

.checkbox-lg .checkbox-label {
  font-size: var(--font-size-lg);
}

.checkbox-wrapper.is-disabled .checkbox-label {
  opacity: 0.7;
}
</style>