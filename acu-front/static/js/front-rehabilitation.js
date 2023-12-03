//登出
var logout1 = document.getElementById("logout");
logout1.addEventListener("click", logout);
function logout() {
  if (confirm("確認要登出嗎？") == true) {
    window.location.href = "/static/login.html";
    localStorage.clear();
  } else {
  }
}
