```js
onMounted(() => {
  merge(fromEvent(window, 'mouseover'), fromEvent(window, 'keydown'))
    .pipe(throttleTime(1000 * 2))
    .subscribe(() => {
      if (isLogin.value) {               
        ajax.get('/login/re', tokenHeaders.value)
          .then(res => {
            login(res.data);
            console.log(user.value);
          })
          .catch(err => {
            console.log(err);
          })
      }
      else {
        const fullPath = router.currentRoute.value.fullPath;
        if (fullPath != "/login")
          router.push("/login");
      }
    })
})
``` 