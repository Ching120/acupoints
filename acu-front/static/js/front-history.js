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

var user_id = localStorage.getItem("user_id");
console.log(user_id);
//日期
let url = `https://localhost:7105/api/D_record/GetD_record_date/${user_id}`;
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
        const p_title = document.querySelector(".date-space");
        let str = "";
        // 將資料存入
        arr.forEach(function (data) {
          // 原始日期字串
          const originalDateString = `${data.d_record_date}`;

          // 使用 Date 物件解析日期字串
          const originalDate = new Date(originalDateString);

          // 取得月份和日期部分
          const month = originalDate.getMonth() + 1; // 月份是從 0 開始的，所以要加 1
          const day = originalDate.getDate();

          // 格式化成 "MM/DD" 字串
          const formattedDateString = `${month}/${day}`;

          str += `
                <label><input type="radio" name="MM" value="${data.d_record_date}"/><span class="date-button mouse">${formattedDateString}</span></label>
                `;
        });
        p_title.innerHTML = str;
      }
    }
  });

//診斷日期
const dateSpace = document.querySelector(".date-space");
const dateDisplay = document.querySelector(".datee");
dateSpace.addEventListener("change", function (event) {
  if (event.target.type === "radio") {
    const selectedDate = event.target.value;
    // 原始日期字串
    const originalDateString = selectedDate;

    // 使用 Date 物件解析日期字串
    const originalDate = new Date(originalDateString);

    // 取得月份和日期部分
    const year = originalDate.getFullYear(); // 取得年份
    const month = originalDate.getMonth() + 1; // 月份是從 0 開始的，所以要加 1
    const day = originalDate.getDate();

    // 格式化成 "MM/DD" 字串
    const formattedDateString = `${year}-${month}-${day}`;

    dateDisplay.textContent = `${formattedDateString}`;
  }
  document.querySelector(".datee").innerHTML = dateDisplay.textContent;

  //OSDI診斷紀錄
  let url_2 = `https://localhost:7105/api/D_record/GetD_record/${user_id}/${dateDisplay.textContent}`;
  let data_2 = [];
  fetch(url_2) //判斷api有沒有資料
    .then((response) => {
      if (response.ok) {
        //如果api有資料就執行以下程式碼顯示資料

        // step 1 - 取得資料
        (function getData() {
          axios.get(url_2).then(function (response) {
            // 檢查
            console.log(response.data);
            // 將取得資料帶入空陣列data中
            data_2 = response.data;
            title(data_2);
          });
        })();

        function title(arr) {
          // 抓取欄位
          const p_title_2 = document.querySelector(".date-space_2");
          let str_2 = "";
          let score = "無";
          // 將資料存入
          arr.forEach(function (data_2) {
            if (`${data_2.d_record_score}` == 1) {
              score = "隨時";
            } else if (`${data_2.d_record_score}` == 2) {
              score = "約一半時間";
            } else if (`${data_2.d_record_score}` == 3) {
              score = "大部分時間";
            } else if (`${data_2.d_record_score}` == 4) {
              score = "隨時";
            } else {
              score = "無";
            }
            aa_2 = `
                    <table>
                        <tr>
                            <td class="title" colspan="2"><a class="center">OSDI診斷紀錄</a></td>
                        </tr>
                    `;
            str_2 += `
                    <tr>
                        <td class="answer">${data_2.eye_question_content}　(${score})</td>
                    </tr>
                    `;
            a_2 = `</table>`;
          });
          p_title_2.innerHTML = aa_2 + str_2 + a_2;
        }
      }
    });

  //穴位復健紀錄
  let url_3 = `https://localhost:7105/api/R_record/GetR_record/${user_id}/${dateDisplay.textContent}`;
  let data_3 = [];
  fetch(url_3) //判斷api有沒有資料
    .then((response) => {
      if (response.ok) {
        //如果api有資料就執行以下程式碼顯示資料

        // step 1 - 取得資料
        (function getData() {
          axios.get(url_3).then(function (response) {
            // 檢查
            console.log(response.data);
            // 將取得資料帶入空陣列data中
            data_3 = response.data;
            title(data_3);
          });
        })();

        function title(arr) {
          // 抓取欄位
          const p_title = document.querySelector(".date-space_3");
          let str = "";
          let finish = "未完成";
          // 將資料存入
          arr.forEach(function (data_3) {
            if (`${data_3.r_record_finish}` == 1) {
              finish = "完成";
            }
          let Color = finish === "完成" ? "green" : "red";
            aa = `
                    <table>
                        <tr>
                            <td class="topic">穴位名稱</td>
                            <td class="topic">是否完成復建</td>
                        </tr>
                    `;
            str += `
                    <tr>
                        <td class="topic bgcolor-white">${data_3.acupuncture_points_name}</td>
                        <td class="answer"><a class="${Color}" id="">${finish}</a></td>
                    </tr>
                    `;
            a = `</table>`;
          });
          p_title.innerHTML = aa + str + a;
        }
      }
    });
});

document.getElementById("exportPDF").addEventListener("click", function () {
  var element = document.getElementById("history"); //顯示 PDF 內容在 pdf-output 區塊。
  var options = {
    margin: 0, // 設定頁邊距，單位為毫米
    filename: "2023-09-24" + " _ history.pdf", // 設定生成的 PDF 檔案名稱
    image: { type: "jpeg", quality: 0.98 }, // 設定圖片品質和類型
    html2canvas: { scale: 5 }, // 設定 html2canvas 的參數
    jsPDF: { unit: "mm", format: "a3", orientation: "portrait" }, // 設定 jsPDF 的參數
  };

  // 將 HTML 轉換為 PDF
  html2pdf()
    .from(element) //指定從element生成 PDF
    .set(options)
    .save(); //保存生成的 PDF，會被提示要下載轉換後的檔案。
});

// function mdtopdf() {

//         var element = document.getElementById('pdf-output'); //顯示 PDF 內容在 pdf-output 區塊。
//         element.innerHTML = html; //渲染在頁面上

//         // 將 HTML 轉換為 PDF
//         html2pdf()
//             .from(element) //指定從element生成 PDF
//             .save(); //保存生成的 PDF，會被提示要下載轉換後的檔案。
//     };

//     reader.readAsText(file);
//     //readAsText(file) 是 FileReader 的方法之一，用於以文本格式讀取文件的內容。
//     //開始讀取指定的文件，文件讀取完成後，會觸發上面的 onload 事件，並將文件作為文本儲存在 event.target.result 中。
