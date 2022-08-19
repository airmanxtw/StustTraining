## form message example 

### GMsg.vue 
```js
<template>
    <q-banner dense inline-actions rounded  :class="`bg-${modelValue.type} text-white`">{{ modelValue.message }}</q-banner>
</template>
<script setup>
import {   watch } from 'vue';
let props = defineProps({
    modelValue: {
        type: Object,
        default() {
            return { type: '', message: '' }
        }
    }
})
let emits = defineEmits(['update:modelValue']);

watch(() => props.modelValue.type, (newVal) => {
    if (newVal) {
        setTimeout(() => {
            emits('update:modelValue', { type: '', message: '' });
        }, 6000);
    }
}, { deep: true })

</script>
``` 
### useMsg.js 
```js
import { ref } from "vue";
export function useMsg() {
  let message = ref({
    type: "",
    message: "",
  });
  const success = (msg) => {
    message.value.type = "positive";
    message.value.message = msg;
  };
  const warning = (msg) => {
    message.value.type = "warning";
    message.value.message = msg;
  };
  const error = (msg) => {
    message.value.type = "negative";
    message.value.message = msg;
  };
  return { message, success, warning, error };
}
``` 