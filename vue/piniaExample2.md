1. useStore.js 
```js
import { defineStore } from "pinia";
import { ref, computed, } from "vue";
import jwt_decode from "jwt-decode";
import moment from "moment";
export const useStore = defineStore(
  "main",
  () => {
    let token = ref(null);

    const isLogin = computed(() => !!token.value == true && !isExpired.value);

    const tokenObject = computed(() =>
      !!token.value == true ? jwt_decode(token.value) : null
    );

    const exp = computed(() =>
      !!tokenObject.value == true
        ? moment.unix(tokenObject.value.exp).format("YYYY-MM-DD HH:mm:ss")
        : null
    );

    const isExpired = computed(() =>
      !!tokenObject.value == true ? moment().isAfter(exp.value) : false
    );

    const tokenHeaders = computed(() => {
      return !!token.value == true
        ? {
          headers: {
            Authorization: `bearer ${token.value}`,
          },
        }
        : {
          headers: {},
        };
    });

    const user = computed(() => {
      if (!!tokenObject.value == true) {
        return {
          userid:
            tokenObject.value[
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"
            ],
          username: tokenObject.value.userName,
          roles:
            tokenObject.value[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            ],
        };
      } else {
        return null;
      }
    });

    const login = (key) => {
      token.value = key;
    };

    const logout = () => {
      token.value = null;
    };

    return { isExpired, user, exp, isLogin, login, logout, tokenHeaders };
  },
);

// 這裡定義了Presist的方法
export function StorePresist(context) {

  let storage = sessionStorage.getItem(context.store.$id);
  if (storage)
    context.store.$patch(JSON.parse(storage));

  context.store.$subscribe((mutation, state) => {
    sessionStorage.setItem(mutation.storeId, JSON.stringify(state));
  })
}
``` 
2. main.js 
```js
import { createPinia } from "pinia";
import { StorePresist } from '@/store/useStore';
let pinia = createPinia();
pinia.use(StorePresist);
``` 

3. use pinia
```js
import { storeToRefs } from "pinia";
import { useStore } from "@/store/useStore";
let store = useStore();

const { user } = storeToRefs(store);
const { login } = store;

``` 
