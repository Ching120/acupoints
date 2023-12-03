const apiUrl =
  "https://localhost:7105/api/Chinese_Medicine/GetChinese_Medicine_Information/46510f5e-ef9f-4af1-9eb0-999dfe103d5f";

fetch(apiUrl)
  .then((response) => {
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
    return response.json();
  })
  .then((data) => {
    const url =
      "https://localhost:7105/api/Chinese_Medicine/GetChinese_Medicine_Information/46510f5e-ef9f-4af1-9eb0-999dfe103d5f";

    (function getData() {
      axios.get(url).then(function (response) {
        console.log(response.data);
        data = response.data;

        title(data);
      });
    })();

    function title(arr) {
      const q01 = document.querySelector("#q06-01");
      let str = "";

      arr.forEach(function (data) {
        const text = `${data.chinese_medicinal_other}`;
        const lines = text.split("\n");
        const formattedText = lines.join("<br>");
        console.log(formattedText);
        str += `
        <div class="tips">${data.chinese_medicine_taboo}</div>
        <div class="make">
            <a class="title">製法用量：</a>
            <a class="content">${data.chinese_medicinal_materials}</a>
        </div>
        <div class="effect">
            <a class="title">功效：</a>
            <a class="content">${data.chinese_medicinal_effect}</a>
        </div>
        <div class="cure">
            <a class="title">主治：</a>
            <a class="content">${data.chinese_medicinal_main}</a>
        </div>
        <div class="significance">
            <a class="title">方義：</a>
            <a class="content">${data.chinese_medicinal_explain}</a>
        </div>
        <div class="add">
            <a class="title" >加減：</a>
            <a class="content">${formattedText}</a>
        </div>`;
      });
      q01.innerHTML = str;
    }
  })
  .catch((error) => {
    console.error("Fetch error:", error);
  });
