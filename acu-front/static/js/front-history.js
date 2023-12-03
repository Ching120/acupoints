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

// 問卷紀錄
const D_record =
  "https://localhost:7105/api/D_record/GetD_record/d418bac0-aebe-4c89-8180-89b0c7cfc6f1/2023-10-19";

fetch(D_record)
  .then((response) => response.json())
  .then((data) => {
    d_recordTable(data);
  })
  .catch((error) => {
    console.error("API錯誤:", error);
  });

function d_recordTable(data) {
  const q1Tbody = document.getElementById("q1");
  const q2Tbody = document.getElementById("q2");
  const q3Tbody = document.getElementById("q3");

  data.forEach((item, index) => {
    const row = document.createElement("tr");
    const cell = document.createElement("td");

    let text;
    if (item.d_record_score === 4) {
      text = "(隨時)";
    } else if (item.d_record_score === 3) {
      text = "(大部分時間)";
    } else if (item.d_record_score === 2) {
      text = "(約一半時間)";
    } else if (item.d_record_score === 1) {
      text = "(偶爾)";
    } else if (item.d_record_score === 0) {
      text = "(無)";
    } else {
      text = "";
    }

    cell.className = "answer";
    cell.textContent = item.eye_question_content + " " + text;

    if (index < 8) {
      row.appendChild(cell);
      q1Tbody.appendChild(row);
    } else if (index >= 8 && index < 12) {
      row.appendChild(cell);
      q2Tbody.appendChild(row);
    } else if (index >= 12 && index < 15) {
      row.appendChild(cell);
      q3Tbody.appendChild(row);
    }
  });
}

//復健紀錄
const R_record =
  "https://localhost:7105/api/R_record/GetR_record/ec51afc8-b34c-43e3-9434-c5a071086478/2023-10-19"; // 替换为实际的API端点

fetch(R_record)
  .then((response) => response.json())

  .then((data) => {
    console.log("hihi", data);
    r_recordTable(data);
  })
  .catch((error) => {
    console.error("API錯誤:", error);
  });

function r_recordTable(data) {
  const q4Tbody = document.getElementById("q4");

  data.forEach((item, index) => {
    const row = document.createElement("tr");

    // 第一個 <td> 顯示穴位名稱
    const cell1 = document.createElement("td");
    cell1.className = "topic bgcolor-white";
    cell1.textContent = item.acupuncture_points_name;

    // 第二個 <td> 顯示是否完成復建
    const cell2 = document.createElement("td");
    cell2.className = "answer";
    const completionText = item.r_record_finish === 1 ? "完成" : "未完成";
    const completionClass = item.r_record_finish === 1 ? "green" : "red";
    const completionLink = document.createElement("a");
    completionLink.className = completionClass;
    completionLink.textContent = completionText;
    cell2.appendChild(completionLink);

    row.appendChild(cell1);
    row.appendChild(cell2);
    q4Tbody.appendChild(row);
  });
}
