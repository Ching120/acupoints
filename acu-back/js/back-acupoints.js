var logout1 = document.getElementById("logout");
logout1.addEventListener("click", logout);
    function logout() {
        if (confirm('確認要登出嗎？') == true) {
            window.location.href = "../acu-front/login.html";
            localStorage.clear();
        } else {

        }
    }


    const url = "https://localhost:7105/api/Acupuncture_Points/GetAcupuncture_PointsName";
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
                <a href="./back-acupoints-edit.html?acupuncture_points_id=${data.acupuncture_points_id}"><input type="button" class="point mouse" value="${data.acupuncture_points_name}"></a>
                `;
            });
            p_title.innerHTML = str;
    }}});