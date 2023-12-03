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

const user_id = localStorage.getItem("user_id");
console.log(user_id);

//JSON 檔案網址
const url = "https://localhost:7105/api/CM_question/GetCM_Question_Button";
let data = [];
fetch(url) //判斷api有沒有資料
  .then((response) => {
    if (response.ok) {
      //如果api有資料就執行以下程式碼顯示資料

      // step 1 - 取得資料
      (function getData() {
        axios.get(url).then(function (response) {
          // 檢查
          console.log(response.data);
          // 將取得資料帶入空陣列data中
          data = response.data;
          title(data);
        });
      })();

      function title(arr) {
        // 抓取欄位
        const p_title = document.querySelector(".Q-button-group");
        let str = "";
        let bb = 11; //這
        let cc = 0; //這
        // 將資料存入
        arr.forEach(function (data) {
          cc++;
          if (cc == 5 || cc == 9 || cc == 13 || cc == 17 || cc == 21) {
            bb += 11; //這
          }
          str += `
        <label><input class="mouse button" type="checkbox" value="${data.cM_question}" id="01" name="0${bb}" onclick="return checkCount(this)" /><span class="Q-button mouse">${data.cM_question}</span></label>
        `;
          // <input id="delete${data.eye_question_id}" class="delete mouse" type="button" value="刪除"></input>
        });
        p_title.innerHTML = str;
      }
    }
  });

var checkedCount = 0;
var count = document.getElementById("count");
count.innerHTML = "已選" + checkedCount + "/7";
function checkCount(obj) {
  if (obj.checked === true) {
    if (checkedCount >= 7) {
      alert("最多只能選擇7個哦");
      return false;
    }

    checkedCount++;

    if (checkedCount <= 7) {
      var count = document.getElementById("count");
      count.innerHTML = 0;
      count.innerHTML = "已選" + checkedCount + "/7";
      if (checkedCount == 7) {
        document.querySelector(".count").classList.add("red");
      }
    }
  } else {
    checkedCount--;

    if (checkedCount < 7) {
      var count = document.getElementById("count");
      count.innerHTML = "已選" + checkedCount + "/7";
      if (checkedCount < 7) {
        document.querySelector(".count").classList.remove("red");
      }
    }
  }

  return true;
}

function next() {
  // if(checkedCount == 0){
  //     alert("最少要選擇1個哦");
  //     return false;
  // }else
  if (checkedCount <= 7) {
    window.location.href = "./front-diagnosis-q04.html";
    return true;
  }
}

const p1total = parseInt(localStorage.getItem("p1total"), 10);
//把上一頁得到的分數加總
var getUrlString = location.href; //取得網址
var url1 = new URL(getUrlString); //將網址 (字串轉成URL)
const aa = url1.searchParams.get("contact01"); //取得網址裡01=?
const bb = url1.searchParams.get("contact02"); //取得網址裡02=?
const cc = url1.searchParams.get("contact03"); //取得網址裡03=?
const dd = url1.searchParams.get("contact04"); //取得網址裡04=?
const ee = url1.searchParams.get("contact05"); //取得網址裡05=?
const ff = url1.searchParams.get("contact06"); //取得網址裡06=?
const gg = url1.searchParams.get("contact07"); //取得網址裡07=?

var aa1 = parseInt(aa, 10);
var bb1 = parseInt(bb, 10);
var cc1 = parseInt(cc, 10);
var dd1 = parseInt(dd, 10);
var ee1 = parseInt(ee, 10);
var ff1 = parseInt(ff, 10);
var gg1 = parseInt(gg, 10);

var num = 0;
var score = 0;
num =
  p1total +
  Number(aa) +
  Number(bb) +
  Number(cc) +
  Number(dd) +
  Number(ee) +
  Number(ff) +
  Number(gg);
score = Math.round((num * 25) / 15);

// console.log(num);
console.log(score);
// alert(total);

//  total in localStorage
localStorage.setItem("totalValue", score);

