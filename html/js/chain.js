let user = [
  { name: "airmanx", active: true },
  { name: "airmanx2", active: false },
];

const result = user
  .map((u) => {
    console.log(u);
    return u;
  })
  .filter((u) => u.active)
  .map((u) => u.name);

console.log(result);
