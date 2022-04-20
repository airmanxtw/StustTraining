# 增加alias 
1. 我們如果想將~這個符號指定至@/components,點選打開vue.config.js，加入configureWebpack  
```js
const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  configureWebpack: {
    resolve: {
      alias: {
        '~': '@/components',
      }
    }
  }
})
```

2. 打開jsconfig.json,找到paths,加入~/*
```js
"paths": {
      "@/*": [
        "src/*"
      ],
      "~/*": [
        "src/components/*"
      ]
    },
```

3. 可以使用~/了  
``` js
import HelloWorld from "~/HelloWorld.vue";
```
