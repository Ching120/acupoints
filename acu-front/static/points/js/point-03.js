function ButtonText(button) {
  if (button.value === "完成穴位按摩") {
    button.value = "取消穴位按摩";
    button.style.backgroundColor = "gray";
    button.style.color = "white";
  } else {
    button.value = "完成穴位按摩";
    button.style.backgroundColor = "";
    button.style.color = "";
  }
}
