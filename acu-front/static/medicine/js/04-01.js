const apiUrl =
  "https://localhost:7105/api/Chinese_Medicine/GetChinese_Medicine_Information/9c750ad9-e43a-4fdf-bfe3-aa3e05395aae";

fetch(apiUrl)
  .then((response) => {
    if (!response.ok) {
      throw new Error("Network response was not ok");
    }
    return response.json();
  })
  .then((data) => {
    const url =
      "https://localhost:7105/api/Chinese_Medicine/GetChinese_Medicine_Information/9c750ad9-e43a-4fdf-bfe3-aa3e05395aae";

    (function getData() {
      axios.get(url).then(function (response) {
        console.log(response.data);
        data = response.data;

        title(data);
      });
    })();

    function title(arr) {
      const q01 = document.querySelector("#q04-01");
      let str = "";

      arr.forEach(function (data) {
        const text = `${data.chinese_medicinal_other}`;
        const lines = text.split("\n");
        const formattedText = lines.join("<br>");
        console.log(formattedText);
        str += `
        <div class="make">
            <a class="title">製法用量：</a>
            <a class="content">桑白皮、半夏、蘇子、杏仁、貝母、山梔、黃芩、黃連 各2.4克</a>
        </div>
        <div class="effect">
            <a class="title">功效：</a>
            <a class="content">清肺降氣，化痰止嗽。</a>
        </div>
        <div class="cure">
            <a class="title">主治：</a>
            <a class="content">肺經熱甚，喘嗽痰多。</a>
        </div>`;
      });
      q01.innerHTML = str;
    }
  })
  .catch((error) => {
    console.error("Fetch error:", error);
  });
