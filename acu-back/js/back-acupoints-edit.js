var logout1 = document.getElementById("logout");
logout1.addEventListener("click", logout);
    function logout() {
        if (confirm('確認要登出嗎？') == true) {
            window.location.href = "../acu-front/login.html";
            localStorage.clear();
        } else {

        }
    }

    var uu = window.location.href;
    var product_id = uu.split("=");
    console.log(product_id[1]);

    const url = `https://localhost:7105/api/Acupuncture_Points/GetAcupuncture_Points_Infromation/`+
    product_id[1];

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
            const p_title = document.querySelector(".table_content");
            let str = "";
            // 將資料存入
            arr.forEach(function (data) {
                str += `
                      <table>
                        <tr>
                            <td class="Q-title"><i class="fa-solid fa-font"></i>　穴道名稱：<input class="input_question" type="text" value="${data.acupuncture_points_name}"><input type="button" class="update-btn  mouse" id="insert0" value="修改穴道名稱"></td>
                        </tr>
                        <tr>
                            <td class="Q-title"><i class="fa-solid fa-location-crosshairs"></i>　穴道位置：<input type="button" class="update-btn  mouse" id="insert1" value="修改穴道位置"></td>
                        </tr>
                        <tr>
                            <td class="Q-title"><i class="fa-solid fa-compress"></i>　按壓方式：<input type="button" class="update-btn  mouse" id="insert2" value="修改按壓方式"></td>
                        </tr>
                        <tr>
                            <td class="Q-title"><i class="fa-solid fa-signature"></i>　穴名介紹：<input type="button" class="update-btn  mouse" id="insert3" value="修改穴名介紹"></td>
                        </tr>
                        <tr>
                            <td class="Q-title"><i class="fa-solid fa-book-journal-whills"></i>　穴道介紹：<input type="button" class="update-btn  mouse" id="insert4" value="修改穴道介紹"></td>
                        </tr>
                    </table>
                `;
            });
            p_title.innerHTML = str;
    }}});

        
    var button1 = document.getElementById("insert1");
    var button2 = document.getElementById("insert2");
    var button3 = document.getElementById("insert3");
    var button4 = document.getElementById("insert4");
    button1.addEventListener("click", showPopupp1);
    button2.addEventListener("click", showPopupp2);
    button3.addEventListener("click", showPopupp3);
    button4.addEventListener("click", showPopupp4);
    var change = document.getElementById("change");
    function showPopupp1() {
        // var change = document.getElementById("change");
        change.innerHTML="<div class='acupoint_info'><i class='fa-solid fa-location-crosshairs'>　穴道位置：</div><textarea name='editorDemo' id='editorDemo' style='white-space:pre-wrap;' class='textarea'></textarea>"
        var editor = CKEDITOR.replace("editorDemo");
        document.getElementById("popupp").style.display = "block"; // 顯示浮動視窗
    }
    function showPopupp2() {
        // var change = document.getElementById("change");
        change.innerHTML="<div class='acupoint_info'><i class='fa-solid fa-compress'></i>　按壓方式：</div><textarea name='editorDemo' id='editorDemo' style='white-space:pre-wrap;' class='textarea'></textarea>"
        var editor = CKEDITOR.replace("editorDemo");
        document.getElementById("popupp").style.display = "block"; // 顯示浮動視窗
    }
    function showPopupp3() {
        // var change = document.getElementById("change");
        change.innerHTML="<div class='acupoint_info'><i class='fa-solid fa-signature'></i>　穴名介紹：</div><textarea name='editorDemo' id='editorDemo' style='white-space:pre-wrap;' class='textarea'></textarea>"
        var editor = CKEDITOR.replace("editorDemo");
        document.getElementById("popupp").style.display = "block"; // 顯示浮動視窗
    }
    function showPopupp4() {
        // var change = document.getElementById("change");
        change.innerHTML="<div class='acupoint_info'><i class='fa-solid fa-book-journal-whills'></i>　穴道名稱：</div><textarea name='editorDemo' id='editorDemo' style='white-space:pre-wrap;' class='textarea'></textarea>"
        var editor = CKEDITOR.replace("editorDemo");
        document.getElementById("popupp").style.display = "block"; // 顯示浮動視窗
    }

    function insertcancel() {
        // 關閉浮動視窗
        document.getElementById("popupp").style.display = "none";
      }
