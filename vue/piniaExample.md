```js 
import { defineStore } from "pinia";
import { ref, computed } from "vue";
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

    return { isExpired, user, exp, isLogin, login, logout };
  },
  {
    persist: {
      enabled: true,
    },
  }
);
```